using System;
using aspcore_watchshop.EF;
using aspcore_watchshop.Entities;
using System.Linq;
using System.Collections.Generic;

namespace aspcore_watchshop.Daos
{
    public class OrderDao : IDisposable
    {
        public void Dispose() { }

        public Order Insert(watchContext context, Order order)
        {
            context.Orders.Add(order);
            context.SaveChanges();
            return order;
        }

        public void Insert(watchContext context, List<OrderDetail> orderDetails)
        {
            context.OrderDetails.AddRange(orderDetails);
            context.SaveChangesAsync();
        }
    }
}