using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using aspcore_watchshop.EF;
using aspcore_watchshop.Hepler;
using aspcore_watchshop.Interfaces;
using aspcore_watchshop.Models;
using aspcore_watchshop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace aspcore_watchshop.Controllers {
    public class ProductController : Controller {
        private IProductModel _productModel = null;
        private IPromModel _promModel = null;
        private IMemoryCache _cache = null;
        private watchContext _context = null;

        public ProductController (IMemoryCache cache, IProductModel product, IPromModel prom, watchContext ctext) {
            _productModel = product;
            _promModel = prom;
            _context = ctext;
            _cache = cache;
        }

        #region Request  
        [HttpGet]
        [ActionName ("khuyen-mai")]
        public IActionResult Discount () {
            ViewBag.PageCode = 0;
            LayoutData.Instance.SetForCategory (GetCategory (0));
            ViewBag.Wires = GetWires ();
            return View ("Index");
        }

        [HttpGet]
        [ActionName ("dong-ho-nam")]
        public IActionResult Men () {
            ViewBag.PageCode = 1;
            LayoutData.Instance.SetForCategory (GetCategory (1));
            ViewBag.Wires = GetWires ();
            return View ("Index");
        }

        [HttpGet]
        [ActionName ("dong-ho-nu")]
        public IActionResult Women () {
            ViewBag.PageCode = 2;
            LayoutData.Instance.SetForCategory (GetCategory (2));
            ViewBag.Wires = GetWires ();
            return View ("Index");
        }

        [HttpGet]
        [ActionName ("dong-ho-doi")]
        public IActionResult Couple () {
            ViewBag.PageCode = 3;
            LayoutData.Instance.SetForCategory (GetCategory (3));
            ViewBag.Wires = GetWires ();
            return View ("Index");
        }

        [HttpGet]
        [ActionName ("phu-kien")]
        public IActionResult Accessories () {
            ViewBag.PageCode = 4;
            LayoutData.Instance.SetForCategory (GetCategory (4));
            ViewBag.Wires = GetWires ();
            return View ("Index");
        }

        [HttpGet]
        [ActionName ("tim-kiem")]
        public IActionResult FindProduct (string text) {
            ViewBag.PageCode = -2;
            ViewBag.Wires = GetWires ();
            ViewBag.TextSearch = text;
            if (text != "" && text != null) {
                var response = _productModel.GetProductVMsByChar (GetProducts (), text);
                if (response.Count == 0) return View ("Index");
                ViewBag.ResultCount = response.Count;
                var json = JsonSerializer.Serialize (response);
                TempData["result"] = json;
            }
            return View ("Index");
        }

        [HttpGet]
        public IActionResult Detail (
            [FromServices] IPolicyModel policyModel, [FromServices] IPostModel postModel, [FromServices] IProdDetailModel detailModel,
            int id) {
            if (id <= 0) return RedirectToAction ("Home/Error");
            ProdDetailVM detail = detailModel.GetVM (_context, id);
            LayoutData.Instance.SetForProduct (detail);
            //
            ViewBag.Detail = detail != null ? detail : new ProdDetailVM ();
            ViewBag.Post = postModel.GetVM (_context, id);
            ViewBag.Policies = policyModel.GetListVMs (_context);
            var product = GetProducts ().Find (item => item.Id == id);
            return View (product);
        }
        #endregion
        //====================== AJAX ======================//
        #region AJAX

        [HttpGet]
        public IActionResult GetProductByCate (int pageCode, int numberItem) {

            Changed keyCache;
            switch (pageCode) {
                case -1:
                    keyCache = Changed.SELLER_PRODUCT;
                    break;
                case 0:
                    keyCache = Changed.DISCOUNT_PRODUCT;
                    break;
                case 1:
                    keyCache = Changed.MEN_PRODUCT;
                    break;
                case 2:
                    keyCache = Changed.WOMEN_PRODUCT;
                    break;
                case 3:
                    keyCache = Changed.COUPLE_PRODUCT;
                    break;
                case 4:
                    keyCache = Changed.ACCESSORIES_PRODUCT;
                    break;
                default:
                    return NotFound ();
            }
            List<ProductVM> resqonse = null;
            //Get in Cache
            if (!CacheHelper.isChanged (Changed.PRODUCT))
                resqonse = CacheHelper.Get<List<ProductVM>> (_cache, keyCache);
            if (resqonse == null || resqonse.Count == 0) {
                //Get Product in List Product (Db or Cache)
                if (pageCode == 0)
                    resqonse = _productModel.GetPromProductVMs (GetProducts ());
                else if (pageCode < 0)
                    resqonse = _productModel.GetTopProductVMs (GetProducts ());
                else {
                    resqonse = _productModel.GetListVMs (_context, pageCode);
                    _productModel.AddDiscount (ref resqonse, _promModel.GetListVMProducts (_context));
                }
                if (resqonse == null) return NoContent ();
                // Save in Cache
                CacheHelper.Set (_cache, resqonse, keyCache);
            }
            return Json (numberItem != 0 ? resqonse.Take (numberItem) : resqonse);
        }

        [HttpGet]
        public JsonResult GetProductResult () {
            if (!TempData.ContainsKey ("result")) return Json (null);
            return Json (JsonSerializer.Deserialize<List<ProductVM>> (TempData["result"].ToString ()));
        }

        [HttpGet]
        public JsonResult GetProductsByIDs (string idString) {
            return Json (_productModel.GetProductVMsByIDs (GetProducts (), idString));
        }
        #endregion

        //==================== PRIVATE ===================//
        private List<ProductVM> GetProducts () {
            return DataHelper.Instance.Products (_context, _cache, _productModel, _promModel);
        }

        private CategoryVM GetCategory (int idCate) {
            Changed keyCache;
            switch (idCate) {
                case 1:
                    keyCache = Changed.MEN_SEO;
                    break;
                case 2:
                    keyCache = Changed.WOMEN_SEO;
                    break;
                case 3:
                    keyCache = Changed.COUPLE_SEO;
                    break;
                case 4:
                    keyCache = Changed.ACCESSORIES_SEO;
                    break;
                default:
                    return null;
            }
            CategoryVM cates = CacheHelper.Get<CategoryVM> (_cache, keyCache);
            if (cates == null) {
                CategoryModel cateModel = new CategoryModel ();
                cates = cateModel.GetVM (_context, idCate);
                if (cates != null)
                    CacheHelper.Set (_cache, cates, keyCache);
            }
            return cates;
        }

        private List<WireVM> GetWires () {
            List<WireVM> wires = CacheHelper.Get<List<WireVM>> (_cache, Changed.TYPEWIRE);
            if (wires == null || wires.Count == 0) {
                WireModel wireModel = new WireModel ();
                wires = wireModel.GetListVMs (_context);
                if (wires != null)
                    CacheHelper.Set (_cache, wires, Changed.TYPEWIRE);
            }
            return wires;
        }
    }
}