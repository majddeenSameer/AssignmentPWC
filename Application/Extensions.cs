using DotNetCore.IoC;
using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Domain;
using Domain.DomainModels;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Lookups;
using Domain.Services;
using DTO;
using Infrastructure.Database;
using Infrastructure.Repositories;

namespace Application
{
    public static class Extensions
    {
        public static void AddContext(this IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();

            var connectionString = configuration.GetConnectionString(nameof(Context));

            services.AddDbContext<Context>(x => x.UseLazyLoadingProxies().UseSqlServer(connectionString));
            // Configure Identity
           // services.AddDbContext<IdentityContext>(options =>
           //    options.UseSqlServer(configuration.GetConnectionString(nameof(IdentityContext))));
           // services.AddIdentityCore<User>().AddRoles<Role>()
           //.AddEntityFrameworkStores<IdentityContext>();
        }

        public static void AddMatchingInterfaces(this IServiceCollection services)
        {
            //services.AddClassesInterfaces(typeof(IdentityContext).Assembly, typeof(EntityBase).Assembly);
            services.AddClassesInterfaces(typeof(Context).Assembly, typeof(EntityBase).Assembly);
            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            services.AddScoped(typeof(IReadAsyncRepository<>), typeof(RepositoryBase<>));
            services.AddScoped(typeof(IDomainService<>), typeof(GenericServiceBase<>));
        }

        public static void ConfiguerMapster(this IServiceCollection services)
        {
            TypeAdapterConfig.GlobalSettings.Default.PreserveReference(true);
            TypeAdapterConfig.GlobalSettings.Default.MapToConstructor(true);
            TypeAdapterConfig.GlobalSettings.Default.IgnoreMember((member, side) => side == MemberSide.Destination && member.SetterModifier == AccessModifier.Private);
        }

        public static IApplicationBuilder UseLocalization(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LocalizationMiddleware>();
        }

        public static void HandleExceptions(this IApplicationBuilder app)
        {
            app.Run(async context =>
            {

                context.Response.ContentType = "application/json";

                var exceptionHandlerPathFeature =
                    context.Features.Get<IExceptionHandlerPathFeature>();

                if (exceptionHandlerPathFeature?.Error is BusinessException)
                {
                    var businessException = exceptionHandlerPathFeature?.Error as BusinessException;

                    context.Response.StatusCode = 422;
                    await context.Response.WriteAsync(businessException.Message);
                }
                else
                {
                    context.Response.StatusCode = 500;
                    if (!string.IsNullOrEmpty(exceptionHandlerPathFeature?.Error?.Message))
                        await context.Response.WriteAsync(exceptionHandlerPathFeature.Error.Message);
                    else
                        await context.Response.WriteAsync("Oops! Something went wrong!");

                    Log.Error(exceptionHandlerPathFeature?.Error, "Error Handling Middleware - Unhandled Exception");

                }

            });
        }
    }
}
