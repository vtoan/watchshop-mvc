using System;
using System.Collections.Generic;
using System.Linq;
using aspcore_watchshop.EF;
using aspcore_watchshop.Hepler;

namespace aspcore_watchshop.Daos {
    public class OrderDao : DAO<Order> {
        //Query Order
        public List<Order> GetList (watchContext context, DateTime start, DateTime end) {
            return context.Orders
                .Where (item => item.DateCreated >= start && item.DateCreated <= end)
                .ToList ();
        }

        // Find by phone or, id;
        public List<Order> Find (watchContext context, string txt) {
            return context.Orders
                .Where (item => item.CustomerPhone.Contains (txt) || item.Id == Int32.Parse (txt)).ToList ();

        }

        public bool UpdateStatus (watchContext context, int id, int stt) {
            Order od = context.Orders.Where (item => item.Id == id).FirstOrDefault ();
            if (od == null) return false;
            od.Status = Convert.ToByte (stt);
            context.SaveChangesAsync ();
            return true;
        }

        public int Insert (watchContext context, Order newObj, List<OrderDetail> newLsObj) {
            newObj.OrderDetails = newLsObj;
            return base.Insert (context, newObj) ? newObj.Id : 0;
        }
    }
}