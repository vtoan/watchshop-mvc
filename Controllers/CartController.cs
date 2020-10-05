using aspcore_watchshop.EF;
using aspcore_watchshop.Hepler;
using aspcore_watchshop.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace aspcore_watchshop.Controllers
{
    public class CartController : Controller
    {
        private watchContext _context = null;
        private IProductModel _productModel = null;
        private IMemoryCache _cache = null;
        private IPromModel _promModel = null;

        public CartController(watchContext ctext, IMemoryCache cache, IProductModel productModel, IPromModel promModel)
        {
            _context = ctext;
            _productModel = productModel;
            _cache = cache;
            _promModel = promModel;
        }

        public IActionResult Index()
        {
            LayoutData.NamePage = "Giỏ hàng";
            LayoutData.Title = "Giỏ hàng";
            ViewBag.PageCode = -1;
            ViewBag.Fees = GetFees();
            return View();
        }

        public IActionResult Confirm(OrderVM orderVM, string items)
        {
            if (!ModelState.IsValid)
            {
                foreach (var field in ModelState)
                {
                    orderVM.GetType().GetProperty("CustomerProvince").SetValue(orderVM, "");
                    // Remove values invalid
                    if (field.Value.ValidationState == ModelValidationState.Invalid)
                        orderVM.GetType().GetProperty(field.Key).SetValue(orderVM, "");
                }
                return View("Index", orderVM);
            }
            if (!CheckOrderSubmit(items)) return View(orderVM);
            //
            LayoutData.NamePage = "Xác nhận đơn hàng";
            LayoutData.Title = "Xác nhận đơn hàng";
            ViewBag.Fees = GetFees();
            return View(orderVM);
        }

        public IActionResult Submit([FromServices] IOrderModel orderModel, OrderVM orderVM, string items)
        {
            if (!CheckOrderSubmit(items) || orderVM == null) return View(orderVM);
            var lsItems = JsonToList(items);
            return orderModel.AddOrderVM(_context, orderVM, lsItems, GetFees(), FindBillPromotion(ref lsItems)) ? RedirectToAction("Status") : null;
        }

        public IActionResult Status()
        {
            LayoutData.NamePage = "Đặt hàng thành công";
            LayoutData.Title = "Thành công";
            return View();
        }

        //================ AJAX ================
        public JsonResult GetBillPromotion(string items)
        {
            var lsItems = JsonToList(items);
            return Json(FindBillPromotion(ref lsItems));
        }

        //================ Helper ================
        private string FindBillPromotion(ref List<OrderDetailVM> lsItems)
        {
            string response = "";
            var promBills = _promModel.GetPromBillVMs(_context);
            if (promBills == null || promBills.Count == 0) return response;
            // Determine Totol, Quantity item
            int total = 0;
            int quantity = 0;
            foreach (var i in lsItems)
            {
                quantity += (int)i.quantity;
                total += i.price * i.quantity;
            };
            // Check Promtion
            foreach (var prom in promBills)
            {
                if (quantity >= prom.ConditionItem || total >= prom.ConditionAmount)
                {
                    response = prom.Discount != 0 ? prom.Discount.ToString() : prom.ItemFree;
                    break;
                }
            }
            return response;
        }

        private List<OrderDetailVM> JsonToList(string items)
        {
            var listItems = JsonSerializer.Deserialize<List<OrderDetailVM>>(items);
            // Calculator total items in cart
            _productModel.GetProductInCarts(DataHelper.Products(_context, _cache, _productModel, _promModel), ref listItems);
            return listItems;
        }

        private bool CheckOrderSubmit(string items)
        {
            if (items == "" || items == "[]") return false;
            return true;
        }

        private List<FeeVM> GetFees()
        {
            List<FeeVM> feeVMs = Cache.Get<List<FeeVM>>(_cache, CacheKey.FEES);
            if (feeVMs == null || feeVMs.Count == 0)
            {
                FeeModel feeModel = new FeeModel();
                feeVMs = feeModel.GetFeeVMs(_context);
                if (feeVMs != null)
                    Cache.Set(_cache, feeVMs, CacheKey.FEES);
            }
            return feeVMs;
        }
    }
}