using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Yun.Share.Voice.DataBase;
using Yun.Share.Voice.IApplication.Input;
using Yun.Share.Voice.IApplication.UtilDtos;

namespace Yun.Share.Voice.Application
{
    public abstract class BaseVoiceServer<TEntity, TDto, TSearchList, TCreateUpdateDto>
    {
        public IQueryable<TEntity> query;


        public BaseVoiceServer()
        {
        }

        #region 查询

        public virtual async Task<PagedResultDto<TDto>> GetListAsync(TSearchList input)
        {
            query = Filter(query, input);
            var totalCount = await query.CountAsync();
            if(input is TSearchDto)
            {
                var ser = input as TSearchDto;
                query = ApplyMySorting(query, ser);
                query = ApplyMyPaging(query, ser);
            }

            var entities = await query.ToListAsync();
            var entityDtos = await MapToGetListOutputDtosAsync(entities);

            return new PagedResultDto<TDto>(
                totalCount,
                entityDtos
            );
        }
        public abstract Task<TDto> GetAsync(Guid Id);
        /// <summary>
        /// 过滤数据
        /// </summary>
        /// <param name="iq"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        protected abstract IQueryable<TEntity> Filter(IQueryable<TEntity> iq, TSearchList input);

        protected virtual IQueryable<TSomeEntity> ApplyMySorting<TSomeEntity>(IQueryable<TSomeEntity> query, TSearchDto input)
            where TSomeEntity : TEntity
        {
            if (string.IsNullOrEmpty(input.Sorting))
            {
                input.Sorting = "CreationTime DESC";
                return query;
            }
            return query.OrderBy(input.Sorting);
        }

        protected virtual IQueryable<TSomeEntity> ApplyMyPaging<TSomeEntity>(IQueryable<TSomeEntity> query, TSearchDto input)
            where TSomeEntity : TEntity
        {


            input.MaxResultCount = input.MaxResultCount.HasValue ? input.MaxResultCount : (input.Limit.HasValue ? input.Limit : 10);

            input.SkipCount = input.SkipCount.HasValue ? input.SkipCount : (input.Page.HasValue ? (input.Page - 1) * input.MaxResultCount : 0);

            return query.Skip(input.SkipCount.Value ).Take(input.MaxResultCount.Value);
        }
        protected abstract Task<List<TDto>> MapToGetListOutputDtosAsync(List<TEntity> entities);

        #endregion
        #region 添加
        public abstract Task<TDto> UpdateAsync(TCreateUpdateDto input);

        public abstract Task<TDto> CreateAsync(TCreateUpdateDto input);

      
        #endregion
    }
}
