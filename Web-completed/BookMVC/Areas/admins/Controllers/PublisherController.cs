using BookMVC.Dao;
using BookMVC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookMVC.Common;
namespace BookMVC.Areas.admins.Controllers
{
    public class PublisherController : Controller
    {
        [HasCredential(RoleID = "VIEW_PUBLISHER")]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 4)
        {
            var dao = new PublisherDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpGet]
        [HasCredential(RoleID = "EDIT_PUBLISHER")]
        public ActionResult Edit(int id)
        {

            var model = new PublisherDao().ViewDetail(id);
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        [HasCredential(RoleID = "EDIT_PUBLISHER")]
        public ActionResult Edit(Publisher bk)
        {

            if (ModelState.IsValid)
            {

                var dao = new PublisherDao();

                var result = dao.Update(bk);
                if (result)
                {
                    SetAlert("Cập nhật sách thành công", "success");

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
        [HasCredential(RoleID = "ADD_PUBLISHER")]
        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        [HasCredential(RoleID = "ADD_PUBLISHER")]
        public ActionResult Create(Publisher us)
        {
            if (ModelState.IsValid)
            {
                var dao = new PublisherDao();
                var id = dao.addPublisher(us);
                if (id)
                {
                    SetAlert("Thêm mới thành công", "success");

                    return RedirectToAction("Index");
                }
                else
                {
                    SetAlert("Thêm không thành công", "error");

                }
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        [HasCredential(RoleID = "DELETE_PUBLISHER")]
        public ActionResult Delete(int id)
        {

            var res = new PublisherDao().Delete(id);

            if (res)
            {

                SetAlert("xóa thành công", "success");

                return RedirectToAction("Index");
            }
            else
            {
                SetAlert("xóa không thành công", "error");
            }
            return RedirectToAction("Index");


        }
        [HasCredential(RoleID = "DELETE_PUBLISHER")]
        public ActionResult SetNull(int id)
        {

            var res = new PublisherDao().SetNull(id);

            if (res)
            {

                SetAlert("xóa thành công", "success");

                return RedirectToAction("Index");
            }
            else
            {
                SetAlert("xóa không thành công", "error");

            }

            return RedirectToAction("Index");


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
            var data = new PublisherDao().ListName(q);
            return Json(new
            {
                data = data,
                status = true
            }, JsonRequestBehavior.AllowGet);


        }
        public JsonResult ChangeStatus(int id)
        {
            var result = new PublisherDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}