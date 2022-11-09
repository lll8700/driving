using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Yun.Share.Voice.DataBase;
using Yun.Share.Voice.IApplication;
using Yun.Share.Voice.IApplication.Dtos.Models;
using Yun.Share.Voice.IApplication.Input;
using Yun.Share.Voice.IApplication.UtilDtos;
using Yun.Share.Voice.Models.Entities;
using Yun.Share.Voice.Utils;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Authorization;

namespace Yun.Share.Voice.Application.Serve
{
    public class PracticeServer : BaseVoiceServer<Practice, PracticeDto, PracticeListInput, PracticeDto>, IPracticeServer
    {
        private readonly CoreDbContext _db;
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _cache;
        private readonly IPracticeServer _practiceServer;
        public PracticeServer(CoreDbContext db, IMemoryCache cache, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
            _cache = cache;
            base.query = db.Practices.AsQueryable();
        }
        public override async Task<PracticeDto> CreateAsync(PracticeDto input)
        {
            Practice per = null;
            if (input.Id.HasValue)
                return await UpdateAsync(input);
            else
            {
                per = input.MapTo<Practice, PracticeDto>();
                await _db.Practices.AddAsync(per);
                await _db.SaveChangesAsync();
                return per.MapTo<PracticeDto, Practice>();
            }
         
        }

        public override async Task<PracticeDto> GetAsync(Guid Id)
        {
            Practice per = await _db.Practices.FindAsync(Id);
            return per.MapTo<PracticeDto, Practice>();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _db.Practices.FindAsync(id);
            _db.Practices.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<PracticeDto> GetNextAsync(PracticeListInput input)
        {
            var iq = NextFilter(input);

            Practice perItem = await iq.OrderByDescending(x=>x.CreationTime).FirstOrDefaultAsync();
            
            return perItem == null? new PracticeDto() : await GetMapDto(perItem);
        }

        /// <summary>
        /// 获取考试100题
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<PracticeDto>> GetTestListAsync(PracticeTestListInput input)
        {

            var list = new List<PracticeDto>();

            var baseList = await GetCachePracticeDtos();

            if (input.ChoiceCount > 0)
            {
                var iqList = baseList.Where(x => x.ChoiceTyope == Enum.ChoiceTyope.Choice).OrderBy(x => Guid.NewGuid()).Take(input.ChoiceCount).ToList();
                list.AddRange(iqList);
            }
            if (input.MoreCount > 0)
            {
                var iqList = baseList.Where(x => x.ChoiceTyope == Enum.ChoiceTyope.More).OrderBy(x => Guid.NewGuid()).Take(input.MoreCount).ToList();
                list.AddRange(iqList);
            }
            if (input.SingleCount > 0)
            {
                var iqList = baseList.Where(x => x.ChoiceTyope == Enum.ChoiceTyope.Single).OrderBy(x => Guid.NewGuid()).Take(input.SingleCount).ToList();
                list.AddRange(iqList);
            }
            if (input.UnChoiceCount > 0)
            {
                var iqList = baseList.Where(x => x.ChoiceTyope == Enum.ChoiceTyope.Single || x.ChoiceTyope == Enum.ChoiceTyope.More).OrderBy(x => Guid.NewGuid()).Take(input.UnChoiceCount).ToList();
                list.AddRange(iqList);
            }

            return new PagedResultDto<PracticeDto>(list.Count, list);

        }


        public async Task<PracticeDto> GetRandomAsync(PracticeListInput input)
        {
            var baseList = await GetCachePracticeDtos();
            var perItem = baseList.OrderBy(x=> Guid.NewGuid()).FirstOrDefault();
            return perItem == null ? new PracticeDto() : perItem;
        }

        public override async Task<PracticeDto> UpdateAsync(PracticeDto input)
        {

            Practice per = await _db.Practices.FindAsync(input.Id.Value);

            var opList = await _db.Optiones.Where(x => x.PracticeId == input.Id.Value).ToArrayAsync();
            for (var i = 0; i < opList.Count(); i++)
            {
                _db.Optiones.Remove(opList[i]);
            }

            var images = await _db.PracticeImages.Where(x => x.PracticeId == input.Id.Value).ToArrayAsync();
            for (var i = 0; i < images.Count(); i++)
            {
                _db.PracticeImages.Remove(images[i]);
            }

            if(input.Options.IsNotEmpty())
            {
                List<Option> ops = new List<Option>();
                for (var j = 0; j < input.Options.Count; j++)
                {
                    var opData = input.Options[j];
                    Option op = new Option()
                    {
                        PracticeId = input.Id.Value,
                        Index = opData.Index,
                        Title = opData.Title,
                        IsCorrect = opData.IsCorrect
                    };
                    ops.Add(op);
                    await _db.Optiones.AddAsync(op);
                }
                per.Options = ops;
            }
            if (input.ImageSrc.IsNotEmpty())
            {

                var sp = input.ImageSrc.Split(new char[] { ',', '，' });
                List<PracticeImage> practiceImages = new List<PracticeImage>();
                foreach (var im in sp)
                {
                    if (im.IsNotEmpty())
                    {
                        PracticeImage image = new PracticeImage
                        {
                            PracticeId = input.Id.Value,
                            Url = im
                        };
                        practiceImages.Add(image);
                        await _db.PracticeImages.AddAsync(image);
                    }
                }
                per.PracticeImages = practiceImages;
            }

            per.Title = input.Title;
            per.SubjectTypeId = input.SubjectTypeId;
            per.CarTypeId = input.CarTypeId;
            per.ChoiceTyope = input.ChoiceTyope;
            per.Skill = input.Skill;
            per.SkillLast = input.SkillLast;
            per.Introduce = input.Introduce;


            await _db.SaveChangesAsync();
            return new PracticeDto();
        }

        public override async Task<PagedResultDto<PracticeDto>> GetListAsync(PracticeListInput input)
        {
            return await base.GetListAsync(input);
        }

        public async Task<int> GetSucceedCount()
        {
            var iq = _db.Practices.AsQueryable();
            iq = IncludeDefault(iq);

            iq = iq.Where(x => x.StatusTypeEnum == Enum.StatusTypeEnum.Succeed);

            return await iq.CountAsync();
        }
        /// <summary>
        /// 初始化外键
        /// </summary>
        /// <param name="iq"></param>
        /// <returns></returns>
        private IQueryable<Practice> IncludeDefault(IQueryable<Practice> iq)
        {
            return iq
                //.Include(x => x.PracticeImages)
                //.Include(x => x.CarType)
                //.Include(x => x.Options)
                //.Include(x => x.SubjectType);
                ;
            
        }
        /// <summary>
        /// 答题的筛选
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private IQueryable<Practice> NextFilter(PracticeListInput input)
        {

            var iq = _db.Practices.AsQueryable();
            iq = IncludeDefault(iq);
            input.StatusTypeEnum = Enum.StatusTypeEnum.Succeed;
            if (!input.Ids.IsNotEmpty())
            {
                input.Ids = new List<Guid>();
            }
            if (input.CarTypeId.HasValue)
            {
                iq = iq.Where(x => x.CarTypeId == input.CarTypeId);
            }
            if (input.SubjectTypeId.HasValue)
            {
                iq = iq.Where(x => x.SubjectTypeId == input.SubjectTypeId);
            }
            if (input.StatusTypeEnum.HasValue)
            {
                iq = iq.Where(x => x.StatusTypeEnum == input.StatusTypeEnum);
            }
            return iq.Where(x => !input.Ids.Contains(x.Id));
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
            if (input.ChoiceTyope.HasValue)
            {
                iq = iq.Where(x => x.ChoiceTyope == input.ChoiceTyope);
            }
            if (input.UnIds.IsNotEmpty())
            {
                iq = iq.Where(x => !input.UnIds.Contains(x.Id));
            }
            return iq;
        }

        protected override async Task<List<PracticeDto>> MapToGetListOutputDtosAsync(List<Practice> entities)
        {
            if(entities.Count == 0)
            {
                return new List<PracticeDto>();
            }

            var list = entities.Select(x => x.MapTo<PracticeDto, Practice>()).ToList();

            var carIds = list.Select(x => x.CarTypeId).ToList();

            var subIds = list.Select(x => x.SubjectTypeId).ToList();

            var carList = await _db.CarTypes.Where(x => carIds.Contains(x.Id)).ToListAsync();
            var subLIst = await _db.SubjectTypes.Where(x => subIds.Contains(x.Id)).ToListAsync();

            var ids = list.Select(x => x.Id).ToList();

            var imges = await _db.PracticeImages.Where(x => ids.Contains(x.PracticeId)).ToListAsync();

            var options = await _db.Optiones.Where(x => ids.Contains(x.PracticeId)).ToListAsync();

            for(var i=0;i<list.Count;i++)
            {
                var x = list[i];
                var car = carList.FirstOrDefault(s => s.Id == x.CarTypeId);
                if (car != null)
                {
                    x.CarType = car.MapTo<CarTypeDto, CarType>();
                }
                var sub = subLIst.FirstOrDefault(s => s.Id == x.SubjectTypeId);
                if (sub != null)
                {
                    x.SubjectType = sub.MapTo<SubjectTypeDto, SubjectType>();
                }

                var ims = imges.Where(s => s.PracticeId == x.Id).ToList();

                var ops = options.Where(s => s.PracticeId == x.Id).OrderBy(f => f.Index).ToList();

                x.PracticeImages = ims.Select(f => f.MapTo<PracticeImageDto, PracticeImage>()).ToList();

                x.Options = ops.Select(f => f.MapTo<OptionDto, Option>()).ToList();
            }


            return list;
        }

        private async Task<PracticeDto> GetMapDto(Practice per)
        {
            var list = await MapToGetListOutputDtosAsync(new List<Practice> { per });
            return list[0];
        }
        public async Task<int> UploadExcel(PracticeFileInput input)
        {
            List<Practice> list = new List<Practice>();
            for (int i = 0; i < input.ResultsLists.Count; i++)
            {
                var item = input.ResultsLists[i];
                Practice per = new Practice()
                {
                    CarTypeId = input.CarTypeId,
                    SubjectTypeId = input.SubjectTypeId,
                    ChoiceTyope = item.TypeName.Contains("判断") ? Enum.ChoiceTyope.Choice : item.TypeName.Contains("单选") ? Enum.ChoiceTyope.Single : Enum.ChoiceTyope.More,
                    Title = item.Title,
                    Skill = item.Skill,
                    SkillLast = item.SkillLast,
                    Introduce = item.Introduce,
                    StatusTypeEnum = Enum.StatusTypeEnum.Succeed,
                };
                if (item.Images.IsNotEmpty())
                {
                    var sp = item.Images.Split(new char[] { ',', '，' });
                    List<PracticeImage> listImage = new List<PracticeImage>();
                    foreach (var im in sp)
                    {
                        if (im.IsNotEmpty())
                        {
                            listImage.Add(new PracticeImage
                            {
                                Url = im
                            });
                        }
                    }
                    per.PracticeImages = listImage;
                }
                List<Option> ops = new List<Option>();

                for (var j = 0; j < item.Options.Count; j++)
                {
                    var opData = item.Options[j];
                    Option op = new Option()
                    {
                        Index = opData.Index,
                        Title = opData.Title,
                        IsCorrect = opData.IsCorrect
                    }; 
                    ops.Add(op);
                }
                per.Options = ops; 
                list.Add(per);
                
            }
            await _db.Practices.AddRangeAsync(list);
            await _db.SaveChangesAsync();
            return list.Count;
        }
        public async Task<int> UploadFilePath(IFormFile file)
        {
            ExcelUtil excelUtil = new ExcelUtil();
            var dt =  excelUtil.ExcelToTable(file);
            List<Practice> list = new List<Practice>();
            var car = await _db.CarTypes.FirstOrDefaultAsync();
            var su = await _db.SubjectTypes.FirstOrDefaultAsync();
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
            //await _db.Practices.AddRangeAsync(list);
            //await _db.SaveChangesAsync();
            return list.Count;
        }

        public  int UploadImageZip(IFormFile file)
        {
            using (var stream = file.OpenReadStream())
            {
                var appPath = _configuration["Authentication:Oss:ImagePath"];
                //解压缩到指定文件夹
                string target = Directory.GetCurrentDirectory() + "/" + appPath;
                var srcArchive = new ZipArchive(stream, ZipArchiveMode.Read);
                var list = srcArchive.Entries.ToList();
                list.ForEach((entry) =>
                            {
                                // 分每个文件进行保存
                                using (Stream srcEntry = entry.Open())
                                {
                                    //文件保存
                                    using (var fs = System.IO.File.Create(target + entry.FullName))
                                    {
                                        srcEntry.CopyTo(fs);
                                        fs.Flush();
                                    }
                                }
                            });
                return list.Count;
            }
        }

        public async Task<List<PracticeDto>> GetCachePracticeDtos()
        {
            var str = _configuration["Cache:Practice:Succeed"];

            var list = _cache.Get(str);

            if (list == null)
            {
                var items = await GetInitList();
                _cache.Set(str, items);
            }
            else
            {
                var succeedCount = await GetSucceedCount();

                var dtoList = (List<PracticeDto>)list;

                if (dtoList.Count != succeedCount)
                {
                    var items = await GetInitList();
                    _cache.Set(str, items);
                }
            }

            var baseList = _cache.Get(str);

            var dtos = (List<PracticeDto>)list;

            return dtos;
        }

        private async Task<List<PracticeDto>> GetInitList()
        {
            var cacheCount = Convert.ToInt32(_configuration["Cache:Practice:CacheCount"]);
            PracticeListInput input = new PracticeListInput
            {
                StatusTypeEnum = Enum.StatusTypeEnum.Succeed,
                MaxResultCount = cacheCount,  // 默认缓存一万条数据
            };
            var dtolist = await GetListAsync(input);
            return dtolist.Items;
        }

        #region  导出Excel

        /// <summary>
        /// 导出题库
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ExcelData> GetCommissionFormListExcel(PracticeListInput input)
        {

            input.SkipCount = 0;
            input.MaxResultCount = int.MaxValue;
            var fromData = await GetListAsync(input);
            var tableName = "题库数据";
            var fileName = $"{tableName}.xlsx";
            ExcelUtil excel = new ExcelUtil();
            List<string> titles = new List<string>
            {
                "序号",
                "题型",
                "图片",
                "题目",
                "选项一",
                "选项二",
                "选项三",
                "选项四",
                "正确答案",
                "答题技巧一",
                "答题技巧二",
                "题目解析"
            };
            List<List<string>> list = new List<List<string>>();
            var index = 1;
            foreach(var data in fromData.Items)
            {
                List<string> items = new List<string>();
                items.Add(index.ToString());
                items.Add(data.ChoiceTyopeEnmName);
                var images = string.Join(',', data.PracticeImages.Select(x => x.Url));
                items.Add(images);
                items.Add(data.Title);
                var ops = data.Options.OrderBy(x => x.Index).ToList();
                var isOps = ops.Where(x => x.IsCorrect).Select(x=>x.Title.Trim().Substring(0,1)).ToList();
                foreach(var o in ops)
                {
                    items.Add(o.Title);
                }
                for(var i = ops.Count ; i < 4; i++)
                {
                    items.Add("");
                }
                items.Add(string.Join(',', isOps));
                items.Add(data.Skill);
                items.Add(data.SkillLast);
                items.Add(data.Introduce);
                list.Add(items);
                index++;
            }
          
            var dt = excel.ToDataTable(titles,list);
            var excelPath = _configuration["Authentication:Oss:OutExcelPath"];
            string target = Directory.GetCurrentDirectory() + "/" + excelPath;
            var isOut = excel.TableToExcel(dt, target + fileName);
            ExcelData excelData = new ExcelData
            {
                Status = false,
                Url = null,
            };
            if(isOut)
            {
                excelData.Status = true;
                excelData.Url = excelPath + fileName;
            }

            //以字符流的形式下载文件
            return excelData;
        }
        #endregion

    }
}
