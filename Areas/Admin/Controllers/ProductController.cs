using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using aspcore_watchshop.EF;
using aspcore_watchshop.Hepler;
using aspcore_watchshop.Interfaces;
using aspcore_watchshop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace aspcore_watchshop.Areas.Admin.Controllers {
    [Area ("Admin")]
    // [Authorize]
    public class ProductController : Controller {
        private watchContext _context = null;
        private IProductModel _productModel = null;
        private IProdDetailModel _prodDetailModel = null;
        public ProductController (watchContext context, IProductModel productModel, IProdDetailModel prodDetailModel) {
            _context = context;
            _productModel = productModel;
            _prodDetailModel = prodDetailModel;
        }

        [HttpGet]
        public JsonResult ListData () {
            return Json (_productModel.GetListVMs (_context));
        }

        [HttpGet]
        public IActionResult Detail (int id) {
            if (id <= 0) return NotFound ();
            var result = _prodDetailModel.GetVM (_context, id);
            if (result == null)
                return NotFound ();
            else
                return Json (result);
        }

        [HttpPut]
        public IActionResult UpdateStatus (int id, bool stt) {
            if (id <= 0) return NotFound ();
            if (!_productModel.UpdateStatus (_context, id, stt)) return null;
            //Modified Data
            CacheHelper.DataUpdated (Changed.PRODUCT);
            return Ok ();
        }

        [HttpPost]
        public IActionResult Add (string item) {
            if (item == null || item == "") return BadRequest ();
            var helper = DataHelper.Instance;
            //Convert to product
            ProductVM product = helper.ParserJsonTo<ProductVM> (item);
            ProdDetailVM detail = helper.ParserJsonTo<ProdDetailVM> (item);
            if (!_productModel.AddModel (_context, product, detail)) return null;
            //Modified Data
            CacheHelper.DataUpdated (Changed.PRODUCT);
            return Ok ();
        }

        [HttpPut]
        public IActionResult Update (int id, string item) {
            if (id <= 0) return NotFound ();
            if (item == null || item == "") return BadRequest ();
            var helper = DataHelper.Instance;
            //Convert to product
            ProductVM product = helper.ParserJsonTo<ProductVM> (item);
            ProdDetailVM detail = helper.ParserJsonTo<ProdDetailVM> (item);
            //Update Product
            if (!_productModel.UpdateModel (_context, id, product)) return null;;
            _prodDetailModel.UpdateModel (_context, id, detail);
            //Modified Data
            CacheHelper.DataUpdated (Changed.PRODUCT);
            return Ok ();
        }

        [HttpPut]
        public IActionResult Delete (int id) {
            if (id <= 0) return NotFound ();
            //Remove Product
            if (!_productModel.RemoveModel (_context, id)) return NotFound ("Sản phẩm đã đc xoá");
            //Remove Product Detail
            // _prodDetailModel.RemoveModel (_context, id); // pending
            //Modified Data
            CacheHelper.DataUpdated (Changed.PRODUCT);
            return Ok ();
        }

        [HttpPost]
        public IActionResult Upload (IFormFileCollection imgs) {
            if (imgs == null) return BadRequest ();
            foreach (var img in imgs) {
                string path = Path.Combine (Directory.GetCurrentDirectory (), @"wwwroot\products", img.FileName);
                img.CopyToAsync (new FileStream (path, FileMode.OpenOrCreate));
            }
            return Ok ();
        }

        //============== Future ==================
        public JsonResult Export () {
            return null;
        }
        public JsonResult Import () {
            return null;
        }

    }
}