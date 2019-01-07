using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookMVC.Entities;
using BookMVC.Models;
using BookMVC.Dao;
namespace BookMVC.Controllers
{
     public class AccountController : Controller
     {
          // GET: Account
          public ActionResult UserProfile()
          {
               if (Session["UserID"] == null)
                    return RedirectToAction("Index", "Home");
               var id = (long)Session["UserID"];
               var user = new UserDao().GetUser(id);
               return View(user);
          }
          [HttpPost]
          public JsonResult CheckOldPassword(string oldpassword) {
               var user = new UserDao().GetUser((long)Session["UserID"]);
               var check = (user.Password == oldpassword);
               return Json(check);
          } 

          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult ChangePassword(FormCollection form)
          {
               var userID = (long)Session["UserID"];
               var newpass = form["password_1"] as string;
               var result = new UserDao().ChangePassword(userID, newpass);
               return RedirectToAction("UserProfile");
          }
          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult ChangeProfile(User user)
          {
               var id = (long)Session["UserID"];
               //var user = new User();
               //user.Name = form["Name"];
               //user.Email = form["Email"];
               //user.UserName = form["UserName"];
               //user.Phone = form["Phone"];
               //user.DayOfBirth = DateTime.Parse(form["DayOfBirth"]);
               var result = new UserDao().ChangeProfile(id, user);
               if (result) TempData["message"] = "Thay đổi thành công";
               else TempData["message"] = "Thay đổi thất bại";
               return RedirectToAction("UserProfile");
          }

          public ActionResult ListOrder()
          {
               var userID = (long)Session["UserID"];
               var lsOrders = new OrderDao().ListOrders(userID);
               return View(lsOrders);
          }

          public ActionResult OrderDetails(long orderID,int number)
          {
               var dao = new OrderDao();
               var order = dao.TakeOrder(orderID);
               var lsItems = dao.ListOrderItems(orderID);
               ViewBag.Number = number;
               ViewBag.Order = order;
               ViewBag.User = new UserDao().GetUser((long)Session["UserID"]);
               ViewBag.ShippingPrice = new ShipDao().TakeByID((long)order.ShipTypeID).Cost;
               return View(lsItems);
          }

          public ActionResult ListWish()
          {
               return View();
          }
          // Trạng thái đơn hàng
          public PartialViewResult StatusOrder(int status)
          {
               return PartialView(status);
          }
          // Hủy đơn hàng
          [HttpPost]
          public PartialViewResult CancelOrder(long orderID)
          {
               new OrderDao().CancelOrder(orderID);
               var userID = (long)Session["UserID"];
               var lsOrders = new OrderDao().ListOrders(userID);
               return PartialView("ReListOrder",lsOrders);
          }
          [HttpPost]
          public PartialViewResult UnDisplayOrder(long orderID)
          {
               new OrderDao().UnDisplayOrder(orderID);
               var userID = (long)Session["UserID"];
               var lsOrders = new OrderDao().ListOrders(userID);
               return PartialView("ReListOrder", lsOrders);
          }
     }
}