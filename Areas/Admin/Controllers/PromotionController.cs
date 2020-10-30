using System.Collections.Generic;
using aspcore_watchshop.EF;
using aspcore_watchshop.Hepler;
using aspcore_watchshop.Interfaces;
using aspcore_watchshop.Models;
using aspcore_watchshop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace aspcore_watchshop.Areas.Admin.Controllers {
    [Area ("Admin")]
    [Authorize]
    public class PromotionController : Controller {
        private watchContext _context = null;
        private IPromModel _promModel = null;
        private IPromProductModel _promProductModel = null;
        private IPromBillModel _promBillModel = null;

        public PromotionController (
            watchContext context,
            IPromModel promModel,
            IPromProductModel promProductModel,
            IPromBillModel promBillModel) {
            _context = context;
            _promModel = promModel;
            _promProductModel = promProductModel;
            _promBillModel = promBillModel;
        }

        [HttpGet]
        public JsonResult ListData () {
            return Json (_promModel.GetListVMs (_context));
        }

        [HttpGet]
        public JsonResult Product () {
            //new==================================================
            var re = _promModel.GetListVMProducts (_context);
            return Json (re);
        }

        [HttpGet]
        public IActionResult Data (int id) {
            if (id <= 0) return NotFound ();
            return Json (_promModel.GetVM (_context, id));
        }

        [HttpPut]
        public IActionResult UpdateStatus (int id, bool stt) {
            if (id <= 0) return NotFound ();
            if (!_promModel.UpdateStatus (_context, id, stt)) return null;
            //Modified Data
            CacheHelper.DataUpdated (Changed.PRODUCT);
            return Ok ();
        }

        [HttpPost]
        public IActionResult AddForProduct (string item) {
            if (item == null || item == "") return BadRequest ();
            //Convert to prom
            PromProductVM promProduct = DataHelper.Instance.ParserJsonTo<PromProductVM> (item);
            if (!_promModel.AddModel (_context, promProduct, PromType.PRODUCT)) return null;
            //Modified Data
            CacheHelper.DataUpdated (Changed.PRODUCT);
            return Ok ();
        }

        [HttpPost]
        public IActionResult AddForBill (string item) {
            if (item == null || item == "") return BadRequest ();
            //Convert to prom
            PromBillVM promBill = DataHelper.Instance.ParserJsonTo<PromBillVM> (item);
            if (!_promModel.AddModel (_context, promBill, PromType.BILL)) return null;
            //Modified Data
            return Ok ();
        }

        [HttpPut]
        public IActionResult UpdateForBill (int id, string item) {
            if (id <= 0) return NotFound ();
            if (item == null || item == "") return BadRequest ();
            var helper = DataHelper.Instance;
            //Convert to prom
            PromBillVM promBill = helper.ParserJsonTo<PromBillVM> (item);
            //Update
            if (!_promModel.UpdateModel (_context, id, promBill)) return null;
            if (!_promBillModel.UpdateModel (_context, id, promBill)) return null;
            return Ok ();
        }

        [HttpPut]
        public IActionResult UpdateForProduct (int id, string item) {
            if (id <= 0) return NotFound ();
            if (item == null || item == "") return BadRequest ();
            var helper = DataHelper.Instance;
            //Convert to product
            PromProductVM promProduct = helper.ParserJsonTo<PromProductVM> (item);
            //Update
            if (!_promModel.UpdateModel (_context, id, promProduct)) return null;
            if (!_promProductModel.UpdateModel (_context, id, promProduct)) return null;
            //Modified Data
            CacheHelper.DataUpdated (Changed.PRODUCT);
            return Ok ();
        }

        [HttpPut]
        public IActionResult Remove (int id) {
            if (id <= 0) return NotFound ();
            if (!_promModel.RemoveModel (_context, id)) return null;
            //Modified Data
            CacheHelper.DataUpdated (Changed.PRODUCT);
            return Ok ();
        }
    }
}