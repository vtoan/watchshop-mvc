using System;
using aspcore_watchshop.EF;
using aspcore_watchshop.Entities;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace aspcore_watchshop.Daos
{
    public class OrderDao : IDisposable
    {
        private watchContext _context = null;

        public OrderDao(watchContext context) => _context = context;

        public void Dispose() { }
        //Query Order
        public Order Get(int id)
        {
            return _context.Orders.Where(item => item.ID == id)
            .FirstOrDefault();
        }

        public List<Order> GetList(DateTime start, DateTime end)
        {
            return _context.Orders
                .Where(item => item.DateCreated >= start || item.DateCreated <= end)
                .ToList();
        }

        public List<Order> Find(int txt)
        {
            return _context.Orders
                .Where(item => item.CustomerPhone.Contains(txt.ToString()) || item.ID == txt)
                .ToList();
        }

        public int Insert(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
            return order.ID;
        }

        public int UpdateStatus(int id, int stt)
        {
            Order od = _context.Orders.Where(item => item.ID == id).FirstOrDefault();
            if (od == null) return 0;
            od.Status = Convert.ToByte(stt);
            _context.SaveChangesAsync();
            return 1;
        }

        // Query Detail
        public List<OrderDetail> GetDetails(int id)
        {
            return _context.OrderDetails.Where(item => item.OrderID == id).ToList();
        }

        public int InsertDetail(List<OrderDetail> orderDetails)
        {
            _context.OrderDetails.AddRange(orderDetails);
            _context.SaveChangesAsync();
            return 1;
        }
    }
}