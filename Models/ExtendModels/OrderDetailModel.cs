using System.Collections.Generic;
using aspcore_watchshop.Daos.ExtendDaos;
using aspcore_watchshop.EF;
using aspcore_watchshop.Interfaces;
using aspcore_watchshop.ViewModels;
using aspcore_watchshop.Hepler;

namespace aspcore_watchshop.Models.ExtendModels
{
    public class OrderDetailModel : DataModelBase<OrderDetailVM, OrderDetail>, IOrderDetailModel
    {
        public OrderDetailModel() : base(new OrderDetailDao()) { }

        public List<OrderDetailVM> GetListVMs(watchContext context, int id)
        {
            using (OrderDetailDao db = new OrderDetailDao())
                return DataHelper.Instance.LsObjectMapperTo<OrderDetailVM, OrderDetail>(db.GetList(context, id));
        }
    }
}