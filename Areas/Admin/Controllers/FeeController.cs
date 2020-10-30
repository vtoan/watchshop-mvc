using aspcore_watchshop.EF;
using aspcore_watchshop.Hepler;
using aspcore_watchshop.Interfaces;
using aspcore_watchshop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace aspcore_watchshop.Areas.Admin.Controllers {
    [Area ("Admin")]
    [Authorize]
    public class FeeController : Controller {
        private watchContext _context = null;
        private IFeeModel _feeModel = null;

        public FeeController (watchContext context, IFeeModel feeModel) {
            _context = context;
            _feeModel = feeModel;
        }

        [HttpGet]
        public JsonResult ListData () {
            return Json (_feeModel.GetListVMs (_context));
        }

        [HttpPost]
        public IActionResult Add (string item) {
            if (item == "" || item == null) return BadRequest ();
            FeeVM fee = DataHelper.Instance.ParserJsonTo<FeeVM> (item);
            if (!_feeModel.AddModel (_context, fee)) return null;
            //Modified Data
            CacheHelper.DataUpdated (Changed.FEE);
            return Ok ();
        }

        [HttpPut]
        public IActionResult Update (int id, string item) {
            if (id <= 0) return NotFound ();
            if (item == "" || item == null) return BadRequest ();
            FeeVM fee = DataHelper.Instance.ParserJsonTo<FeeVM> (item);
            if (!_feeModel.UpdateModel (_context, id, fee)) return null;
            //Modified Data
            CacheHelper.DataUpdated (Changed.FEE);
            return Ok ();
        }

        [HttpPut]
        public IActionResult Remove (int id) {
            if (id <= 0) return NotFound ();
            if (!_feeModel.RemoveModel (_context, id)) return null;
            //Modified Data
            CacheHelper.DataUpdated (Changed.FEE);
            return Ok ();
        }
    }
}