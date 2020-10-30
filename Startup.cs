using aspcore_watchshop.EF;
using aspcore_watchshop.Hepler;
using aspcore_watchshop.MiddleWares;
using aspcore_watchshop.Services;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace aspcore_watchshop {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices (IServiceCollection services) {
            services.AddOptions ();
            //Mail Service
            var mailSetting = Configuration.GetSection ("MailSettings");
            services.Configure<MailSettings> (mailSetting);
            services.AddTransient<IEmailSender, EmailSender> ();
            //
            services.AddControllersWithViews ();
            services.AddAutoMapper (typeof (MapperProfile).Assembly);
            services.AddMemoryCache ();
            services.AddDbContext<watchContext> (options =>
                options.UseSqlServer (Configuration.GetConnectionString ("default")));
            // Add Model Service
            services.AddModels (this.Configuration);
            // Identity
            services.AddConfigIdentity ();
            services.AddRazorPages ();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
            app.UseMiddleware<InitLayoutData> ();

            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else {
                app.UseExceptionHandler ("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts ();
            }
            app.UseStatusCodePagesWithReExecute ("/Home/Error", "?statusCode={0}");

            app.UseHttpsRedirection ();

            app.UseStaticFiles ();

            app.UseRouting ();

            app.UseAuthentication ();

            app.UseAuthorization ();

            app.UseEndpoints (endpoints => {
                endpoints.MapControllerRoute (
                    name: "admin",
                    pattern: "/Admin/{controller}/{action}",
                    defaults : new { area = "Admin", controller = "Home", action = "Index" });
                endpoints.MapControllerRoute (
                    name: "trang-chu",
                    pattern: "/",
                    defaults : new { controller = "Home", action = "Index" });
                endpoints.MapControllerRoute (
                    name: "error",
                    pattern: "/error",
                    defaults : new { controller = "Home", action = "Error" });
                //
                endpoints.MapControllerRoute (
                    name: "san-pham",
                    pattern: "/san-pham",
                    defaults : new { controller = "Product", action = "Detail" });
                endpoints.MapControllerRoute (
                    name: "san-pham",
                    pattern: "/{action}",
                    defaults : new { controller = "Product", action = "Discount" });
                //
                endpoints.MapControllerRoute (
                    name: "gio-hang",
                    pattern: "/gio-hang",
                    defaults : new { controller = "Cart", action = "Index" });
                endpoints.MapControllerRoute (
                    name: "don-hang",
                    pattern: "/don-hang/{action}",
                    defaults : new { controller = "Cart" });
                //
                endpoints.MapControllerRoute (
                    name: "all",
                    pattern: "/{controller}/{action}");
                endpoints.MapRazorPages ();
            });

        }
    }
}