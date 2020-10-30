using System;
using aspcore_watchshop.EF;
using aspcore_watchshop.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace aspcore_watchshop.Areas.Admin.Controllers {
    [Area ("Admin")]
    // [Authorize]
    public class OrderController : Controller {
        private watchContext _context = null;
        private IOrderModel _orderModel = null;
        public OrderController (watchContext context, IOrderModel order) {
            _context = context;
            _orderModel = order;
        }

        [HttpGet]
        public IActionResult ListData (string start, string end) {
            if (start == null || end == null) return NotFound ();
            var dateStart = DateTime.Parse (start);
            var dateEnd = DateTime.Parse (end);
            return Json (_orderModel.GetListVMs (_context, dateStart, dateEnd));
        }

        [HttpGet]
        public IActionResult Data (int id) {
            if (id <= 0) return NotFound ();
            return Json (_orderModel.GetVM (_context, id));
        }

        [HttpGet]
        public IActionResult Find (string find) {
            if (find == "" || find == null) return NotFound ();
            return Json (_orderModel.FindVMsByString (_context, find));
        }

        [HttpGet]
        public IActionResult Detail ([FromServices] IOrderDetailModel orderDetailModel, int id) {
            if (id <= 0) return NotFound ();
            var re = orderDetailModel.GetListVMs (_context, id);
            return Json (re);
        }

        [HttpPut]
        public IActionResult UpdateStatus (int id, int stt) {
            if (id <= 0 || stt <= 0) return NotFound ();
            return _orderModel.UpdateStatus (_context, id, stt) ? Ok () : null;
        }
        // Future
        [HttpGet]
        public JsonResult Export () {
            return null;
        }

        [HttpGet]
        public JsonResult Import () {
            return null;
        }

    }
}