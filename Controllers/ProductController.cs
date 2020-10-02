using System.Collections.Generic;
using System.Linq;
using aspcore_watchshop.Models;
using Microsoft.AspNetCore.Mvc;
using aspcore_watchshop.EF;
using Microsoft.Extensions.Caching.Memory;
using aspcore_watchshop.Hepler;
using System.Text.Json;

namespace aspcore_watchshop.Controllers
{
    public class ProductController : Controller
    {
        private IProductModel _modelProduct = null;
        private IPromModel _modelProm = null;
        private IMemoryCache _cache = null;
        private watchContext _context = null;

        public ProductController(IMemoryCache cache, IProductModel product, IPromModel prom, watchContext ctext)
        {
            _modelProduct = product;
            _modelProm = prom;
            _context = ctext;
            _cache = cache;
        }

        #region Request            
        [ActionName("khuyen-mai")]
        public IActionResult Discount()
        {
            ViewBag.PageCode = 0;
            LayoutData.SetForCategory(GetCategory(0));
            ViewBag.Wires = GetWires();
            return View("Index");
        }

        [ActionName("dong-ho-nam")]
        public IActionResult Men()
        {
            ViewBag.PageCode = 1;
            LayoutData.SetForCategory(GetCategory(1));
            ViewBag.Wires = GetWires();
            return View("Index");
        }

        [ActionName("dong-ho-nu")]
        public IActionResult Women()
        {
            ViewBag.PageCode = 2;
            LayoutData.SetForCategory(GetCategory(2));
            ViewBag.Wires = GetWires();
            return View("Index");
        }

        [ActionName("dong-ho-doi")]
        public IActionResult Couple()
        {
            ViewBag.PageCode = 3;
            LayoutData.SetForCategory(GetCategory(3));
            ViewBag.Wires = GetWires();
            return View("Index");
        }

        [ActionName("phu-kien")]
        public IActionResult Accessories()
        {
            ViewBag.PageCode = 4;
            LayoutData.SetForCategory(GetCategory(4));
            ViewBag.Wires = GetWires();
            return View("Index");
        }

        [ActionName("tim-kiem")]
        public IActionResult FindProductByChart(string text)
        {
            ViewBag.PageCode = -2;
            ViewBag.Wires = GetWires();
            ViewBag.TextSearch = text;
            if (text != "" && text != null)
            {
                var response = _modelProduct.GetProductVMsByChar(GetProducts(), text);
                if (response.Count == 0) return View("Index");
                ViewBag.ResultCount = response.Count;
                var json = JsonSerializer.Serialize(response);
                TempData["result"] = json;
            }
            return View("Index");
        }

        public IActionResult Detail([FromServices] IPolicyModel policy, [FromServices] IPostModel post, int id)
        {
            if (id <= 0) return RedirectToAction("Home/Error");
            PropDetailVM detail = _modelProduct.GetProdDetailVM(_context, id);
            LayoutData.SetForProduct(detail);
            //
            ViewBag.Detail = detail != null ? detail : new PropDetailVM();
            ViewBag.Post = post.GetPostVM(_context, id);
            ViewBag.Policies = policy.GetPolices(_context);
            var product = GetProducts().Find(item => item.ID == id);
            return View(product);
        }
        #endregion
        //====================== AJAX ======================//
        #region AJAX
        public JsonResult ProductByCate(int pageCode, int numberItem)
        {
            string keyCache = "";
            switch (pageCode)
            {
                case -1: keyCache = CacheKey.SELLER; break;
                case 0: keyCache = CacheKey.DISCOUNT; break;
                case 1: keyCache = CacheKey.MEN; break;
                case 2: keyCache = CacheKey.WOMEN; break;
                case 3: keyCache = CacheKey.COUPLE; break;
                case 4: keyCache = CacheKey.ACCESSORIES; break;
                default: break;
            }
            if (keyCache == "") return Json(null);
            //Get in Cache
            List<ProductVM> resqonse = Cache.Get<List<ProductVM>>(_cache, keyCache);
            if (resqonse == null || resqonse.Count == 0)
            {
                //Get Product in List Product (Db or Cache)
                if (pageCode == 0)
                    resqonse = _modelProduct.GetPromProductVMs(GetProducts());
                else if (pageCode < 0)
                    resqonse = _modelProduct.GetTopProductVMs(GetProducts());
                else
                    resqonse = _modelProduct.GetProductVMs(_context, _modelProm.GetPromProductVMs(_context), pageCode);
                if (resqonse == null) return Json(null);
                // Save in Cache
                Cache.Set(_cache, resqonse, keyCache);
            }
            return Json(numberItem != 0 ? resqonse.Take(numberItem) : resqonse);
        }

        public JsonResult FindProduct()
        {
            if (!TempData.ContainsKey("result")) return Json(null);
            return Json(JsonSerializer.Deserialize<List<ProductVM>>(TempData["result"].ToString()));
        }

        public JsonResult GetProductsByIDs(string idString)
        {
            return Json(_modelProduct.GetProductVMsByIDs(GetProducts(), idString));
        }
        #endregion
        //==================== GET DATA ===================//
        private List<ProductVM> GetProducts()
        {
            return DataHelper.Products(_context, _cache, _modelProduct, _modelProm);
        }

        private CategoryVM GetCategory(int idCate)
        {
            string keyCache = "";
            switch (idCate)
            {
                case 1: keyCache = CacheKey.MEN_SEO; break;
                case 2: keyCache = CacheKey.WOMEN_SEO; break;
                case 3: keyCache = CacheKey.COUPLE_SEO; break;
                case 4: keyCache = CacheKey.ACCESSORIES_SEO; break;
            }
            if (keyCache == "") return null;
            CategoryVM cates = Cache.Get<CategoryVM>(_cache, keyCache);
            if (cates == null)
            {
                CategoryModel cateModel = new CategoryModel();
                cates = cateModel.GetCategoryVMByID(_context, idCate);
                if (cates != null)
                    Cache.Set(_cache, cates, keyCache);
            }
            return cates;
        }

        private List<WireVM> GetWires()
        {
            List<WireVM> wires = Cache.Get<List<WireVM>>(_cache, CacheKey.WIRE_INFO);
            if (wires == null || wires.Count == 0)
            {
                TypeWireModel wireModel = new TypeWireModel();
                wires = wireModel.GetWireVMs(_context);
                if (wires != null)
                    Cache.Set(_cache, wires, CacheKey.WIRE_INFO);
            }
            return wires;
        }

    }
}