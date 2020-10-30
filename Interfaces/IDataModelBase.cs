using System.Collections.Generic;
using aspcore_watchshop.EF;

namespace aspcore_watchshop.Interfaces {
    public interface IGetListVMs<T> { List<T> GetListVMs (watchContext context); }

    public interface IGetVM<T> { T GetVM (watchContext context, int id); }

    public interface IAddModel<T> { bool AddModel (watchContext context, T newObj); }

    public interface IAddRangeModel<T> { bool AddRangeModel (watchContext context, List<T> newObj); } // Not include in IDataModel

    public interface IUpdateModel<T> { bool UpdateModel (watchContext context, int idSrc, T objVM); }

    public interface IRemoveModel { bool RemoveModel (watchContext context, int id); }

    // Full CURD Inteface
    public interface IDataModelBase<T> : IRemoveModel, IAddModel<T>, IGetListVMs<T>, IGetVM<T>, IUpdateModel<T> { }
}