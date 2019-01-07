using BookMVC.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookMVC.Entities;
using BookMVC.Common;
namespace BookMVC.Areas.admins.Controllers
{
    public class ShipController : Controller
    {
        // GET: admins/Ship
        [HasCredential(RoleID = "VIEW_SHIP")]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 4)
        {
            var dao = new ShipDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpGet]
        [HasCredential(RoleID = "EDIT_SHIP")]
        public ActionResult Edit(int id)
        {
            
            var B = new ShipDao().FindID(id);
           return View(B);
        }
        [HttpPost]
        [ValidateInput(false)]
        [HasCredential(RoleID = "EDIT_SHIP")]
        public ActionResult Edit(ShippingType bk)
        {

            if (ModelState.IsValid)
            {

                var dao = new ShipDao();

                var result = dao.EditShip(bk);
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
        [HasCredential(RoleID = "ADD_SHIP")]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [HasCredential(RoleID = "ADD_SHIP")]
        public ActionResult Create(ShippingType us)
        {
            if (ModelState.IsValid)
            {
                var dao = new ShipDao();
                var id = dao.AddShip(us);
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
        [HttpPost]
        [HasCredential(RoleID = "DELETE_SHIP")]
        public ActionResult Delete(int id)
        {

            var res = new ShipDao().Deleteship(id);

            if (res)
            {
                return Json(new { status = true });
            }
            else
            {
                return Json(new { status = false });
            }


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
            var data = new ShipDao().Listsearch(q);
            return Json(new
            {
                data = data,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
    }

}