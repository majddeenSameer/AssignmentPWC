using Domain.DomainModels;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var filePath = Path.Combine(System.AppContext.BaseDirectory, "Application.xml");
            services.AddMatchingInterfaces();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddContext();

            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(UnitOfWorkActionFilter));
                options.CacheProfiles.Add("Default", new Microsoft.AspNetCore.Mvc.CacheProfile()
                {
                    Duration = (Convert.ToInt32(Configuration["Cache:DefaultResponseCacheInMinutes"]) * 60)//Minutes to Seconds conversion
                });
            })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Unspecified;
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.DateFormatString = Configuration["DateFormatString"];
                });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Application", Version = "v1" });
                c.SwaggerDoc("integration", new OpenApiInfo { Title = "integration", Version = "v1" });

                c.IncludeXmlComments(filePath);
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert the JWT with Bearer into field \n Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJlZTc3OWZlYy1hZDNlLTQyY2UtOTIyOC0zMTM2NDQ4MzA3MjYiLCJ1bmlxdWVfbmFtZSI6ImFkbWluIiwiVXNlck5hbWUiOiJhZG1pbiIsIlVzZXJJZCI6ImVlNzc5ZmVjLWFkM2UtNDJjZS05MjI4LTMxMzY0NDgzMDcyNiIsImVtYWlsIjoiYWRtaW5AYWRtaW4uY29tIiwiTmFtZUFyIjoiIiwiTmFtZUVuIjoiIiwicm9sZSI6IkFkbWluIiwibmJmIjoxNjE0MTkyMDUzLCJleHAiOjE2NzcyNjQwNTMsImlhdCI6MTYxNDE5MjA1MywiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo2MTA1OS8iLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjYxMDU5LyJ9.GGeANP9EhH68WCyT1dh9I93T1VG51Bj22XKFyU6Anoc",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
            });

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            // Configure JWT
            var jwtSection = Configuration.GetSection(nameof(JwtBearerTokenSettings));
            services.Configure<JwtBearerTokenSettings>(jwtSection);
            var jwtBearerTokenSettings = jwtSection.Get<JwtBearerTokenSettings>();
            var key = Encoding.ASCII.GetBytes(jwtBearerTokenSettings.SecretKey);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = jwtBearerTokenSettings.Issuer,
                    ValidAudience = jwtBearerTokenSettings.Audience,
                    ValidateIssuerSigningKey = false,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateLifetime = false
                };
            });

            services.ConfiguerMapster();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env
            //,
            //                UserManager<User> userManager,
            //                RoleManager<Role> roleManager,
            //                IAsyncRepository<Domain.DomainModels.Application> applicationRepository,
            //                IAsyncRepository<UserApplication> userApplicationRepository
            )
        {
            //AppIdentityDbContextSeed.SeedAsync(userManager, roleManager, applicationRepository, userApplicationRepository).Wait();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(Configuration["baseUrl"] + "/swagger/v1/swagger.json", "Application v1");
                c.SwaggerEndpoint(Configuration["baseUrl"] + "/swagger/integration/swagger.json", "Integration");
            });
            app.UseLocalization();
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.HandleExceptions();
            });

            app.UseCors("MyPolicy");

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
