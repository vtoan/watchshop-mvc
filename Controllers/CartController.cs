using System;
using System.Collections.Generic;
using System.Text.Json;
using aspcore_watchshop.EF;
using aspcore_watchshop.Hepler;
using aspcore_watchshop.Interfaces;
using aspcore_watchshop.Models;
using aspcore_watchshop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Caching.Memory;

namespace aspcore_watchshop.Controllers {
    public class CartController : Controller {
        private watchContext _context = null;
        private IMemoryCache _cache = null;
        private IPromModel _promModel = null;

        public CartController (
            watchContext ctext,
            IMemoryCache cache,
            IPromModel promModel,
            IPromBillModel promBillModel) {
            _context = ctext;
            _cache = cache;
            _promModel = promModel;
        }

        [HttpGet]
        public IActionResult Index () {
            LayoutData.Instance.SetPageTitle ("Giỏ hàng");
            ViewBag.Fees = GetFees ();
            return View ();
        }

        [HttpPost]
        [ActionName ("xac-nhan-don-hang")]
        public IActionResult Confirm (OrderVM orderVM, string items) {
            if (!ModelState.IsValid) {
                foreach (var field in ModelState) {
                    orderVM.GetType ().GetProperty ("CustomerProvince").SetValue (orderVM, "");
                    // Remove values invalid
                    if (field.Value.ValidationState == ModelValidationState.Invalid)
                        orderVM.GetType ().GetProperty (field.Key).SetValue (orderVM, "");
                }
                return View ("Index", orderVM);
            }
            if (!CheckOrderSubmit (items)) return View ("Index", orderVM);
            //
            LayoutData.Instance.SetPageTitle ("Xác nhận đơn hàng");
            ViewBag.Fees = GetFees ();
            return View ("Confirm", orderVM);
        }

        [HttpPost]
        [ActionName ("dat-hang")]
        public IActionResult Submit (
            [FromServices] IOrderModel orderModel, [FromServices] IOrderDetailModel orderDetailModel, [FromServices] IProductModel productModel,
            OrderVM orderVM,
            string items) {
            if (!ModelState.IsValid || !CheckOrderSubmit (items)) return View ("Confirm", orderVM);
            var helper = DataHelper.Instance;
            // Add Order
            var lsItems = helper.ParserJsonTo<List<OrderDetailVM>> (items);
            // Calculator total items in cart
            productModel.GetProductInCarts (DataHelper.Instance.Products (_context, _cache, productModel, _promModel), ref lsItems);
            orderVM.DateCreated = DateTime.Now;
            orderVM.Fees = helper.ParserObjToJson (GetFees ());
            orderVM.Promotion = FindBillPromotion (lsItems).ToString ();
            var result = orderModel.AddModel (_context, orderVM, lsItems);
            if (result != 0) {
                LayoutData.Instance.SetPageTitle ("Đặt hàng thành công");
                ViewBag.OrderId = result;
                return View ("CheckoutSuccess");
            }
            return StatusCode (500);
        }

        [HttpGet]
        [ActionName ("kiem-tra")]
        public IActionResult StatusOrder () {
            return View ("StatusOrder");
        }

        [HttpGet, HttpPost]
        [ActionName ("trang-thai")]
        public IActionResult StatusOrder ([FromServices] IOrderModel orderModel, string idOrder) {
            LayoutData.Instance.SetPageTitle ("Thông tin đơn hàng");
            int id;
            OrderVM orderResult = null;
            if (Int32.TryParse (idOrder, out id)) orderResult = orderModel.GetVM (_context, id);
            if (id == 0 || orderResult == null) {
                ViewBag.Message = "Không tìm thấy đơn hàng ' " + idOrder + " '";
                return View ("StatusOrder");
            }
            ViewBag.Status = true;
            return View ("Confirm", orderResult);
        }

        //================ AJAX ================
        [HttpGet]
        public JsonResult GetBillPromotion () {
            var result = _promModel.GetListVMBills (_context);
            return Json (result);
        }

        [HttpGet]
        public JsonResult GetOrderItems ([FromServices] IOrderDetailModel orderDetailModel, int id) {
            if (id <= 0) return Json (null);
            var result = orderDetailModel.GetListVMs (_context, id);
            var s = DataHelper.Instance.ParserObjToJson (result);
            return Json (s);
        }

        //================ Helper ================
        private double FindBillPromotion (List<OrderDetailVM> lsItems) {
            int total = 0;
            int quantity = 0;
            var proms = _promModel.GetListVMBills (_context);
            if (proms == null || proms.Count == 0) return 0;
            // Determine Totol, Quantity item 
            foreach (var i in lsItems) {
                quantity += (int) i.Quantity;
                total += i.Price * i.Quantity;
            };
            // Check Promtion
            foreach (var prom in proms) {
                if (prom is PromBillVM) {
                    var promBill = (PromBillVM) prom;
                    if ((promBill.ConditionAmount != null && total >= promBill.ConditionAmount) ||
                        (promBill.ConditionItem != null && quantity >= promBill.ConditionItem)) {
                        return prom.Discount;
                    }
                }
            }
            return 0;
        }

        private bool CheckOrderSubmit (string items) {
            if (items == null || items == "" || items == "[]") return false;
            return true;
        }

        private List<FeeVM> GetFees () {
            List<FeeVM> feeVMs = CacheHelper.Get<List<FeeVM>> (_cache, Changed.FEE);
            if (feeVMs == null || feeVMs.Count == 0) {
                FeeModel feeModel = new FeeModel ();
                feeVMs = feeModel.GetListVMs (_context);
                if (feeVMs != null)
                    CacheHelper.Set (_cache, feeVMs, Changed.FEE);
            }
            return feeVMs;
        }
    }
}