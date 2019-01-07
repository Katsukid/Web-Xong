using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookMVC.Entities;
using BookMVC.Dao;
using PagedList;
using BookMVC.Common;
//using B.Common;
namespace BookMVC.Areas.admins.Controllers
{
    public class UserController : Controller
    {
        // GET: admins/Usesr
        [HasCredential(RoleID = "VIEW_USER")]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 4)
        {
            var dao = new UserDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpGet]
        [HasCredential(RoleID = "EDIT_USER")]
        public ActionResult Edit(int id)
        {
            var user = new UserDao().ViewDetail(id);
            return View(user);
        }
        [HttpPost]
        [HasCredential(RoleID = "EDIT_USER")]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();

                var result = dao.Update(user);
                if (result)
                {
                    SetAlert("Cập nhật người dùng thành công", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    SetAlert("Cập nhật không thành công", "error");
                }
            }
            return RedirectToAction("Index", "User");
        }

        [HttpGet]
        [HasCredential(RoleID = "ADD_USER")]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [HasCredential(RoleID = "ADD_USER")]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var id = dao.Insert(user);
                if (id)
                {
                    SetAlert("Thêm người dùng thành công", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    SetAlert("Thêm người dùng không thành công", "error");
                    return RedirectToAction("Index", "User");
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        [HasCredential(RoleID = "DELETE_USER")]
        public JsonResult Delete(long id)
        {
            var us = (long)Session["UserID"];
            var res = new UserDao().Delete(id, us);

            if (res == 1)
            {
                return Json(new { status = 1 });
            }
            else if (res == 0)
            {
                return Json(new { status = 0 });
            }
            else
            {
                return Json(new { status = -1 });
            }
        }
        [HttpPost]
        [HasCredential(RoleID = "EDIT_USER")]
        public JsonResult ChangeStatus(long id)
        {
            var result = new UserDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
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
        public JsonResult ListName(string q)
        {
            var data = new UserDao().Listsearch(q);
            return Json(new
            {
                data = data,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        //public void SetViewBag(long? selectID = null)
        //{
        //    var dao = new UserGroupDao();
        //    ViewBag.GroupID = new SelectList(dao.ListAll(), "ID", "Name", selectID);
        //}
    }
}