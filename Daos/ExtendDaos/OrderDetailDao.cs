using System.Collections.Generic;
using System.Linq;
using aspcore_watchshop.EF;

namespace aspcore_watchshop.Daos.ExtendDaos {
    public class OrderDetailDao : DAO<OrderDetail> {
        public List<OrderDetail> GetList (watchContext context, int id) {
            return context.OrderDetails.Where (item => item.OrderId == id).ToList ();
        }
    }
}