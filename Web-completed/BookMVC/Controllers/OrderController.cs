using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookMVC.Dao;
using BookMVC.Entities;
using BookMVC.Models;
namespace BookMVC.Controllers
{
    public class OrderController : Controller
    {
          // Đơn hàng
          //
          public decimal? TotalQuantity
          {
               get
               {
                    var userID = (long)Session["UserID"];
                    var cart = Session["Cart"] as List<CartItemDetail>;
                    var lsSelected = cart.Where(x => x.Selected == true).ToList();
                    if (lsSelected == null || lsSelected.Count() == 0)
                         return 0;
                    return lsSelected.Count();
               }
          }

          // Tong tien
          public decimal? TotalSelectedPrice
          {
               get
               {
                    var userID = (long)Session["UserID"];
                    var cart = Session["Cart"] as List<CartItemDetail>;
                    var lsSelected = cart.Where(x => x.Selected == true).ToList();
                    if (lsSelected == null || lsSelected.Count() == 0)
                         return 0;
                    decimal? sum = 0;
                    foreach (var i in lsSelected)
                    {
                         sum += (i.Price * i.Quantity);
                    }
                    return sum;
               }
          }

          public decimal? TotalPromotion
          {
               get
               {
                    var userID = (long)Session["UserID"];
                    var cart = Session["Cart"] as List<CartItemDetail>;
                    var lsSelected = cart.Where(x => x.Selected == true).ToList();
                    if (lsSelected == null || lsSelected.Count() == 0)
                         return 0;
                    decimal? sum = 0;
                    foreach (var i in lsSelected)
                    {
                         sum += ((i.Price - i.PromotionPrice) * i.Quantity);
                    }
                    return sum;
               }
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult OrderDetail(FormCollection form)
          {
               if(Session["UserID"] == null)
               {
                    return Json(new
                    {
                         check = 0,
                         message = "Bạn phải đăng nhập để tiến hành thanh toán"
                    });
               }
               var userID = (long)Session["UserID"];
               var cart = Session["Cart"] as List<CartItemDetail>;
               if (cart == null)
                    return RedirectToAction("Index", "Home");
               var lsSelected = cart.Where(x => x.Selected == true).ToList();
               if (lsSelected.Sum(x => x.Quantity) > 0)
               {
                    // Tạo giỏ hàng mới
                    var order = new OrderDao().CreateOrder(userID);
                    // Tạo danh sách chi tiết giỏ hàng
                    List<Order_Detail> orderDetail = new List<Order_Detail>();
                    foreach (var i in lsSelected)
                    {
                         orderDetail.Add(new Order_Detail()
                         {
                              BookID = i.ItemID,
                              OrderID = order.ID,
                              Price = i.PromotionPrice,
                              Quantity = i.Quantity
                         });
                    };
                    // Tính tổng tiền đơn hàng
                    order.TotalPrice = orderDetail.Sum(x => (x.Price * x.Quantity));
                    Session["TotalOrderPrice"] = orderDetail.Sum(x => (x.Price * x.Quantity));
                    Session["Order"] = order;
                    Session["Order_Detail"] = orderDetail;
                    return Json(new
                    {
                         check = 1,
                         url = "/Order/CheckOut"
                    });
               }
               else
               {
                    return Json(new
                    {
                         check = 2,
                         message = "Bạn chưa chọn gì cho đơn hàng cả!\n Hãy bắt đầu với 1 cuốn sách hay từ giỏ hàng nào!",
                         url = "/Cart/Index"
                    });
               }
          }
          // Kiểm tra thông tin người nhận
          public ActionResult CheckOut()
          {
               // Kiem tra gio hang
               var cart = Session["Cart"] as List<CartItemDetail>;
               if (cart == null)
                    return RedirectToAction("Index", "Home");
               // Kiem tra xem da chon san pham nao chua
               var lsSelected = cart.Where(x => x.Selected == true).ToList();
               if (lsSelected.Sum(x => x.Quantity) > 0)
               {
                    // Kiem tra thong tin khach hang
                    var order = TempData["checkout"] as Order;
                    if (order != null)
                    {
                         var modelstate = TempData["statecheckout"] as ModelStateDictionary;
                         if (modelstate != null)
                              foreach (var e in modelstate)
                                   foreach (var err in e.Value.Errors)
                                   {
                                        if (err != null)
                                             ModelState.AddModelError(e.Key, err.ErrorMessage);
                                   }
                    }
                    else
                    {
                         // Kiem tra xem co don hang chua
                         if (Session["Order"] == null)
                         {
                              var userID = (long)Session["UserID"];
                              order = new OrderDao().CreateOrder(userID);
                              Session["Order"] = order;
                         }
                         else
                              order = Session["Order"] as Order;
                    }
                    ViewBag.totalPrice = TotalSelectedPrice;
                    ViewBag.totalQuantity = TotalQuantity;
                    ViewBag.totalPromotion = TotalPromotion;
                    return View(order);
               }
               TempData["message"] = "Bạn chưa chọn gì cho đơn hàng cả!\n Hãy bắt đầu với 1 cuốn sách hay từ giỏ hàng nào!";
               return RedirectToAction("Index", "Cart");
          }
          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult CheckOut(Order o)
          {
               if (ModelState.IsValid)
               {
                    // Cập nhật thông tin người nhận
                    var order = Session["Order"] as Order;
                    order.ShipName = o.ShipName;
                    order.ShipMobile = o.ShipMobile;
                    order.ShipEmail = o.ShipEmail;
                    order.ShipAdress = o.ShipAdress;
                    Session["Order"] = order;
                    return RedirectToAction("CheckOut2");
               }
               else
               {
                    // Lỗi
                    TempData["checkout"] = o;
                    TempData["statecheckout"] = ModelState;
                    return RedirectToAction("CheckOut");
               }
          }
          // Kiểm tra hình thức vận chuyển
          public ActionResult CheckOut2()
          {
               var lsShippingType = new ShipDao().ListAll();
               var order = Session["Order"] as Order;
               if (order.ShipTypeID != null)
               {
                    ViewBag.ShipTypeChoised = order.ShipTypeID;
                    ViewBag.shippingCost = new ShipDao().TakeByID((long)order.ShipTypeID).Cost;
               }
               ViewBag.totalPrice = TotalSelectedPrice;
               ViewBag.totalQuantity = TotalQuantity;
               ViewBag.totalPromotion = TotalPromotion;
               return View(lsShippingType);
          }
          [HttpPost]
          public JsonResult CheckOut2Demo(long? shippingType)
          {
               var order = Session["Order"] as Order;
               order.ShipTypeID = shippingType;
               var shippingcost = new ShipDao().TakeByID((long)shippingType).Cost;
               order.TotalPrice = (decimal)Session["TotalOrderPrice"] + shippingcost;
               Session["Order"] = order;
               return Json(new {
                    shippingCost = shippingcost.ToString("N0"),
                    realPrice = ((decimal)(TotalSelectedPrice - TotalPromotion + shippingcost)).ToString("N0")
               });
          }
          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult CheckOut2(long? shippingType)
          {
               if(shippingType == null)
               {
                    TempData["message"] = "Hãy chọn 1 hình thức vận chuyển để tiếp tục";
                    return RedirectToAction("CheckOut2");
               }
               var order = Session["Order"] as Order;
               order.ShipTypeID = shippingType;
               var shippingcost = new ShipDao().TakeByID((long)shippingType).Cost;
               order.TotalPrice = (decimal)Session["TotalOrderPrice"] + shippingcost;
               Session["Order"] = order;
               return RedirectToAction("CheckOut3");
          }

          public ActionResult CheckOut3()
          {
               var order = Session["Order"] as Order;
               ViewBag.ShipTypeChoised = order.ShipTypeID;
               ViewBag.shippingCost = new ShipDao().TakeByID((long)order.ShipTypeID).Cost;
               ViewBag.totalPrice = TotalSelectedPrice;
               ViewBag.totalQuantity = TotalQuantity;
               ViewBag.totalPromotion = TotalPromotion;
               return View();
          }
          // Kiểm tra phương thức thanh toán
          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult CheckOut3(long? paymentType)
          {
               return RedirectToAction("CheckOut4");
          }
          // Kiểm tra chi tiết đơn hàng
          public ActionResult CheckOut4()
          {
               var lsSelected = Session["Cart"] as List<CartItemDetail>;
               var order_detail = new Order_DetailDao().OrderItems(lsSelected);
               Session["Order_Detail"] = order_detail;

               var order = Session["Order"] as Order;
               ViewBag.ShipTypeChoised = order.ShipTypeID;
               ViewBag.shippingCost = new ShipDao().TakeByID((long)order.ShipTypeID).Cost;
               ViewBag.totalPrice = TotalSelectedPrice;
               ViewBag.totalQuantity = TotalQuantity;
               ViewBag.totalPromotion = TotalPromotion;

               return View(order_detail);
          }
          // Lưu đơn hàng
          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult SaveOrder()
          {
               
               var orderdetaildao = new Order_DetailDao();
               var cartitemdao = new CartItemDao();
               var order = Session["Order"] as Order;
               var detail = Session["Order_Detail"] as List<OrderDetailViewModel>;
               var orderID = new OrderDao().SaveOrder(order);
               foreach (var i in detail)
               {
                    orderdetaildao.SaveOrder_Detail(orderID, i.orderdetail);
                    cartitemdao.DeleteItem((long)order.CreatID,i.book.ID);
               }
               Session["Cart"] = null;
               Session["Order"] = null;
               Session["Order_Detail"] = null;
               Session["TotalOrderPrice"] = null;
               return RedirectToAction("Index", "Home");
          }


          // Cập nhật 
          [HttpPost]
          [ValidateAntiForgeryToken]
          public JsonResult Coupon(string Serial, string total, string check)
          {
               var coupon = new CouponDao().TakeCoupon(Serial);
               var totalafterdiscount = decimal.Parse(total) - (decimal)coupon.Discount;
               if (coupon != null)
               {
                    if (coupon.StartDate < DateTime.Now && DateTime.Now < coupon.EndDate)
                         return Json(new
                         {
                              status = true,
                              discount = ((decimal)coupon.Discount).ToString("N0"),
                              newtotal = totalafterdiscount.ToString("N0")
                         });
               }
               return Json(new
               {
                    status = false,
                    discount = 0
               });
          }
     }
}