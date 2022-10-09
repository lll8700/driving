
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Yun.Share.Voice.IApplication;
using Yun.Share.Voice.Models.Base;
using Yun.Share.Voice.Models.Entities;

namespace Yun.Share.Voice.DataBase
{
    public class CoreDbContext : DbContext
    {
        private readonly IJwtTokenServer _jwtTokenServer;
        public CoreDbContext(DbContextOptions<CoreDbContext> options, IJwtTokenServer jwtTokenServer) : base(options)
        {
            _jwtTokenServer = jwtTokenServer;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //打印sql日志
            optionsBuilder.LogTo((s) => { Console.WriteLine(s); }
                , LogLevel.Information);
        }
        #region 新增
        public virtual DbSet<CarType> CarTypes { get; set; }

        public virtual DbSet<ErrorPracticeLog> ErrorPracticeLoges { get; set; }

        public virtual DbSet<ErrorPracticeId> ErrorPracticeIds { get; set; }

        public virtual DbSet<Option> Optiones { get; set; }

        public virtual DbSet<Practice> Practices { get; set; }

        public virtual DbSet<PracticeImage> PracticeImages { get; set; }

        public virtual DbSet<SubjectType> SubjectTypes { get; set; }

        public virtual DbSet<User> Users { get; set; }

        #endregion

        #region 重写
        //  SaveChanges
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            UpdateSaveChangesData();

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        public override int SaveChanges()
        {
            UpdateSaveChangesData();

            return base.SaveChanges();
        }

        private void UpdateSaveChangesData()
        {
            //自动修改 CreateTime,UpdateTime
            var entityEntries = ChangeTracker.Entries().ToList();
            //获取当前用户信息

            Guid? userId = _jwtTokenServer.GetCurrentUserId();

            foreach (var entry in entityEntries)
            {
                if (entry.State == EntityState.Added)
                {
                    Entry(entry.Entity).Property(nameof(BaseModel.Id)).CurrentValue = Guid.NewGuid();
                    Entry(entry.Entity).Property(nameof(BaseModel.CreationTime)).CurrentValue = DateTime.Now;
                    Entry(entry.Entity).Property(nameof(BaseModel.CreatorId)).CurrentValue = userId;
                }
                if (entry.State == EntityState.Modified)
                {
                    Entry(entry.Entity).Property(nameof(BaseModel.LastModificationTime)).CurrentValue = DateTime.Now;
                    Entry(entry.Entity).Property(nameof(BaseModel.LastModifierId)).CurrentValue = userId;
                }
                if (entry.State == EntityState.Deleted)
                {
                    Entry(entry.Entity).Property(nameof(BaseModel.DeletedId)).CurrentValue = userId;
                    Entry(entry.Entity).Property(nameof(BaseModel.IsDeleted)).CurrentValue = true;
                    Entry(entry.Entity).Property(nameof(BaseModel.DeletedTime)).CurrentValue = DateTime.Now;
                    entry.State = EntityState.Modified;
                }
            }
        }
        public override EntityEntry Remove(object entity)
        {
            return base.Remove(entity);
        }
        /// <summary>
        /// 重新创建sql语句 自动全局过滤
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            IEnumerable<IMutableEntityType> entityTypes = modelBuilder.Model.GetEntityTypes();
            foreach (IMutableEntityType entityType in entityTypes)
            {
                InitGobalFilter(entityType, modelBuilder);
            }
        }
        /// <summary>
        /// 初始化全局的过滤,如软自带的
        /// </summary>
        /// <param name="entityType">过滤</param>
        /// <param name="modelBuilder">builder</param>
        private void InitGobalFilter(IMutableEntityType entityType, ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                return;
            }
            IEnumerable<IMutableProperty> props = entityType.GetProperties();
            if (props.Any(x => x.Name == "IsDeleted"))
            {
                ParameterExpression parameter = Expression.Parameter(entityType.ClrType, "e");
                bool defaultValue = false;
                BinaryExpression body = Expression.Equal(
                    Expression.Call(typeof(EF), nameof(EF.Property), new[] { typeof(bool) }, parameter, Expression.Constant("IsDeleted")),
               Expression.Constant(defaultValue));
                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(Expression.Lambda(body, parameter));
            }
        }
        #endregion
    }
}
