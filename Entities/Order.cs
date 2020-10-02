using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aspcore_watchshop.Entities
{
    public class Order
    {
        [Key]
        public int ID { get; set; }
        public DateTime DateCreated { get; set; }

        [StringLength(40)]
        public string CustomerName { get; set; }

        [StringLength(50)]
        public string CustomerPhone { get; set; }

        [StringLength(25)]
        public string CustomerProvince { get; set; }

        [StringLength(250)]
        public string CustomerAddress { get; set; }

        [StringLength(250)]
        public string CustomerNote { get; set; }

        [StringLength(50)]
        public string Promotion { get; set; }

        [StringLength(150)]
        public string Fees { get; set; }
        public byte Status { get; set; }
        //Nav property
        public List<OrderDetail> OrderDetails { get; set; }
    }
}