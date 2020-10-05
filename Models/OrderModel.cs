using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using aspcore_watchshop.Daos;
using aspcore_watchshop.EF;
using aspcore_watchshop.Entities;
using aspcore_watchshop.Hepler;

namespace aspcore_watchshop.Models
{

    public interface IOrderModel
    {
        bool AddOrderVM(watchContext ctext, OrderVM orderVM, List<OrderDetailVM> items, List<FeeVM> fees, string promotion);
        List<OrderVM> GetOrders(watchContext ctext, DateTime start, DateTime end);
        OrderVM GetOrderByID(watchContext ctext, int id);
        bool UpdateStatus(watchContext ctext, int id, int stt);
        List<OrderDetailVM> GetOrderDetailVMs(watchContext ctext, int id);
        List<OrderVM> FindOrders(watchContext ctext, int txt);
    }

    public class OrderModel : IOrderModel
    {
        public bool AddOrderVM(watchContext ctext, OrderVM orderVM, List<OrderDetailVM> items, List<FeeVM> fees, string promotion)
        {
            orderVM.DateCreated = DateTime.Now;
            orderVM.Fees = JsonSerializer.Serialize(fees);
            using (OrderDao db = new OrderDao(ctext))
            {
                Order order = Helper.ObjectMapperTo<Order, OrderVM>(orderVM);
                int orderID = db.Insert(order); // Add order into DB
                if (orderID == 0) return false;
                items.ForEach(i => i.orderID = orderID);
                db.InsertDetail(Helper.LsObjectMapperTo<OrderDetail, OrderDetailVM>(items)); // Add orderdetail into DB
            }
            return true;
        }

        public List<OrderVM> FindOrders(watchContext ctext, int txt)
        {
            List<Order> asset = null;
            using (OrderDao db = new OrderDao(ctext))
                asset = db.Find(txt);
            if (asset == null || asset.Count == 0) return null;
            return Helper.LsObjectMapperTo<OrderVM, Order>(asset);
        }

        public OrderVM GetOrderByID(watchContext ctext, int id)
        {
            Order asset = null;
            using (OrderDao db = new OrderDao(ctext))
                asset = db.Get(id);
            if (asset == null) return null;
            return Helper.ObjectMapperTo<OrderVM, Order>(asset);
        }

        public List<OrderDetailVM> GetOrderDetailVMs(watchContext ctext, int id)
        {
            List<OrderDetail> asset = null;
            using (OrderDao db = new OrderDao(ctext))
                asset = db.GetDetails(id);
            if (asset == null || asset.Count == 0) return null;
            return Helper.LsObjectMapperTo<OrderDetailVM, OrderDetail>(asset);
        }

        public List<OrderVM> GetOrders(watchContext ctext, DateTime start, DateTime end)
        {
            List<Order> asset = null;
            using (OrderDao db = new OrderDao(ctext))
                asset = db.GetList(start, end);
            if (asset == null || asset.Count == 0) return null;
            return Helper.LsObjectMapperTo<OrderVM, Order>(asset);
        }

        public bool UpdateStatus(watchContext ctext, int id, int stt)
        {
            using (OrderDao db = new OrderDao(ctext))
                return db.UpdateStatus(id, stt) == 0 ? false : true;
        }
    }

    public class OrderVM
    {
        public int ID { get; set; }
        public DateTime DateCreated { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string CustomerPhone { get; set; }
        [Required]
        public string CustomerEmail { get; set; }
        [Required]
        public string CustomerProvince { get; set; }
        [Required]
        public string CustomerAddress { get; set; }
        public string CustomerNote { get; set; }
        public string Promotion { get; set; }
        public string Fees { get; set; }
        public byte Status { get; set; }
    }

    public class OrderDetailVM
    {
        public int orderID { get; set; }
        public int id { get; set; }
        public byte quantity { get; set; }
        public int price { get; set; }
        public double discount { get; set; }
    }
}