using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookMVC.Entities;
using BookMVC.Dao;
//using BookMVC.Areas.admins.Code;
using System.Web.Security;
using BookMVC.Models;
namespace BookMVC.Controllers
{
     public class UserController : Controller
     {
          BookMVCDbContext db = new BookMVCDbContext();
          // Gộp giỏ
          public void UpdateCartAfterLogin()
          {
               var cartDao = new CartItemDao();
               var UserID = Session["UserID"] as long?;
               if (Session["Cart"] != null)
               {
                    var currentCart = Session["Cart"] as List<CartItemDetail>;
                    if(currentCart.Count != 0)
                    // Gộp 2 giỏ hàng
                    foreach (var i in currentCart)
                    {
                         cartDao.AddItem((long)UserID, i.ItemID, i.Quantity);
                    }
                    // Giỏ mới sau khi gộp
                    Session["Cart"] = cartDao.TakeCartFromDB(UserID);
               }               
          }
          // Lay Cookie chua thong tin dang nhap phien lam viec truoc
          //[AllowAnonymous]
          public AccountModelView checkCookie()
          {
               AccountModelView user = new AccountModelView()
               {
                    Email = Request.Cookies["Login"].Values["Email"],
                    Password = Request.Cookies["Login"].Values["Password"],
                    RememberMe = true
               };
               if (!(String.IsNullOrEmpty(user.Email) && String.IsNullOrEmpty(user.Password)))
                    return user;
               return null;
          }

          //[AllowAnonymous]
          [HttpGet]
          public ActionResult LoginModal()
          {
               if (Request.Cookies["Login"] != null)
               {
                    AccountModelView user = checkCookie();
                    if (user != null)
                         return PartialView(user);
               }
                    return PartialView();
          }
          
          [HttpPost]
          [ValidateAntiForgeryToken]
          public JsonResult Login(AccountModelView acc)
          {
               //if (Membership.ValidateUser(acc.Email, acc.Password))
               if(new UserDao()._Login(acc.Email, acc.Password))
               {
                    var dao = new UserDao();
                    var us = dao._TakeUser(acc.Email, acc.Password);
                    //dao.RememberMe(us.ID);
                    //FormsAuthentication.SetAuthCookie(us.UserName, acc.RememberMe);
                    
                    //HttpContext.Request.ServerVariables["AUTH_USER"]
                    if (acc.RememberMe)
                    {
                         HttpCookie cookie = new HttpCookie("Login");
                         cookie.Values.Add("Email", acc.Email);
                         cookie.Values.Add("Password", acc.Password);
                         cookie.Expires = DateTime.Now.AddMinutes(5);
                         Response.Cookies.Add(cookie);
                    }
                    else
                    {
                         Response.Cookies["Login"].Expires = DateTime.Now.AddDays(-1);
                    }
                    Session["UserName"] = us.UserName; // Lay ten dang nhap
                    Session["UserID"] = us.ID;
                    Session.Timeout = 30;
                    UpdateCartAfterLogin();
                    return Json(new
                    {
                         username = us.UserName,
                         status = us.Status
                    });
               }
               return Json(new { check = false });
          }
          //[Authorize]
          public ActionResult Logout()
          {
               //FormsAuthentication.SignOut();
               Session.Clear();
               return RedirectToAction("Index", "Home");
          }
          [HttpGet]
          public ActionResult Register()
          {
               var us = TempData["us"] as User;
               var model = TempData["modal"] as ModelStateDictionary;
               if (model != null)
               {
                    foreach( var e  in model)
                    {
                         foreach(var err in e.Value.Errors)
                         {
                              if(err != null)
                              {
                                   ModelState.AddModelError(e.Key, err.ErrorMessage);
                              }
                         }
                    }
               }
               //HttpContext.Request.ServerVariables["AUTH_USER"] == null ||
               if (Session["UserName"] == null)
                    return View(us);
               return RedirectToAction("Index","Home");
          }
          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult Register(User us)
          {
               //db = new BookMVCDbContext();
               var log = new UserDao();
               {
                    if (ModelState.IsValid)
                    {
                         //if (!log.ValidEmail(us.Email)){
                         //     setAlert("Email không hợp lệ hoặc không tồn tại!","Error");
                         //}
                         //else 
                         if (log.ExistedEmail(us.Email))
                         {
                              setAlert("Email đã được sử dụng bởi tài khoản khác!", "Error");
                         }
                         else
                         {
                              var res = log.AddUser(us);
                              if (res)
                              {
                                   setAlert("Đăng ký thành công!", "success");
                                   return Redirect("/");
                              }
                              else
                              {
                                   setAlert("Đăng ký không thành công! Quý khác vui lòng đăng ký lại", "error");
                                   return RedirectToAction("Register","User");
                              }
                         }
                    }
                    else
                    {
                         TempData["us"] = us;
                         TempData["modal"] = ModelState;
                         return RedirectToAction("Register");
                    }
               }
               return RedirectToAction("Index","Home");
          }
          // Canh bao nguoi dung
          public void setAlert(string message, string type)
          {
               TempData["AlertMessage"] = message;
               if (type == "success")
               {
                    TempData["AlertType"] = "alert-success";
               }
               else if (type == "error")
               {
                    TempData["AlertType"] = "alert-danger";
               }
          }
          [HttpPost]
          public JsonResult ChangeStatus(long id)
          {
               var result = new UserDao().ChangeStatus(id);
               return Json(new
               {
                    status = result
               });
          }
     }
}