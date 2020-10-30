using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace aspcore_watchshop.EF {
    public class watchContext : IdentityDbContext<IdentityUser> {
        public watchContext (DbContextOptions<watchContext> options) : base (options) { }
        //DbSets    
        public DbSet<Band> Bands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Fee> Fees { get; set; }
        public DbSet<Info> Infos { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Policy> Policies { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductDetail> ProductDetails { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<PromProduct> PromProducts { get; set; }
        public DbSet<PromBill> PromBills { get; set; }
        public DbSet<TypeWire> TypeWires { get; set; }

        protected override void OnModelCreating (ModelBuilder modelBuilder) {
            base.OnModelCreating (modelBuilder);
            //Config OrderDetail
            modelBuilder.Entity<OrderDetail> ().HasKey (od => new { od.OrderId, od.ProductId });
            //Config Order
            modelBuilder.Entity<Order> ().Property (o => o.Status).HasDefaultValue (1);
            modelBuilder.Entity<Order> ().Property (o => o.DateCreated).HasColumnType ("smalldatetime");
            //Config Promotion
            modelBuilder.Entity<Promotion> ().Property (p => p.Status).HasDefaultValue (true);
            modelBuilder.Entity<Promotion> ().Property (p => p.ToDate).HasColumnType ("smalldatetime");
            modelBuilder.Entity<Promotion> ().Property (p => p.FromDate).HasColumnType ("smalldatetime");
            //Config Product
            modelBuilder.Entity<Product> ().Property (p => p.isShow).HasDefaultValue (true);
            modelBuilder.Entity<Product> ().Property (p => p.isDel).HasDefaultValue (false);
            modelBuilder.Entity<Product> ().Property (p => p.SaleCount).HasDefaultValue (0);
            // Remove prefix of identity
            foreach (var entityType in modelBuilder.Model.GetEntityTypes ()) {
                var tableName = entityType.GetTableName ();
                if (tableName.StartsWith ("AspNet")) {
                    entityType.SetTableName (tableName.Substring (6));
                }
            }
        }

    }

    //Entities
    public class User : IdentityUser {

    }

    public class Band {
        [Key]
        public int Id { get; set; }

        [StringLength (30)]
        public string Name { get; set; }
        //Nav property
        public List<Product> Products { get; set; }

    }

    public class TypeWire {
        [Key]
        public int Id { get; set; }

        [MaxLength (50)]
        public string Name { get; set; }
        //Nav property
        public List<Product> Products { get; set; }
    }

    public class Category {
        [Key]
        public int Id { get; set; }

        [MaxLength (30)]
        public string Name { get; set; }
        //SEO
        [MaxLength (50)]
        public string SeoImage { get; set; }

        [MaxLength (150)]
        public string SeoTitle { get; set; }

        [MaxLength (350)]
        public string SeoDescription { get; set; }

        //Nav property
        public List<Product> Products { get; set; }
    }

    public class Fee {
        [Key]
        public int Id { get; set; }

        [StringLength (30)]
        public string Name { get; set; }
        public double? Cost { get; set; }
    }

    public class Info {
        [Key]
        public int Id { get; set; }

        [StringLength (50)]
        public string NameStore { get; set; }

        [StringLength (50)]
        public string Logo { get; set; }

        [StringLength (100)]

        public string Email { get; set; }

        [StringLength (100)]

        public string Facebook { get; set; }

        [StringLength (100)]
        public string Messenger { get; set; }

        [StringLength (100)]

        public string Instargram { get; set; }

        [StringLength (100)]

        public string Phone { get; set; }

        [StringLength (150)]
        public string Address { get; set; }

        [StringLength (50)]
        public string WorkTime { get; set; }
        //SEO
        [MaxLength (50)]
        public string SeoImage { get; set; }

        [StringLength (250)]
        public string SeoTitle { get; set; }

        [StringLength (350)]
        public string SeoDescription { get; set; }
    }

    public class Order {
        [Key]
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }

        [StringLength (40)]
        public string CustomerName { get; set; }

        [StringLength (50)]
        public string CustomerPhone { get; set; }

        [StringLength (25)]
        public string CustomerProvince { get; set; }

        [StringLength (35)]
        public string CustomerEmail { get; set; }

        [StringLength (250)]
        public string CustomerAddress { get; set; }

        [StringLength (250)]
        public string CustomerNote { get; set; }

        [StringLength (50)]
        public string Promotion { get; set; }

        [StringLength (150)]
        public string Fees { get; set; }
        public byte Status { get; set; }
        //Nav property
        public List<OrderDetail> OrderDetails { get; set; }
    }

    public class OrderDetail {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public byte Quantity { get; set; }
        public int Price { get; set; }
        public double Discount { get; set; }
        //Nav property
        public Order Order { get; set; }
        public Product Product { get; set; }
    }

    public class Policy {
        [Key]
        public int Id { get; set; }

        [StringLength (150)]
        public string PolicyContent { get; set; }

        [StringLength (50)]
        public string Icon { get; set; }
    }

    public class Post {
        [Key]
        [DatabaseGenerated (DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string PostContent { get; set; }
    }

    public class Product {
        [Key]
        public int Id { get; set; }

        [MaxLength (50)]
        public string Name { get; set; }
        public bool? isShow { get; set; }
        public bool? isDel { get; set; }
        public int Price { get; set; }
        public int SaleCount { get; set; }

        [StringLength (50)]
        public string Image { get; set; }

        [ForeignKey ("Category")]
        public int CategoryId { get; set; }

        [ForeignKey ("TypeWire")]
        public int TypeWireId { get; set; }

        [ForeignKey ("Band")]
        public int BandId { get; set; }
        //Nav property
        public Band Band { get; set; }
        public Category Category { get; set; }
        public TypeWire TypeWire { get; set; }
        public ProductDetail ProductDetail { get; set; }

    }

    public class ProductDetail {
        [Key]
        [ForeignKey ("Product")]
        [DatabaseGenerated (DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Images { get; set; }

        [MaxLength (30)]
        public string TypeGlass { get; set; }

        [MaxLength (30)]
        public string TypeBorder { get; set; }

        [MaxLength (30)]
        public string TypeMachine { get; set; }

        [MaxLength (30)]
        public string Parameter { get; set; }

        [MaxLength (30)]
        public string ResistWater { get; set; }

        [MaxLength (30)]
        public string Warranty { get; set; }

        [MaxLength (30)]
        public string Origin { get; set; }

        [MaxLength (30)]
        public string Color { get; set; }

        [MaxLength (30)]
        public string Func { get; set; }
        //SEO
        [MaxLength (50)]
        public string SeoImage { get; set; }

        [StringLength (250)]
        public string SeoTitle { get; set; }

        [StringLength (350)]
        public string SeoDescription { get; set; }

        //Nav property
        public Product Product { get; set; }
    }

    public class Promotion {
        [Key]
        public int Id { get; set; }

        [StringLength (50)]
        public string Name { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public bool? Status { get; set; }
        public byte? Type { get; set; }
        //Nav property
        public PromProduct PromProduct { get; set; }
        public PromBill PromBill { get; set; }
    }

    public class PromBill {
        [Key]
        [ForeignKey ("Promotion")]
        [DatabaseGenerated (DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public double? Discount { get; set; }
        public byte? ConditionItem { get; set; }
        public int? ConditionAmount { get; set; }
        //Nav property
        public Promotion Promotion { get; set; }
    }

    public class PromProduct {
        [Key]
        [ForeignKey ("Promotion")]
        public int Id { get; set; }
        public double Discount { get; set; }

        [StringLength (250)]
        public string ProductIds { get; set; }
        public int? CategoryId { get; set; }
        public int? BandId { get; set; }
        //Nav property
        public Promotion Promotion { get; set; }
    }
}