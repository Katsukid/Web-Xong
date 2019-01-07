using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookMVC.Entities;
using BookMVC.Dao;
using BookMVC.Models;
using System.IO;
using System.Web.Routing;

namespace BookMVC.Controllers
{
     public class CartController : Controller
     {
          BookMVCDbContext db = new BookMVCDbContext();
          // Tạo giỏ hàng
          public List<CartItemDetail> CreateCart()
          {
               var cartDao = new CartItemDao();
               var currentCart = new List<CartItemDetail>();
               var userID = Session["UserId"] as long?;
               // Nếu chưa đăng nhập
               if(userID == null)
               {
                    Session["Cart"] = currentCart;
                    return currentCart;
               }
               // Nếu đăng nhập rồi
               else
               {
                    currentCart = cartDao.TakeCartFromDB(userID);
                    Session["Cart"] = currentCart;
                    return currentCart;
               }
          }
          
          
          // Lấy danh sách item trong Cart 
          public List<CartItemDetail> TakeCart()
          {
               return Session["Cart"] as List<CartItemDetail>;
          }
          // Tong so san pham
          public decimal? TotalQuantity
          {
               get
               {
                    var cart = TakeCart();
                    if (cart == null || cart.Count == 0)
                         return 0;
                    return cart.Count;
               }
          }

          // Tổng giá trị giỏ hàng
          public decimal? TotalPrice
          {
               get
               {
                    var cart = TakeCart();
                    if (cart == null || cart.Count == 0)
                         return 0;
                    decimal? sum = 0;
                    foreach (var i in cart)
                    {
                         sum += (i.PromotionPrice * i.Quantity);
                    }
                    return sum;
               }
          }

          // Tổng tiền đơn hàng dự kiến
          public decimal? TotalSelectedPrice
          {
               get
               {
                    var cart = TakeCart();
                    if (cart == null || cart.Count == 0)
                         return 0;
                    var selected = cart.Where(x => x.Selected == true).ToList();
                    decimal? sum = 0;
                    foreach (var i in selected)
                    {
                         sum += (i.Price * i.Quantity);
                    }
                    return sum;
               }
          }
          // Tổng khuyến mãi
          public decimal? TotalPromotion
          {
               get
               {
                    var cart = TakeCart();
                    if (cart == null || cart.Count == 0)
                         return 0;
                    var selected = cart.Where(x => x.Selected == true).ToList();
                    decimal? sum = 0;
                    foreach (var i in cart)
                    {
                         sum += ((i.Price-i.PromotionPrice) * i.Quantity);
                    }
                    return sum;
               }
          }
          // So luong sach trong gio hang hien thi tren TopNavBar va khoi tao Session["Cart"]
          public ActionResult CartPartial()
          {
               // Nếu như đã đăng nhập và chưa có giỏ hàng
               if (Session["Cart"] == null)
                    CreateCart();
               ViewBag.TotalQuantity = TotalQuantity;
               ViewBag.TotalPrice = TotalPrice;
               return PartialView();
          }

          // Cập nhật số lượng giỏ khi thêm, sửa, xóa giỏ
          [HttpPost]
          public JsonResult Cart()
          {
               if (Session["Cart"] == null)
                    CreateCart();
               return Json(new
               {
                    totalprice = TotalPrice,
                    totalquantity = TotalQuantity });
          }

          // Danh sach sach trong gio hang
          public ActionResult Index(long? listShippingType)
          {
               //if (Session["UserID"] == null)
               //{
               //     TempData["message"] = "Hãy đăng nhập trước";
               //     return RedirectToAction("Index", "Home");
               //}
               if (TotalQuantity == 0)
               {
                    TempData["message"] = "Không có sản phẩm nào trong giỏ hàng, hãy thêm gì đó vào giỏ hàng nào!!";
                    return RedirectToAction("Index", "Home");
               }
               if (listShippingType == null)
               {
                    listShippingType = 1;
               }
               var group = new IndexCartModel
               {
                    cart = TakeCart(),
                    totalPrice = TotalSelectedPrice,
                    totalQuantity = TotalQuantity,
                    totalPromotion = TotalPromotion,
                    listHotBook = new BookDao().ListHotBook(20,4)
               };
               return View(group);
          }

          // Chuyen 1 partial view sang dang text Html de truyen vào ajax
          public string RenderRazorViewToString(string viewName, object model)
          {
               ViewData.Model = model;
               using (var sw = new StringWriter())
               {
                    var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                    var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                    viewResult.View.Render(viewContext, sw);
                    viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                    return sw.GetStringBuilder().ToString();
               }
          }

          // + Thêm Item vào Cart
          // - Thêm trong giao diện giỏ
          [HttpPost]
          public ActionResult AddCartItemInCart(long ItemID)
          {
               var inventory = new BookDao().getInventory(ItemID);
               if (inventory == 0)
                    return Json(new
                    {
                         stringView = "Số lượng hàng trong kho không đủ!",
                         status = false
                    });
               if (Session["UserID"] != null)
               {
                    var UserID = Session["UserID"] as long?;
                    var check = new CartItemDao().AddItem((long)UserID, ItemID, null);
                    Session["Cart"] = CreateCart();
               }
               else
               {
                    var curCart = TakeCart();
                    var check = 0;
                    foreach(var i in curCart)
                    {
                         if(i.ItemID == ItemID)
                         {
                              i.Quantity += 1;
                              check = 1;
                              break;
                         }
                    }
                    if(check == 0)
                    {
                         var item = new CartItemDao().TakeItemDetail(ItemID);
                         curCart.Add(item);
                    }
                    Session["Cart"] = curCart;
               }
               var group = new IndexCartModel
               {
                    cart = Session["Cart"] as List<CartItemDetail>,
                    totalPrice = TotalSelectedPrice,
                    totalQuantity = TotalQuantity,
                    totalPromotion = TotalPromotion,
                    listHotBook = new BookDao().ListHotBook(20,4)
               };
               return PartialView("ContentCart", group);
          }

          // - Thêm ở ngoài giao diện giỏ
          [HttpPost]
          public ActionResult AddCartItem(long ItemID, int? Quantity)
          {
               int check;
               var inventory = new BookDao().getInventory(ItemID);
               if (inventory == 0)
                    return Json(0);
               if (Session["UserID"] != null)
               {
                    var UserID = Session["UserID"] as long?;
                    check = new CartItemDao().AddItem((long)UserID, ItemID, null);
                    Session["Cart"] = CreateCart();
               }
               else
               {
                    var curCart = TakeCart();
                    check = 0;
                    foreach (var i in curCart)
                    {
                         if (i.ItemID == ItemID)
                         {
                              i.Quantity += 1;
                              check = 1;
                              break;
                         }
                    }
                    if (check == 0)
                    {
                         var item = new CartItemDao().TakeItemDetail(ItemID);
                         curCart.Add(item);
                    }
                    Session["Cart"] = curCart;
               }
               return Json(2);
          }

          // + Xoa Item trong Cart
          [HttpPost]
          public JsonResult DeleteCartItem(long? ItemID)
          {
               int check = 0;
               var dao = new CartItemDao();
               if (Session["UserID"] != null)
               {
                    var userID = (long)Session["UserID"];
                    var item = dao.TakeItem(userID, (long)ItemID);
                    if (item != null)
                    {
                         new CartItemDao().DeleteItem(userID, ItemID);
                         check = 1;
                    }
                    Session["Cart"] = CreateCart();
               }
               else
               {
                    var curCart = TakeCart();
                    foreach (var i in curCart)
                    {
                         if (i.ItemID == ItemID)
                         {
                              curCart.Remove(i);
                              check = 1;
                              break;
                         }
                    }
                    Session["Cart"] = curCart;
               }
               if (check == 0)
               {
                    return Json(new
                    {
                         error = "Khong co san pham nay trong gio hang",
                         status = true
                    });
               }
               var group = new IndexCartModel
               {
                    cart = Session["Cart"] as List<CartItemDetail>,
                    totalPrice = TotalSelectedPrice,
                    totalQuantity = TotalQuantity,
                    totalPromotion = TotalPromotion,
                    listHotBook = new BookDao().ListHotBook(20, 4)
               };
               return Json(new
               {
                    stringView = RenderRazorViewToString("ContentCart", group),
                    status = true
               });

          }

          // Cập nhật giỏ hàng
          //[HttpPost]
          //[ValidateAntiForgeryToken]
          //public ActionResult UpdateCartAll(FormCollection form)
          //{
          //     var cartdao = new CartItemDao();
          //     var UserID = Session["UserID"] as long?;
          //     var cart = TakeCart();
          //     foreach (var i in cart)
          //     {
          //          var namebox = "Quantity+" + i.ItemID;
          //          var Quantity = int.Parse(form[namebox].ToString());
          //          if (cartdao.TakeItem(UserID, i.ItemID) != null)
          //          {
          //               cartdao.UpdateItem(UserID, i.ItemID, Quantity);
          //               i.Quantity = Quantity;
          //          }
          //          var namebox2 = "Selected+" + i.ItemID;
          //          if (form[namebox2] == null )
          //               i.Selected = false;
          //          else i.Selected = true;
          //     }
          //     Session["Cart"] = cart;
          //     var group = new IndexCartModel
          //     {
          //          cart = cart,
          //          totalPrice = TotalSelectedPrice,
          //          totalQuantity = TotalQuantity,
          //          totalPromotion = TotalPromotion,
          //          listHotBook = new BookDao().ListHotBook(20,4)
          //     };
          //     return Json(new
          //     {
          //          stringView = RenderRazorViewToString("ContentCart",group),
          //          status = true
          //     });
          //}
         
          [HttpPost]
          public JsonResult ShippingCost(string value,string total)
          {
               var v = long.Parse(value);
               var t = decimal.Parse(total);
               var ship = new ShipDao().TakeByID(v);
               return Json(new
               {
                    cost = ship.Cost.ToString("N0") + " VNĐ",
                    time = ship.Time
                    //total = (ship.Cost + t).ToString("N0")
               });
          }

          [HttpPost]
          public ActionResult UpdateQuantity(long ItemID,int quantity)
          {
               int check = 0;
               var dao = new CartItemDao();
               if (Session["UserID"] != null)
               {
                    var userID = (long)Session["UserID"];
                    var item = dao.TakeItem(userID, (long)ItemID);
                    if (item != null)
                    {
                         dao.UpdateItem(userID, ItemID, quantity);
                         check = 1;
                    }
                    Session["Cart"] = CreateCart();
               }
               else
               {
                    var curCart = TakeCart();
                    foreach (var i in curCart)
                    {
                         if (i.ItemID == ItemID)
                         {
                              i.Quantity = quantity;
                              check = 1;
                              break;
                         }
                    }
                    Session["Cart"] = curCart;
               }
               if (check == 0)
               {
                    return Json(new
                    {
                         error = "Khong co san pham nay trong gio hang",
                         status = true
                    });
               }
               var group = new IndexCartModel
               {
                    cart = Session["Cart"] as List<CartItemDetail>,
                    totalPrice = TotalSelectedPrice,
                    totalQuantity = TotalQuantity,
                    totalPromotion = TotalPromotion,
                    listHotBook = new BookDao().ListHotBook(20,4)
               };
               return Json(new
               {
                    stringView = RenderRazorViewToString("ContentCart", group),
                    totalPromotion =((decimal)TotalPromotion).ToString("N0") ,
                    totalPrice = ((decimal)TotalSelectedPrice).ToString("N0"),
                    realPrice = ((decimal)(TotalSelectedPrice - TotalPromotion)).ToString("N0"),
                    status = true
               });
          }
          [HttpPost]
          public JsonResult ChangeSelected(long ItemID)
          {
               var UserID = Session["UserID"] as long?;
               var cart = TakeCart();
               if (cart.ToList().Where(x => x.ItemID == ItemID).Single().Selected)
               {
                    cart.ToList().Where(x => x.ItemID == ItemID).Single().Selected = false;
               }
               else cart.ToList().Where(x => x.ItemID == ItemID).Single().Selected = true;
               Session["Cart"] = cart;
               return Json(new{
                    totalPromotion = ((decimal)TotalPromotion).ToString("N0"),
                    totalPrice = ((decimal)TotalSelectedPrice).ToString("N0"),
                    realPrice = ((decimal)(TotalSelectedPrice - TotalPromotion)).ToString("N0"),
                    status = true
               });
          }
     }
}