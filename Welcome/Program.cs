using AutoMapper;
using Demo.BLL;
using Demo.BLL.Interfaces;
using Demo.BLL.Repositry;
using Demo.DAl.Context;
using Demo.DAl.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Welcome.Mapping_Profils;
using Welcome.MappingProfil;

namespace Welcome
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<WelcomeContext>(options =>
              options.UseSqlServer(builder.Configuration.GetConnectionString("cs"))
          );
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(Option => {
                Option.Password.RequireNonAlphanumeric = true;
                Option.Password.RequireLowercase = true;
                Option.Password.RequireUppercase = true;
                Option.Password.RequireDigit = true;
            }).AddEntityFrameworkStores<WelcomeContext>().AddDefaultTokenProviders();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(Option =>
                {
                    Option.LoginPath = "Account/Login";
                    Option.AccessDeniedPath = "Home/Error";

                });


            builder.Services.AddAutoMapper(M=>M.AddProfile(new EmployeeProfile()));
            builder.Services.AddAutoMapper(M => M.AddProfile(new UserProfile()));
            builder.Services.AddAutoMapper(M => M.AddProfile(new RoleProfile()));




            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}