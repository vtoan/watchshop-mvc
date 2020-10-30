using System;
using System.Collections.Generic;
using aspcore_watchshop.Daos;
using aspcore_watchshop.EF;
using aspcore_watchshop.Hepler;
using aspcore_watchshop.Interfaces;
using aspcore_watchshop.ViewModels;

namespace aspcore_watchshop.Models {

    public class OrderModel : DataModelBase<OrderVM, Order>, IOrderModel {
        public OrderModel () : base (new OrderDao ()) { }

        public int AddModel (watchContext context, OrderVM order, List<OrderDetailVM> lsItem) {
            var obj = DataHelper.Instance.ObjectMapperTo<Order, OrderVM> (order);
            var lsObj = DataHelper.Instance.LsObjectMapperTo<OrderDetail, OrderDetailVM> (lsItem);
            if (obj == null || lsObj == null) return 0;
            using (OrderDao db = new OrderDao ())
            return db.Insert (context, obj, lsObj);
        }

        public List<OrderVM> GetListVMs (watchContext ctext, DateTime start, DateTime end) {
            using (OrderDao db = new OrderDao ())
            return DataHelper.Instance.LsObjectMapperTo<OrderVM, Order> (db.GetList (ctext, start, end));
        }

        public List<OrderVM> FindVMsByString (watchContext ctext, string txt) {
            using (OrderDao db = new OrderDao ())
            return DataHelper.Instance.LsObjectMapperTo<OrderVM, Order> (db.Find (ctext, txt));
        }

        public bool UpdateStatus (watchContext ctext, int id, int stt) {
            using (OrderDao db = new OrderDao ())
            return db.UpdateStatus (ctext, id, stt) ? true : false;
        }
    }

}