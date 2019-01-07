using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookMVC.Entities;
using BookMVC.Dao;
using PagedList;
using BookMVC.Common;
namespace BookMVC.Areas.admins.Controllers
{
    public class BannerController : Controller
    {
        // GET: admins/Usesr
        [HasCredential(RoleID = "VIEW_BANNER")]
        public ActionResult Index( string searchString, int page = 1, int pageSize = 4)
        {
            var dao = new SlideDao();
            var model = dao.List(searchString, page, pageSize);
            ViewBag.Slide = new BookDao().ListAll();
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpGet]
        [HasCredential(RoleID = "EDIT_BANNER")]
        public ActionResult Edit(int id)
        {
            var slide = new SlideDao().FindID(id);
            SetViewBag(slide.ID);
            ViewBag.Where = new SelectList(new SlideDao().ListAll());
            return View(slide);
        }
        [HttpPost]
        [HasCredential(RoleID = "EDIT_BANNER")]
        public ActionResult Edit(Slide slide)
        {
            if (ModelState.IsValid)
            {
                var dao = new SlideDao();

                var result = dao.EditSlide(slide);
                if (result)
                {
                    SetAlert("Cập nhật slide thành công", "success");
                    SetViewBag();
                    return RedirectToAction("Index");
                }
                else
                {
                    SetAlert("Cập nhật không thành công", "error");
                   
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        [HasCredential(RoleID = "ADD_BANNER")]
        public ActionResult Create()
        {
            SetViewBag();
            ViewBag.Where = new SelectList(new SlideDao().ListAll());
            return View();
        }
        [HttpPost]
        [HasCredential(RoleID = "ADD_BANNER")]
        public ActionResult Create(Slide slide)
        {
            if (ModelState.IsValid)
            {
                var res = new SlideDao().AddSlide(slide);
                if (res)
                {
                    SetAlert("Thêm mới thành công", "success");
                    SetViewBag();
                    return RedirectToAction("Index");
                }
                else
                {
                    SetAlert("Thêm mới không thành công", "error");
                    return View();
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        [HasCredential(RoleID = "DELETE_BANNER")]
        public ActionResult Delete(int id)
        {

            var res = new SlideDao().DeleteSlide(id);

            if (res)
            {
                return Json(new { status = true });
            }
            else
            {
                return Json(new { status = false });
            }


        }
        public void SetViewBag(long? selectID = null)
        {
            var dao = new BookDao();
            ViewBag.BookID = new SelectList(dao.ListAll(), "ID", "Name", selectID);
        }
        public void SetViewBagWHere(long? selectID = null)
        {
            var dao = new SlideDao();
            ViewBag.Where = new SelectList(dao.ListAll(), "ID", "Name", selectID);
        }
        public JsonResult ChangeStatus(long id)
        {
            var result = new SlideDao().ChangeStatus(id);
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
            var data = new SlideDao().ListName(q);
            return Json(new
            {
                data = data,
                status = true
            }, JsonRequestBehavior.AllowGet);


        }
    }
}