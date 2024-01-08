using Application.Abstractions;
using Application.Posts.Commands;
using DataAccess.Repositories;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Authentication;
using DataAccess.DatabaseSeeder;

namespace AdminBlazor
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
            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<ISubjectTeachingRepository, SubjectTeachingRepository>();
            services.AddTransient<ClassSeeder>();
        }

        private static void ConfigureAuthentication(IServiceCollection services, WebApplicationBuilder builder)
        {

            services.AddAuthorization();
        }
    }
}
