using aspcore_watchshop.EF;
using aspcore_watchshop.Hepler;
using aspcore_watchshop.Interfaces;
using aspcore_watchshop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace aspcore_watchshop.Areas.Admin.Controllers {
    [Area ("Admin")]
    // [Authorize]
    public class BandController : Controller {
        private watchContext _context = null;
        private IBandModel _bandModel = null;
        public BandController (watchContext context, IBandModel bandModel) {
            _context = context;
            _bandModel = bandModel;
        }

        [HttpGet]
        public JsonResult ListData () {
            return Json (_bandModel.GetListVMs (_context));
        }

        [HttpPost]
        public IActionResult Add (string item) {
            if (item == "" || item == null) return BadRequest ();
            BandVM band = DataHelper.Instance.ParserJsonTo<BandVM> (item);
            if (!_bandModel.AddModel (_context, band)) return null;
            //Modified Data
            CacheHelper.DataUpdated (Changed.BAND);
            CacheHelper.DataUpdated (Changed.PRODUCT);
            CacheHelper.DataUpdated (Changed.DISCOUNT_PRODUCT);
            return Ok ();
        }

        [HttpPut]
        public IActionResult Update (int id, string item) {
            if (id <= 0) return NotFound ();
            if (item == "" || item == null) return BadRequest ();
            BandVM band = DataHelper.Instance.ParserJsonTo<BandVM> (item);
            if (!_bandModel.UpdateModel (_context, id, band)) return null;
            //Modified Data
            CacheHelper.DataUpdated (Changed.BAND);
            CacheHelper.DataUpdated (Changed.PRODUCT);
            CacheHelper.DataUpdated (Changed.DISCOUNT_PRODUCT);
            return Ok ();
        }

        [HttpPut]
        public IActionResult Remove (int id) {
            if (id <= 0) return NotFound ();
            if (!_bandModel.RemoveModel (_context, id)) return null;
            //Modified Data
            CacheHelper.DataUpdated (Changed.BAND);
            CacheHelper.DataUpdated (Changed.PRODUCT);
            return Ok ();
        }
    }
}