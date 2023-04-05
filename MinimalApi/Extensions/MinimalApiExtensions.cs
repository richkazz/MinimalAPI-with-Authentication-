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
            builder.Services.AddScoped<IPostRepository, PostRepository>();
            builder.Services.AddScoped<IActiveSchoolTermRepository, ActiveSchoolTermRepository>();
            builder.Services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
            builder.Services.AddScoped<IJwtProvider, JwtProvider>();
            ConfigureAuthentication(builder.Services,builder);
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
                catch (AuthenticationException e)
                {
                    ctx.Response.StatusCode = 401;
                    await ctx.Response.WriteAsJsonAsync(new { error = "Invalid username or password" });
                }
                catch (ActiveSchoolTermException e)
                {
                    ctx.Response.StatusCode = 401;
                    await ctx.Response.WriteAsJsonAsync(new { error =e.Message });
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
