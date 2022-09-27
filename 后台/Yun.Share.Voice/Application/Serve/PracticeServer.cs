using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yun.Share.Voice.DataBase;
using Yun.Share.Voice.IApplication;
using Yun.Share.Voice.IApplication.Dtos.Models;
using Yun.Share.Voice.IApplication.Input;
using Yun.Share.Voice.IApplication.UtilDtos;
using Yun.Share.Voice.Models.Entities;
using Yun.Share.Voice.Utils;

namespace Yun.Share.Voice.Application.Serve
{
    public class PracticeServer : BaseVoiceServer<Practice, PracticeDto, PracticeListInput, PracticeDto>, IPracticeServer
    {
        private readonly CoreDbContext _db;
        public PracticeServer(CoreDbContext db)
        {
            _db = db;
            base.query = db.Practices.AsQueryable();
        }
        public override async Task<PracticeDto> CreateAsync(PracticeDto input)
        {
            Practice per = input.MapTo<Practice, PracticeDto>();
            await _db.Practices.AddAsync(per);
            await _db.SaveChangesAsync();
            return per.MapTo<PracticeDto, Practice>();
        }

        public override async Task<PracticeDto> GetAsync(Guid Id)
        {
            Practice per = await _db.Practices.FindAsync(Id);
            return per.MapTo<PracticeDto, Practice>();
        }

        
        public async Task<PracticeDto> GetNextAsync(Guid Id)
        {
            Practice per = await _db.Practices.FindAsync(Id);
            var perItem = await _db.Practices.FirstOrDefaultAsync(x => x.CreationTime < per.CreationTime);
            if(perItem == null)
            {
                perItem = await _db.Practices.OrderByDescending(x=>x.CreationTime).FirstOrDefaultAsync();
            }
            return perItem.MapTo<PracticeDto, Practice>();
        }

        public async Task<PracticeDto> GetRandomAsync(PracticeListInput input)
        {
            if(!input.Ids.IsNotEmpty())
            {
                
                input.Ids = new List<Guid>();
            }
            var perItem = await _db.Practices.Where(x => !input.Ids.Contains(x.Id)).OrderBy(x=> Guid.NewGuid()).FirstOrDefaultAsync();
            return perItem.MapTo<PracticeDto, Practice>();
        }

        public override async Task<PracticeDto> UpdateAsync(PracticeDto input)
        {
            Practice per = await _db.Practices.FindAsync(input.Id);
            per.Title = input.Title;
            per.SubjectTypeId = input.SubjectTypeId;
            per.CarTypeId = input.CarTypeId;
            await _db.SaveChangesAsync();
            return per.MapTo<PracticeDto, Practice>();
        }

        public override async Task<PagedResultDto<PracticeDto>> GetListAsync(PracticeListInput input)
        {
            return await base.GetListAsync(input);
        }
        /// <summary>
        /// 初始化外键
        /// </summary>
        /// <param name="iq"></param>
        /// <returns></returns>
        private IQueryable<Practice> IncludeDefault(IQueryable<Practice> iq)
        {
            return iq
                .Include(x => x.PracticeImages)
                .Include(x => x.CarType)
                .Include(x => x.Options)
                .Include(x => x.SubjectType);
        }
        protected override IQueryable<Practice> Filter(IQueryable<Practice> iq, PracticeListInput input)
        {
            iq = IncludeDefault(iq);

            if (input.Name.IsNotEmpty())
            {
                iq = iq.Where(x => x.Title.Contains(input.Name));
            }
            if (input.CarTypeId.HasValue)
            {
                iq = iq.Where(x => x.CarTypeId == input.CarTypeId);
            }
            if (input.SubjectTypeId.HasValue)
            {
                iq = iq.Where(x => x.SubjectTypeId == input.SubjectTypeId);
            }
            if (input.Ids.IsNotEmpty())
            {
                iq = iq.Where(x => input.Ids.Contains(x.Id));
            }
            if (input.StatusTypeEnum.HasValue)
            {
                iq = iq.Where(x => x.StatusTypeEnum == input.StatusTypeEnum);
            }
            if (input.UnIds.IsNotEmpty())
            {
                iq = iq.Where(x => !input.UnIds.Contains(x.Id));
            }
            return iq;
        }

        protected override async Task<List<PracticeDto>> MapToGetListOutputDtosAsync(List<Practice> entities)
        {
            var list = entities.Select(x => x.MapTo<PracticeDto, Practice>()).ToList();
            return list;
        }

        public async Task<bool> UploadFilePath(IFormFile file)
        {
            ExcelUtil excelUtil = new ExcelUtil();
            var dt =  excelUtil.ExcelToTable(file);
            List<Practice> list = new List<Practice>();
            var car = await _db.CarTypes.FirstOrDefaultAsync(x => x.Name == "小车C1C2C3");
            var su = await _db.SubjectTypes.FirstOrDefaultAsync(x => x.Name == "科目一");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Practice per = new Practice()
                {
                    CarTypeId = car.Id,
                    SubjectTypeId = su.Id,
                    ChoiceTyope = dt.Rows[i][1].ToString().Contains("判断")? Enum.ChoiceTyope.Choice: dt.Rows[i][1].ToString().Contains("单选")? Enum.ChoiceTyope.Single: Enum.ChoiceTyope.More,
                    Title = dt.Rows[i][3].ToString(),
                    Skill = dt.Rows[i][9].ToString(),
                    SkillLast = dt.Rows[i][10].ToString(),
                    Introduce = dt.Rows[i][11].ToString()
                };
                var images = dt.Rows[i][2].ToString();
                if(images.IsNotEmpty())
                {
                    var sp = images.Split(new char[] { ',', '，' });
                    List<PracticeImage> listImage = new List<PracticeImage>();
                    foreach (var im in sp)
                    {
                        if(im.IsNotEmpty())
                        {
                            listImage.Add(new PracticeImage
                            {
                                Url = im
                            });
                        }
                    }
                    per.PracticeImages = listImage;
                }
                var isIndex = dt.Rows[i][8].ToString().Trim();
                List<Option> ops = new List<Option>();
                for (var j = 4; j< 8;j++)
                {
                    var sp = dt.Rows[i][j].ToString();
                    if (sp.Split(' ').Length >1)
                    {
                        Option op = new Option()
                        {
                            Index = j - 3,
                            Title = sp,
                            IsCorrect = false
                        };
                        if(sp[0].ToString().Trim() == isIndex)
                        {
                            op.IsCorrect = true;
                        }
                        ops.Add(op);
                    }
                }
                per.Options = ops;



                for (int j = 1; j < dt.Columns.Count; j++)
                {
                    var dataStr = dt.Rows[i][1].ToString();
                }
                list.Add(per);
            }
            await _db.Practices.AddRangeAsync(list);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
