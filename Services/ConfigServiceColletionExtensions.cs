using aspcore_watchshop.Interfaces;
using aspcore_watchshop.Models;
using aspcore_watchshop.Models.ExtendModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace aspcore_watchshop.Services {
    public static class ConfigServiceColletionExtensions {
        public static IServiceCollection AddModels (
            this IServiceCollection services, IConfiguration config) {
            //Model
            services.AddScoped<ICategoryModel, CategoryModel> ();
            services.AddScoped<IWireModel, WireModel> ();
            services.AddScoped<IBandModel, BandModel> ();
            services.AddScoped<IPostModel, PostModel> ();
            services.AddScoped<IPolicyModel, PolicyModel> ();
            services.AddScoped<IInfoModel, InfoModel> ();
            services.AddScoped<IFeeModel, FeeModel> ();
            //
            services.AddScoped<IOrderModel, OrderModel> ();
            services.AddScoped<IOrderDetailModel, OrderDetailModel> ();
            //
            services.AddScoped<IPromModel, PromModel> ();
            services.AddScoped<IPromBillModel, PromBillModel> ();
            services.AddScoped<IPromProductModel, PromProductModel> ();
            //
            services.AddScoped<IProductModel, ProductModel> ();
            services.AddScoped<IProdDetailModel, ProdDetailModel> ();
            return services;
        }
    }
}