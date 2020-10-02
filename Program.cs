using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using aspcore_watchshop.Hepler;
using aspcore_watchshop.Models;
using aspcore_watchshop.EF;

namespace aspcore_watchshop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                try
                {
                    var mapper = services.GetRequiredService<IMapper>();
                    Helper.Mapper = mapper;
                    var infoModel = services.GetRequiredService<IInfoModel>();
                    LayoutData.SetForInfo(infoModel.GetInfo(services.GetRequiredService<watchContext>()));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
