using Application.Abstractions;
using Application.Posts.Commands;
using DataAccess.Repositories;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using MinimalApi.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MinimalApi.OptionsSetup;
using Infrastructure.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DataAccess.DataAccessException.AuthenticationException;
using Microsoft.AspNetCore.Http;
using DataAccess.DataAccessException;
using Domain.Models;

namespace MinimalApi.Extensions
{
    public static class MinimalApiExtensions
    {
        public static void RegisterServices(this WebApplicationBuilder builder)
        {

            var cs = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<SocialDbContext>(opt => opt.UseSqlServer(cs));
            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<SocialDbContext>()
                .AddDefaultTokenProviders();
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreatePost).GetTypeInfo().Assembly));
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });
            RegisterRepositories(builder.Services);
            ConfigureAuthentication(builder.Services, builder);
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<IClassInSchoolRepository, ClassInSchoolRepository>();
            services.AddScoped<ICurrentGradingSystemRepository, CurrentGradingSystemRepository>();
            services.AddScoped<IAdminSettingRepository, AdminSettingRepository>();
            services.AddScoped<ISchoolSubjectsRepository, SchoolSubjectsRepository>();
            services.AddScoped<IJuniorSchoolSubjectRepository, JuniorSchoolSubjectRepository>();
            services.AddScoped<ISeniorSchoolSubjectRepository, SeniorSchoolSubjectRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IActiveSchoolTermRepository, ActiveSchoolTermRepository>();
            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
            services.AddScoped<IJwtProvider, JwtProvider>();
        }

        private static void ConfigureAuthentication(IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
               .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, o =>
               {
                   o.TokenValidationParameters = new()
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       ValidIssuer = builder.Configuration["Jwt:Issuer"],
                       ValidAudience = builder.Configuration["Jwt:Audience"],
                       IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!))
                   };
               });
            services.ConfigureOptions<JwtOptionSetup>();
            services.ConfigureOptions<JwtBearerOptionSetup>();
            
            services.AddAuthorization();
        }
        public static void RegisterEndPointDefinitions(this WebApplication app)
        {
            var endPointDefinitions = typeof(Program).Assembly.GetTypes()
                .Where(t => t.IsAssignableTo(typeof(IEndpointDefinition)) && !t.IsAbstract && !t.IsInterface)
                .Select(Activator.CreateInstance)
                .Cast<IEndpointDefinition>();

            foreach(var endpointDef in endPointDefinitions)
            {
                endpointDef.RegisterEndpoints(app);
            }
        }

        public static void RegisterExecption(this WebApplication app)
        {
            app.Use(async (ctx, next) =>
            {
                try
                {
                    await next();
                }
                catch (DataAccessException e)
                {
                    var errorResponse = new ErrorResponse()
                    {
                        StatusCode = 400,
                        StatusPharase = "Bad request",
                        TimeStamp = DateTime.Now
                    };
                    errorResponse.Errors.Add(e.Message);
                    ctx.Response.StatusCode = 400;
                    await ctx.Response.WriteAsJsonAsync(errorResponse);
                }
                catch (Exception)
                {
                    ctx.Response.StatusCode = 500;
                    await ctx.Response.WriteAsync("An error ocurred");
                }
            });
        }


    }
}
