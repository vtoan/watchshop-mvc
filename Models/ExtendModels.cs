using aspcore_watchshop.EF;
using aspcore_watchshop.Interfaces;
using aspcore_watchshop.ViewModels;

namespace aspcore_watchshop.Models {
    public class PolicyModel : DataModelBase<PolicyVM, Policy>, IPolicyModel { /*new code*/ }

    public class FeeModel : DataModelBase<FeeVM, Fee>, IFeeModel { /*new code*/ }

    public class WireModel : DataModelBase<WireVM, TypeWire>, IWireModel { /*new code*/ }
    public class BandModel : DataModelBase<BandVM, Band>, IBandModel { /*new code*/ }

    public class CategoryModel : DataModelBase<CategoryVM, Category>, ICategoryModel { /*new code*/ }

    public class PostModel : DataModelBase<PostVM, Post>, IPostModel { /*new code*/ }

    public class ProdDetailModel : DataModelBase<ProdDetailVM, ProductDetail>, IProdDetailModel { /*new code*/ }

    public class PromProductModel : DataModelBase<PromProductVM, PromProduct>, IPromProductModel { }

    public class PromBillModel : DataModelBase<PromBillVM, PromBill>, IPromBillModel { }
}