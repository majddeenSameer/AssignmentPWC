using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Domain;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.SeedingData;
using Domain.DomainModels;

namespace Infrastructure.Database
{
    public sealed class Context : DbContext
    {
        private readonly IIdentityService _identityService;

        public Context(DbContextOptions<Context> options, IIdentityService identityService) : base(options)
        {
            _identityService = identityService;
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            var domainTypes = Assembly.GetAssembly(typeof(EntityBase)).GetTypes()
            .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(EntityBase))).ToList();

            builder.ApplyConfigurationsFromAssembly(typeof(ContextFactory).Assembly);
            builder.ApplyConvention(domainTypes);
            base.OnModelCreating(builder);
           
            foreach (var entityType in builder.Model.GetEntityTypes().Where(e => e.ClrType.BaseType == typeof(AuditableEntityBase)))
            {
                if (typeof(ISoftDeletable).IsAssignableFrom(entityType.ClrType))
                {
                    entityType.AddSoftDeleteQueryFilter();
                }
            }

            builder.Seed();
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplyAudit();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void ApplyAudit()
        {
            foreach (var entity in ChangeTracker.Entries())
            {
                if (entity.Entity is IAuditable auditableEntity)
                {
                    switch (entity.State)
                    {
                        case EntityState.Added:
                            if (string.IsNullOrEmpty(auditableEntity.CreatedBy))
                                auditableEntity.CreatedBy = _identityService.GetAuthorizationDetails()?.Username;

                            if (auditableEntity.CreatedDate == null || auditableEntity.CreatedDate == DateTime.MinValue)
                                auditableEntity.CreatedDate = DateTime.Now;
                            break;

                        case EntityState.Modified:
                            entity.State = EntityState.Modified;
                            if (string.IsNullOrEmpty(auditableEntity.ModifiedBy))
                                auditableEntity.ModifiedBy = _identityService.GetAuthorizationDetails()?.Username;

                            if (auditableEntity.ModifiedDate == null || auditableEntity.CreatedDate == DateTime.MinValue)
                                auditableEntity.ModifiedDate = DateTime.Now;
                            break;

                    }
                }
                if (entity.Entity is ISoftDeletable deletableEntity)
                {
                    if (entity.State == EntityState.Deleted)
                    {
                        entity.State = EntityState.Modified;
                        deletableEntity.IsDeleted = true;
                    }
                }
            }

        }
    }
}

public static class SoftDeleteQueryExtension
{
    public static void AddSoftDeleteQueryFilter(
        this IMutableEntityType entityData)
    {
        var methodToCall = typeof(SoftDeleteQueryExtension)
            .GetMethod(nameof(GetSoftDeleteFilter),
                BindingFlags.NonPublic | BindingFlags.Static)
            .MakeGenericMethod(entityData.ClrType);
        var filter = methodToCall.Invoke(null, new object[] { });
        entityData.SetQueryFilter((LambdaExpression)filter);
    }

    private static LambdaExpression GetSoftDeleteFilter<TEntity>()
        where TEntity : class, ISoftDeletable
    {
        Expression<Func<TEntity, bool>> filter = x => !x.IsDeleted;
        return filter;
    }
}

public static class ModelBuilderExtensions
{
    public static void ApplyConvention(this ModelBuilder modelBuilder, List<Type> types)
    {
        foreach (var type in types)
        {
            if (!type.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)))
            {
                if (type.GetInterfaces().Any())
                {
                }
                modelBuilder.Entity(type);
            }
        }
    }

    public static void View<TEntity>(this ModelBuilder modelBuilder, string viewName)
        where TEntity : class
    {
    }
}

