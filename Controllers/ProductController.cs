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
                new Product(){ID=7,Name="TISSOT DASD - ĐỒ HỒ NAM", Price=900000,Discount=100000, CateID=2, WireID=3, SaleCount=85,Image="img3.jpg"},
                new Product(){ID=8,Name="TISSOT DASD - ĐỒ HỒ NAM", Price=10000,Discount=0, CateID=2, WireID=1, SaleCount=160,Image="img2.jpg"},
                new Product(){ID=9,Name="TISSOT DASD - ĐỒ HỒ NAM", Price=782000,Discount=0, CateID=2, WireID=1, SaleCount=85,Image="img2.jpg"},
                new Product(){ID=10,Name="TISSOT DASD - ĐỒ HỒ NAM", Price=199000,Discount=100000, CateID=2, WireID=3, SaleCount=45,Image="img.jpg"},
                new Product(){ID=11,Name="TISSOT DASD - ĐỒ HỒ NAM", Price=197000,Discount=100000, CateID=2, WireID=1, SaleCount=58,Image="img.jpg"},
                new Product(){ID=12,Name="TISSOT DASD - ĐỒ HỒ NAM", Price=196000,Discount=100000, CateID=3, WireID=2, SaleCount=65,Image="img.jpg"},
                new Product(){ID=13,Name="TISSOT DASD - ĐỒ HỒ NAM", Price=1980000,Discount=100000, CateID=3, WireID=2, SaleCount=78,Image="img.jpg"},
                new Product(){ID=14,Name="TISSOT DASD - ĐỒ HỒ NAM", Price=1980000,Discount=0, CateID=3, WireID=1, SaleCount=23,Image="img2.jpg"},
                new Product(){ID=15,Name="TISSOT DASD - ĐỒ HỒ NAM", Price=1900000,Discount=0, CateID=3, WireID=2, SaleCount=85,Image="img2.jpg"},
                new Product(){ID=16,Name="TISSOT DASD - ĐỒ HỒ NAM", Price=1903000,Discount=100000, CateID=3, WireID=3, SaleCount=89,Image="img.jpg"},
                new Product(){ID=17,Name="TISSOT DASD - ĐỒ HỒ NAM", Price=19000,Discount=100000, CateID=3, WireID=3, SaleCount=15,Image="img.jpg"},
                new Product(){ID=18,Name="TISSOT DASD - ĐỒ HỒ NAM", Price=1910000,Discount=100000, CateID=3, WireID=3, SaleCount=14,Image="img2.jpg"}
            };

        private readonly ILogger<HomeController> _logger;

        public ProductController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [ActionName("khuyen-mai")]
        public IActionResult Discount()
        {
            ViewBag.PageTitle = "Khuyến Mãi";
            ViewBag.PageCode = 0;
            return View("Index");
        }

        [ActionName("dong-ho-nam")]
        public IActionResult Men()
        {
            ViewBag.PageTitle = "Đồng hồ nam";
            ViewBag.PageCode = 1;
            return View("Index");
        }

        [ActionName("dong-ho-nu")]
        public IActionResult Women()
        {
            ViewBag.PageTitle = "Đồng hồ nữ";
            ViewBag.PageCode = 2;
            return View("Index");
        }

        [ActionName("dong-ho-doi")]
        public IActionResult Couple()
        {
            ViewBag.PageTitle = "Đồng hồ đôi";
            ViewBag.PageCode = 3;
            return View("Index");
        }

        [ActionName("phu-kien")]
        public IActionResult Accessories()
        {
            ViewBag.PageTitle = "Phụ kiện";
            ViewBag.PageCode = 4;
            return View("Index");
        }

        [ActionName("tim-kiem")]
        public IActionResult FindProductByChart(string text)
        {
            ViewBag.PageTitle = "Kết quả tìm kiếm";
            ViewBag.PageCode = -1;
            ViewBag.SearchResult = 50;
            string result = "";
            data.ForEach(item =>
            {
                if (item.Name.Contains(text)) result += item.ID + ",";
            });
            TempData["result"] = result;
            return View("Index");
        }

        public IActionResult Detail(int id)
        {
            return View();
        }

        //AJAX
        public JsonResult ProductByCate(int pageCode, int number)
        {
            if (number != 0) return Json(data.Take(number));
            if (pageCode == -1) return Json(GetProductsByID(TempData["result"] as string)); // get products form list search result
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


        public List<Product> GetProductsByID(string str)
        {
            List<Product> ls = new List<Product>();
            string[] arr = str.Split(',');
            foreach (string id in arr)
            {
                if (id != "")
                {
                    int i = Int32.Parse(id);
                    ls.Add(data.Find(item => item.ID == i));
                }
            }
            return ls;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
