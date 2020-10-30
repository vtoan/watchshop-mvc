using aspcore_watchshop.EF;
using aspcore_watchshop.Hepler;
using aspcore_watchshop.Interfaces;
using aspcore_watchshop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace aspcore_watchshop.Areas.Admin.Controllers {
    [Area ("Admin")]
    // [Authorize]
    public class WireController : Controller {
        private watchContext _context = null;
        private IWireModel _wireModel = null;

        public WireController (watchContext context, IWireModel wireModel) {
            _context = context;
            _wireModel = wireModel;
        }

        [HttpGet]
        public JsonResult ListData () {
            return Json (_wireModel.GetListVMs (_context));
        }

        [HttpPost]
        public IActionResult Add (string item) {
            if (item == "" || item == null) return BadRequest ();
            WireVM wire = DataHelper.Instance.ParserJsonTo<WireVM> (item);
            if (!_wireModel.AddModel (_context, wire)) return null;
            //Modified Data
            CacheHelper.DataUpdated (Changed.TYPEWIRE);
            return Ok ();
        }

        [HttpPut]
        public IActionResult Update (int id, string item) {
            if (id <= 0) return NotFound ();
            if (item == "" || item == null) return BadRequest ();
            WireVM wire = DataHelper.Instance.ParserJsonTo<WireVM> (item);
            if (!_wireModel.UpdateModel (_context, id, wire)) return null;
            //Modified Data
            CacheHelper.DataUpdated (Changed.TYPEWIRE);
            return Ok ();
        }

        [HttpPut]
        public IActionResult Remove (int id) {
            if (id <= 0) return NotFound ();
            if (!_wireModel.RemoveModel (_context, id)) return null;
            //Modified Data
            CacheHelper.DataUpdated (Changed.TYPEWIRE);
            return Ok ();
        }
    }
}