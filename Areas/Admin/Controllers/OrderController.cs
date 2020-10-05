using System;
using aspcore_watchshop.EF;
using aspcore_watchshop.Models;
using Microsoft.AspNetCore.Mvc;

namespace aspcore_watchshop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private watchContext _context = null;
        private IOrderModel _orderModel = null;
        public OrderController(watchContext context, IOrderModel order)
        {
            _context = context;
            _orderModel = order;
        }
        [HttpGet]
        public IActionResult Data(DateTime start, DateTime end)
        {
            if (start == null || end == null) return NotFound();
            return Json(_orderModel.GetOrders(_context, start, end));
        }
        [HttpPut]
        public IActionResult UpdateStatus(int id, int stt)
        {
            if (id <= 0 || stt <= 0) return NotFound();
            return _orderModel.UpdateStatus(_context, id, stt) ? NoContent() : null;
        }

        [HttpGet]
        public IActionResult Find(int find)
        {
            if (find <= 0) return NotFound();
            return Json(_orderModel.FindOrders(_context, find));
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            if (id <= 0) return NotFound();
            return Json(_orderModel.GetOrderDetailVMs(_context, id));
        }

        [HttpGet]
        public IActionResult Order(int id)
        {
            if (id <= 0) return NotFound();
            return Json(_orderModel.GetOrderByID(_context, id));
        }

        // Future
        [HttpGet]
        public JsonResult Export()
        {
            return null;
        }
        [HttpGet]
        public JsonResult Import()
        {
            return null;
        }

    }
}