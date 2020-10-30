using System;
using System.ComponentModel.DataAnnotations;

namespace aspcore_watchshop.ViewModels {
    public class PolicyVM {
        public int Id { get; set; }

        [Required]
        public string PolicyContent { get; set; }

        [Required]
        public string Icon { get; set; }
    }

    public class FeeVM {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double? Cost { get; set; }
    }

    public class BandVM {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }

    public class WireVM {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }

    public class CategoryVM {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        //SEO
        public string SeoImage { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
    }

    public class PostVM {
        public int Id { get; set; }

        [Required]
        public string PostContent { get; set; }
    }

    public class PromVM {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public bool Status { get; set; }
        public int? Type { get; set; }
    }

    public class PromProductVM : PromVM {
        public double? Discount { get; set; }
        public int[] ProductIds { get; set; }
        public int? CategoryId { get; set; }
        public int? BandId { get; set; }
    }

    public class PromBillVM : PromVM {
        public double Discount { get; set; }
        // public string ItemFree { get; set; }
        public int? ConditionItem { get; set; }
        public int? ConditionAmount { get; set; }
    }

    public class ProductVM {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public string WireName { get; set; }
        public string BandName { get; set; }
        public double? Discount { get; set; }
        public int? Price { get; set; }
        public int? CategoryId { get; set; }
        public int? TypeWireId { get; set; }
        public int? BandId { get; set; }
        public string Image { get; set; }
        public int? SaleCount { get; set; }
        public bool? isShow { get; set; }
    }

    public class ProdDetailVM {
        public string Images { get; set; }
        public string TypeGlass { get; set; }
        public string TypeBorder { get; set; }
        public string TypeMachine { get; set; }
        public string Parameter { get; set; }
        public string ResistWater { get; set; }
        public string Warranty { get; set; }
        public string Origin { get; set; }
        public string Color { get; set; }
        public string Func { get; set; }
        //SEO
        public string SeoImage { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
    }

    public class OrderVM {
        public int Id { get; set; }
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

    public class OrderDetailVM {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public byte Quantity { get; set; }
        public int Price { get; set; }
        public double Discount { get; set; }
    }

}