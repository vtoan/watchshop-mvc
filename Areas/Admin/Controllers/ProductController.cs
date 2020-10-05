using aspcore_watchshop.EF;
using aspcore_watchshop.Models;
using Microsoft.AspNetCore.Mvc;
using aspcore_watchshop.Hepler;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace aspcore_watchshop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private watchContext _context = null;
        private IProductModel _productModel = null;
        public ProductController(watchContext context, IProductModel productModel)
        {
            _context = context;
            _productModel = productModel;
        }
        [HttpGet]
        public JsonResult Data()
        {
            return Json(_productModel.GetALLProductVMs(_context));
        }
        [HttpPost]
        public IActionResult Add(ProductVM product, PropDetailVM detail)
        {
            if (product == null || detail == null) return NotFound();
            if (!_productModel.Add(_context, product, detail)) return null;
            Cache.PRODUCT = true;
            return NoContent();
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id <= 0) return NotFound();
            if (!_productModel.Remove(_context, id)) return null;
            Cache.PRODUCT = true;
            return NoContent();
        }
        [HttpGet]
        public IActionResult Detail(int id)
        {
            if (id <= 0) return NotFound();
            return Json(_productModel.GetProdDetailVM(_context, id));
        }
        [HttpPut]
        public IActionResult UpdateStatus(int id, bool stt)
        {
            if (id <= 0) return NotFound();
            if (!_productModel.UpdateStatus(_context, id, stt)) return null;
            Cache.PRODUCT = true;
            return NoContent();
        }
        // Future
        [HttpPost]
        public IActionResult Upload(IFormFileCollection imgs)
        {
            if (imgs == null) return null;
            foreach (var img in imgs)
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\products", img.FileName);
                img.CopyToAsync(new FileStream(path, FileMode.Create));
            }
            return NoContent();
        }
        public JsonResult Submit()
        {
            return null;
        }
        public JsonResult Export()
        {
            return null;
        }
        public JsonResult Import()
        {
            return null;
        }

    }
}