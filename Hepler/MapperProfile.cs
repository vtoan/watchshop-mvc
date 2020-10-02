using aspcore_watchshop.Models;
using aspcore_watchshop.Entities;
using AutoMapper;
using System.Linq;

namespace aspcore_watchshop.Hepler
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // Product Detail
            CreateMap<ProductDetail, PropDetailVM>();
            CreateMap<PropDetailVM, ProductDetail>();
            // Product
            CreateMap<Product, ProductVM>()
                .ForMember(des => des.BandName, act => act.MapFrom(src => src.Band.Name))
                .ForMember(des => des.CategoryName, act => act.MapFrom(src => src.Category.Name))
                .ForMember(des => des.WireName, act => act.MapFrom(src => src.TypeWire.Name))
                .ForMember(des => des.Discount, act => act.Ignore());
            CreateMap<ProductVM, Product>()
                .ForMember(des => des.isDel, act => act.Ignore())
                .ForMember(des => des.isShow, act => act.Ignore())
                .ForMember(des => des.Band, act => act.Ignore())
                .ForMember(des => des.Category, act => act.Ignore())
                .ForMember(des => des.TypeWire, act => act.Ignore())
                .ForMember(des => des.ProductDetail, act => act.Ignore());
            // Promotion
            CreateMap<PromBill, PromBillVM>();
            CreateMap<PromBillVM, PromBill>();
            //
            CreateMap<Promotion, PromVM>();
            CreateMap<PromVM, Promotion>();
            //
            CreateMap<PromProduct, PromProductVM>()
                .ForMember(des => des.ProductIDs, act => act.MapFrom(src => ConvertStringToArray(src.ProductIDs)));
            CreateMap<PromProductVM, PromProduct>();
            // Cateogry
            CreateMap<Category, CategoryVM>();
            CreateMap<CategoryVM, Category>();
            // TypeWire
            CreateMap<TypeWire, WireVM>();
            CreateMap<WireVM, TypeWire>();
            //Fees
            CreateMap<Fee, FeeVM>();
            CreateMap<FeeVM, Fee>();
            //Order
            CreateMap<OrderVM, Order>()
                .ForMember(src => src.ID, opts => opts.Ignore());
            CreateMap<Order, OrderVM>();
            CreateMap<OrderDetailVM, OrderDetail>()
                .ForMember(src => src.ProductID, act => act.MapFrom(src => src.id));
            CreateMap<OrderDetail, OrderDetailVM>();

        }

        private int[] ConvertStringToArray(string str)
        {
            var arrInt = str.Split(',').Where(s => s != "").Select(int.Parse).ToArray();
            return arrInt;
        }

    }
}