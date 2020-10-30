using aspcore_watchshop.EF;
using aspcore_watchshop.Hepler;
using aspcore_watchshop.Interfaces;
using aspcore_watchshop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace aspcore_watchshop.Areas.Admin.Controllers {
    [Area ("Admin")]
    [Authorize]
    public class CategoryController : Controller {
        private watchContext _context = null;
        private ICategoryModel _cateModel = null;

        public CategoryController (watchContext context, ICategoryModel categoryModel) {
            _context = context;
            _cateModel = categoryModel;
        }

        [HttpGet]
        public JsonResult ListData () {
            return Json (_cateModel.GetListVMs (_context));
        }

        [HttpPut]
        public IActionResult Update (int id, string item) {
            if (id <= 0) return NotFound ();
            if (item == "" || item == null) return BadRequest ();
            CategoryVM category = DataHelper.Instance.ParserJsonTo<CategoryVM> (item);
            if (!_cateModel.UpdateModel (_context, id, category)) return null;
            //Modified Data
            switch (id) {
                case 1:
                    CacheHelper.DataUpdated (Changed.MEN_SEO);
                    break;
                case 2:
                    CacheHelper.DataUpdated (Changed.WOMEN_SEO);
                    break;
                case 3:
                    CacheHelper.DataUpdated (Changed.COUPLE_SEO);
                    break;
                case 4:
                    CacheHelper.DataUpdated (Changed.ACCESSORIES_SEO);
                    break;
            }
            CacheHelper.DataUpdated (Changed.PRODUCT);
            return Ok ();
        }
    }
}