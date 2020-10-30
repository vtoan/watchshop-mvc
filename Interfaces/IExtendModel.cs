using System;
using System.Collections.Generic;
using aspcore_watchshop.EF;
using aspcore_watchshop.Models;
using aspcore_watchshop.ViewModels;

namespace aspcore_watchshop.Interfaces {
    public interface IPolicyModel : IDataModelBase<PolicyVM> { }
    public interface IFeeModel : IDataModelBase<FeeVM> { }
    public interface IWireModel : IDataModelBase<WireVM> { }
    public interface IBandModel : IDataModelBase<BandVM> { }
    public interface ICategoryModel : IUpdateModel<CategoryVM>, IGetListVMs<CategoryVM>, IGetVM<CategoryVM> { }
    public interface IInfoModel : IGetVM<Info>, IUpdateModel<Info> { }
    public interface IPostModel : IDataModelBase<PostVM> { } // GetListVMs not usage

    // ====================== Promotion ======================
    public interface IPromModel : IDataModelBase<PromVM> {
        bool AddModel (watchContext context, PromVM newObj, PromType promType);
        bool UpdateStatus (watchContext context, int id, bool stt);
        List<PromBillVM> GetListVMBills (watchContext context);
        List<PromProductVM> GetListVMProducts (watchContext context);
    }

    public interface IPromProductModel : IUpdateModel<PromProductVM>, IRemoveModel { }

    public interface IPromBillModel : IUpdateModel<PromBillVM>, IRemoveModel { }
    // ====================== Product ======================
    public interface IProductModel : IGetVM<ProductVM>, IUpdateModel<ProductVM>, IGetListVMs<ProductVM>, IRemoveModel {
        bool AddModel (watchContext context, ProductVM product, ProdDetailVM prodDetail);
        List<ProductVM> GetListVMs (watchContext context, int idCate);
        bool UpdateStatus (watchContext context, int id, bool stt);
        //Handler
        List<ProductVM> GetProductVMsByIDs (List<ProductVM> products, string ids);
        List<ProductVM> GetProductVMsByChar (List<ProductVM> products, string character);
        List<ProductVM> GetTopProductVMs (List<ProductVM> products);
        List<ProductVM> GetPromProductVMs (List<ProductVM> products);
        void GetProductInCarts (List<ProductVM> products, ref List<OrderDetailVM> items);
        void AddDiscount (ref List<ProductVM> products, List<PromProductVM> promProds);
    }

    public interface IProdDetailModel : IGetVM<ProdDetailVM>, IUpdateModel<ProdDetailVM>, IAddModel<ProdDetailVM>, IRemoveModel { }

    // ====================== Order ======================
    public interface IOrderModel : IGetVM<OrderVM> {
        int AddModel (watchContext context, OrderVM order, List<OrderDetailVM> lsItem);
        List<OrderVM> GetListVMs (watchContext ctext, DateTime start, DateTime end);
        List<OrderVM> FindVMsByString (watchContext ctext, string txt);
        bool UpdateStatus (watchContext ctext, int id, int stt);
    }

    public interface IOrderDetailModel : IAddRangeModel<OrderDetailVM> {
        List<OrderDetailVM> GetListVMs (watchContext context, int id);
    }

}