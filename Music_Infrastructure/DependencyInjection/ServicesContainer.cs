using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Music_Infrastructure.Data;
using Music_Domain.Entities;
using Music_Application.Interfaces;
using Music_Infrastructure.Services;
using Music_Application.DTOs.Authentication;



namespace Music_Infrastructure.DependencyInjection
{
    public static class ServicesContainer
    {
        public static IServiceCollection MainService(this IServiceCollection services, IConfiguration config)
        {

            //Cau hinh DataBase 
            services.AddDbContext<ApplicationDbContext>(options =>
            {

                string? connection = config.GetConnectionString("Database");
                if (string.IsNullOrEmpty(connection))
                    throw new ArgumentException("Khong co ket noi");
                options.UseSqlServer(connection);
            });


            // // Đăng ký Identity (API minimal)
            // services.AddIdentityCore<Users>(options =>
            // {
            //     options.SignIn.RequireConfirmedEmail = true;
            // })
            //     .AddRoles<IdentityRole>()
            //     .AddRoleManager<RoleManager<IdentityRole>>()
            //     .AddUserManager<UserManager<Users>>()
            //     .AddEntityFrameworkStores<ApplicationDbContext>()
            //     .AddDefaultTokenProviders();
         
            //Dang ky service
            services.AddScoped<IAuthentication, AuthenticationService>();

            // Bind EmailSettings
            services.Configure<EmailSettings>(options => config.GetSection("EmailSettings").Bind(options));

            // Đăng ký EmailSender
            services.AddSingleton<EmailSender>();

            return services;
        }
    }
}
