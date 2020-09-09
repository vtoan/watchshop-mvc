using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using aspcore_watchshop.Models;

namespace aspcore_watchshop.Controllers
{
    public class ProductController : Controller
    {
        public static List<Product> data = new List<Product>()
            {
                new Product(){ID=1,Name="TISSOT DASD - ĐỒ HỒ NAM", Price=1900000,Discount=100000, CateID=1, WireID=1, SaleCount=100,Image="img.jpg"},
                new Product(){ID=2,Name="TISSOT DASD - ĐỒ HỒ NAM", Price=900000,Discount=0, CateID=1, WireID=2, SaleCount=99,Image="img2.jpg"},
                new Product(){ID=3,Name="TISSOT DASD - ĐỒ HỒ NAM", Price=19023000,Discount=0, CateID=1, WireID=3, SaleCount=120,Image="img.jpg"},
                new Product(){ID=4,Name="TISSOT DASD - ĐỒ HỒ NAM", Price=19000,Discount=100000, CateID=1, WireID=1, SaleCount=10,Image="img2.jpg"},
                new Product(){ID=5,Name="TISSOT DASD - ĐỒ HỒ NAM", Price=820000,Discount=100000, CateID=1, WireID=1, SaleCount=0,Image="img3.jpg"},
                new Product(){ID=6,Name="TISSOT DASD - ĐỒ HỒ NAM", Price=100000,Discount=100000, CateID=2, WireID=2, SaleCount=12,Image="img3.jpg"},
                new Product(){ID=1,Name="TISSOT DASD - ĐỒ HỒ NAM", Price=900000,Discount=100000, CateID=2, WireID=3, SaleCount=85,Image="img3.jpg"},
                new Product(){ID=2,Name="TISSOT DASD - ĐỒ HỒ NAM", Price=10000,Discount=0, CateID=2, WireID=1, SaleCount=160,Image="img2.jpg"},
                new Product(){ID=3,Name="TISSOT DASD - ĐỒ HỒ NAM", Price=782000,Discount=0, CateID=2, WireID=1, SaleCount=85,Image="img2.jpg"},
                new Product(){ID=4,Name="TISSOT DASD - ĐỒ HỒ NAM", Price=199000,Discount=100000, CateID=2, WireID=3, SaleCount=45,Image="img.jpg"},
                new Product(){ID=5,Name="TISSOT DASD - ĐỒ HỒ NAM", Price=197000,Discount=100000, CateID=2, WireID=1, SaleCount=58,Image="img.jpg"},
                new Product(){ID=6,Name="TISSOT DASD - ĐỒ HỒ NAM", Price=196000,Discount=100000, CateID=3, WireID=2, SaleCount=65,Image="img.jpg"},
                new Product(){ID=1,Name="TISSOT DASD - ĐỒ HỒ NAM", Price=1980000,Discount=100000, CateID=3, WireID=2, SaleCount=78,Image="img.jpg"},
                new Product(){ID=2,Name="TISSOT DASD - ĐỒ HỒ NAM", Price=1980000,Discount=0, CateID=3, WireID=1, SaleCount=23,Image="img2.jpg"},
                new Product(){ID=3,Name="TISSOT DASD - ĐỒ HỒ NAM", Price=1900000,Discount=0, CateID=3, WireID=2, SaleCount=85,Image="img2.jpg"},
                new Product(){ID=4,Name="TISSOT DASD - ĐỒ HỒ NAM", Price=1903000,Discount=100000, CateID=3, WireID=3, SaleCount=89,Image="img.jpg"},
                new Product(){ID=5,Name="TISSOT DASD - ĐỒ HỒ NAM", Price=19000,Discount=100000, CateID=3, WireID=3, SaleCount=15,Image="img.jpg"},
                new Product(){ID=6,Name="TISSOT DASD - ĐỒ HỒ NAM", Price=1910000,Discount=100000, CateID=3, WireID=3, SaleCount=14,Image="img2.jpg"}
            };

        private readonly ILogger<HomeController> _logger;

        public ProductController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Discount()
        {
            ViewBag.PageTitle = "Khuyến Mãi";
            ViewBag.PageCode = 0;
            return View("Index");
        }

        public IActionResult Men()
        {
            ViewBag.PageTitle = "Đồng hồ nam";
            ViewBag.PageCode = 1;
            return View("Index");
        }

        public IActionResult Women()
        {
            ViewBag.PageTitle = "Đồng hồ nữ";
            ViewBag.PageCode = 2;
            return View("Index");
        }

        public IActionResult Couple()
        {
            ViewBag.PageTitle = "Đồng hồ đổi";
            ViewBag.PageCode = 3;
            return View("Index");
        }

        public IActionResult Accessories()
        {
            ViewBag.PageTitle = "Phụ kiện";
            ViewBag.PageCode = 4;
            return View("Index");
        }

        public IActionResult FindProductByChar(string search)
        {
            ViewBag.PageTitle = "Kết quả tìm kiếm";
            ViewBag.PageCode = -1;
            return View("Index");
        }

        public IActionResult Detail()
        {
            return View("Detail");
        }

        //AJAX
        public JsonResult ProductByCate(int pageCode, int number)
        {
            if (number != 0) return Json(data.Take(number));
            if (pageCode == -1) return Json(data.Take(number)); // get products form list search result
            if (pageCode == -2) return Json(data.Take(number)); // get products best seller
            return Json(data);
        }

        public JsonResult ProductInCart(string orderItemID)
        {
            string[] itemIDs = orderItemID.Split(',');
            List<Product> reponse = new List<Product>();
            foreach (var id in itemIDs)
            {
                if (id == "") break;
                Product obj = data.Find(p => p.ID == Int32.Parse(id));
                if (obj == null) break;
                reponse.Add(obj);
            }
            return Json(reponse);
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
