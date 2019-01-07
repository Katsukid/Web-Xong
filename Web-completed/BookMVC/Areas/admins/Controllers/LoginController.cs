using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookMVC.Dao;
using BookMVC.Areas.admins.Models;
using BookMVC.Common;
//using B.Common;
namespace BookMVC.Areas.admins.Controllers
{
     public class LoginController : Controller
     {
          // GET: admins/Login

          public ActionResult Index()
          {
               if (Request.Cookies["Login"] != null)
               {
                    LoginModel user = checkCookie();
                    if (user != null)
                         return View(user);
               }
               return View();
          }


          public LoginModel checkCookie()
          {
               LoginModel user = new LoginModel()
               {
                    UserName = Request.Cookies["Login"].Values["UserName"],
                    Password = Request.Cookies["Login"].Values["Password"],
                    RememberMe = true
               };
               if (!(string.IsNullOrEmpty(user.UserName) && string.IsNullOrEmpty(user.Password)))
               {
                    return user;
               }
               return null;
          }
          //[HttpGet]
          //public ActionResult Login()
          //{
          //    if (Request.Cookies["Login"]!= null)
          //    {
          //        LoginModel user = checkCookie();
          //        if (user != null)
          //            return View(user);
          //    }
          //    return View();

          //}
          //[HttpPost]
          //[ValidateAntiForgeryToken]
          //public ActionResult Login(LoginModel user)
          //{
          //    var dao = new UserDao();
          //    if(dao.Login(user.UserName,user.Password))
          //    {
          //        var us = dao.TakeUser(user.UserName, user.Password);
          //        if (user.RememberMe)
          //        {
          //            HttpCookie cookie = new HttpCookie("Login");
          //            cookie.Values.Add("Password", user.Password);
          //            cookie.Values.Add("UserName", user.UserName);
          //            cookie.Expires = DateTime.Now.AddMinutes(5);
          //            Response.Cookies.Add(cookie);
          //        }
          //        Session["UserName"] = us.UserName;
          //        Session["UserID"] = us.ID;
          //        return RedirectToAction("Index", "Home");
          //    }
          //    else
          //    {
          //        SetAlert("Tên tài khoản hoặc mật khẩu không đúng!!", "error");
          //        return RedirectToAction("Login", "Login");
          //    }



          //}
          public ActionResult Logout()
          {
               Session.Clear();
               return RedirectToAction("Index", "Login");
          }

          public ActionResult Login(LoginModel model)
          {
               if (ModelState.IsValid)
               {
                    var dao = new UserDao();
                    var result = dao.Login(model.UserName, model.Password, true);
                    if (result == 1)
                    {
                         if (model.RememberMe)
                         {
                              HttpCookie cookie = new HttpCookie("Login");
                              cookie.Values.Add("Password", model.Password);
                              cookie.Values.Add("UserName", model.UserName);
                              cookie.Expires = DateTime.Now.AddMinutes(5);
                              Response.Cookies.Add(cookie);
                         }
                         var user = dao.GetById(model.UserName);
                         var us = dao.TakeUser(user.UserName, user.Password);
                         var userSession = new UserLogin();
                         Session["UserName"] = us.UserName;
                         Session["UserID"] = us.ID;
                         userSession.UserName = user.UserName;
                         userSession.UserID = user.ID;
                         userSession.GroupID = user.GroupID;
                         var listCredentials = dao.GetListCredential(model.UserName);

                         Session.Add(CommonConstant.SESSION_CREDENTIALS, listCredentials);
                         Session.Add(CommonConstant.USER_SESSION, userSession);
                         return RedirectToAction("Index", "Home");
                    }
                    else if (result == 0)
                    {
                         ModelState.AddModelError("", "Tài khoản không tồn tại.");
                    }
                    else if (result == -1)
                    {
                         ModelState.AddModelError("", "Tài khoản đang bị khoá.");
                    }
                    else if (result == -2)
                    {
                         ModelState.AddModelError("", "Mật khẩu không đúng.");
                    }
                    else if (result == -3)
                    {
                         ModelState.AddModelError("", "Tài khoản của bạn không có quyền đăng nhập.");
                    }
                    else
                    {
                         ModelState.AddModelError("", "đăng nhập không đúng.");
                    }
               }
               return View("Index");
          }
          public void SetAlert(string message, string type)
          {
               //Giống ViewBag
               TempData["AlertMessage"] = message;
               if (type == "success")
               {
                    TempData["AlertType"] = "alert-success";
               }
               else if (type == "Warning")
               {
                    TempData["AlertType"] = "alert-warning";
               }
               else if (type == "error")
               {
                    TempData["AlertType"] = "alert-danger";
               }
          }
     }
}
