using System.Linq;
using aspcore_watchshop.EF;
using aspcore_watchshop.Models;
using aspcore_watchshop.ViewModels;
using AutoMapper;

namespace aspcore_watchshop.Hepler {
    public class MapperProfile : Profile {
        public MapperProfile () {
            // Product Detail
            CreateMap<ProductDetail, ProdDetailVM> ();
            CreateMap<ProdDetailVM, ProductDetail> ();
            // Product
            CreateMap<Product, ProductVM> ()
                .ForMember (des => des.BandName, act => act.MapFrom (src => src.Band.Name))
                .ForMember (des => des.CategoryName, act => act.MapFrom (src => src.Category.Name))
                .ForMember (des => des.WireName, act => act.MapFrom (src => src.TypeWire.Name))
                .ForMember (des => des.Discount, act => act.Ignore ());
            CreateMap<ProductVM, Product> ()
                .ForMember (des => des.isDel, act => act.Ignore ())
                .ForMember (des => des.isShow, act => act.Ignore ())
                .ForMember (des => des.Band, act => act.Ignore ())
                .ForMember (des => des.Category, act => act.Ignore ())
                .ForMember (des => des.TypeWire, act => act.Ignore ())
                .ForMember (des => des.ProductDetail, act => act.Ignore ());
            // Promotion
            CreateMap<Promotion, PromVM> ();
            CreateMap<PromVM, Promotion> ();

            CreateMap<Promotion, PromProductVM> ()
                .ForMember (des => des.BandId, act => act.MapFrom (src => src.PromProduct.BandId))
                .ForMember (des => des.CategoryId, act => act.MapFrom (src => src.PromProduct.CategoryId))
                .ForMember (des => des.Discount, act => act.MapFrom (src => src.PromProduct.Discount))
                .ForMember (des => des.ProductIds, act => act.MapFrom (src => ConvertStringToArray (src.PromProduct.ProductIds)));

            CreateMap<Promotion, PromBillVM> ()
                .ForMember (des => des.ConditionAmount, act => act.MapFrom (src => src.PromBill.ConditionAmount))
                .ForMember (des => des.ConditionItem, act => act.MapFrom (src => src.PromBill.ConditionItem))
                .ForMember (des => des.Discount, act => act.MapFrom (src => src.PromBill.Discount));
            //
            CreateMap<PromBill, PromBillVM> ();
            CreateMap<PromBillVM, PromBill> ()
                .ForMember (des => des.Id, act => act.MapFrom (src => src.Id));
            //
            CreateMap<PromProduct, PromProductVM> ()
                .ForMember (des => des.ProductIds, act => act.MapFrom (src => ConvertStringToArray (src.ProductIds)));
            CreateMap<PromProductVM, PromProduct> ()
                .ForMember (des => des.Id, act => act.MapFrom (src => src.Id))
                .ForMember (des => des.ProductIds, act => act.MapFrom (src => src.ProductIds.ToString ()));
            CreateMap<PromProduct, PromVM> ();
            CreateMap<PromBill, PromVM> ();
            // Cateogry
            CreateMap<Category, CategoryVM> ();
            CreateMap<CategoryVM, Category> ();
            // TypeWire
            CreateMap<TypeWire, WireVM> ();
            CreateMap<WireVM, TypeWire> ();
            // Band
            CreateMap<Band, BandVM> ();
            CreateMap<BandVM, Band> ();
            //Fees
            CreateMap<Fee, FeeVM> ();
            CreateMap<FeeVM, Fee> ();
            //Order
            CreateMap<OrderVM, Order> ()
                .ForMember (src => src.Id, opts => opts.Ignore ());
            CreateMap<Order, OrderVM> ();
            CreateMap<OrderDetailVM, OrderDetail> ()
                .ForMember (src => src.ProductId, act => act.MapFrom (src => src.ProductId));
            CreateMap<OrderDetail, OrderDetailVM> ();
            //Policy
            CreateMap<Policy, PolicyVM> ();
            CreateMap<PolicyVM, Policy> ();
        }

        private int[] ConvertStringToArray (string str) {
            var arrInt = str.Split (',').Where (s => s != "").Select (int.Parse).ToArray ();
            return arrInt;
        }

    }
}