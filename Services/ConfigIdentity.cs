using System;
using aspcore_watchshop.EF;
using aspcore_watchshop.Interfaces;
using aspcore_watchshop.Models;
using aspcore_watchshop.Models.ExtendModels;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace aspcore_watchshop.Services {
    public static class ConfigIdentity {
        public static IServiceCollection AddConfigIdentity (
            this IServiceCollection services) {

            services.AddDefaultIdentity<IdentityUser> (options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<watchContext> ();

            services.Configure<IdentityOptions> (options => {
                // Password
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                // Lockout
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes (5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
                // User
                options.User.RequireUniqueEmail = true;
                // Confirn Logim
                options.SignIn.RequireConfirmedEmail = false;
            });
            return services;
        }
    }
}