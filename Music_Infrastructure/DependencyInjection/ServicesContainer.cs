using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Music_Infrastructure.Data;
using Music_Domain.Entities;
using Music_Application.Interfaces;
using Music_Infrastructure.Services;


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


            // Đăng ký Identity (dùng AddIdentityCore cho API minimal)
            services.AddIdentityCore<Users>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false; // Không bắt email xác nhận
            })
            .AddRoles<IdentityRole>()                          // Role support
            .AddRoleManager<RoleManager<IdentityRole>>()       // RoleManager
            .AddUserManager<UserManager<Users>>()             // UserManager
            .AddEntityFrameworkStores<ApplicationDbContext>() ;// EF Core DbContext

            services.AddScoped<IAuthentication , AuthenticationService>() ;
            return services;
        }
    }
}
