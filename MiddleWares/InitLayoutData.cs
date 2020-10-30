using System;
using System.Threading.Tasks;
using aspcore_watchshop.EF;
using aspcore_watchshop.Hepler;
using aspcore_watchshop.Interfaces;
using aspcore_watchshop.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;


namespace aspcore_watchshop.MiddleWares
{
    public class InitLayoutData
    {
        private readonly RequestDelegate _next;

        public InitLayoutData(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var services = context.RequestServices;
            try
            {
                var mapper = context.RequestServices.GetService<IMapper>();
                DataHelper.Instance.Mapper = mapper;
                var infoModel = services.GetService<IInfoModel>();
                var info = infoModel.GetVM(services.GetRequiredService<watchContext>(), 1);
                LayoutData.Instance.SetForInfo(info);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            await _next(context);
        }
    }
}