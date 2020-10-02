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
        void AddOrderVM(watchContext ctext, OrderVM orderVM, List<OrderDetailVM> items, List<FeeVM> fees, string promotion);
    }

    public class OrderModel : IOrderModel
    {
        public void AddOrderVM(watchContext ctext, OrderVM orderVM, List<OrderDetailVM> items, List<FeeVM> fees, string promotion)
        {
            orderVM.DateCreated = DateTime.Now;
            orderVM.Fees = JsonSerializer.Serialize(fees);
            using (OrderDao db = new OrderDao())
            {
                Order order = Helper.ObjectToVM<Order, OrderVM>(orderVM);
                order = db.Insert(ctext, order); // Add order into DB
                if (order == null) return;
                int orderID = order.ID;
                items.ForEach(i => i.orderID = orderID);
                db.Insert(ctext, Helper.LsObjectToLsVM<OrderDetail, OrderDetailVM>(items)); // Add orderdetail into DB
            }

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