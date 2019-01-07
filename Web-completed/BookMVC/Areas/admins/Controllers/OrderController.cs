using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookMVC.Dao;
using BookMVC.Entities;
using BookMVC.Areas.admins.Models;
using BookMVC.Common;
namespace BookMVC.Areas.admins.Controllers
{
    public class OrderController : Controller
    {
        // GET: admins/Order
        [HasCredential(RoleID = "VIEW_ORDER")]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 4)
        {
            var dao = new OrderDao();
            ViewBag.SearchString = searchString;
            var model = dao.ListAllPage(searchString, page, pageSize);
            return View(model);
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
        [HttpGet]
        [HasCredential(RoleID = "EDIT_ORDER")]
        public ActionResult Edit(int id)
        {
            Order order = new Order();
            var dao = new OrderDao();
            order = dao.FindID(id);
            SetViewBag(order.ShipTypeID);
            SetViewBag1(order.Shiper);
            SetViewBag2(order.Status);
            return View(order);
        }
        [HttpPost]
        [HasCredential(RoleID = "EDIT_ORDER")]
        public ActionResult Edit(Order order)
        {

            var dao = new OrderDao();
            bool a = dao.EditOrder(order);
            if (a)
            {
                SetAlert("cập nhật thành công", "success");
                return RedirectToAction("Index", "Order");
            }
            else
            {
                SetAlert("Cập nhật không  thành công", "error");

            }
            return RedirectToAction("Index", "Order");
        }
        [HasCredential(RoleID = "DELETE_ORDER")]
        public ActionResult Delete(int id)
        {

            var res = new OrderDao().Delete(id);

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
               var dao = new ShipDao();
               ViewBag.ShipTypeID = new SelectList(dao.ListAll(), "ID", "TypeShip", selectID);
          }
          public void SetViewBag1(long? selectID = null)
          {
               var dao = new UserDao();
               ViewBag.Shiper = new SelectList(dao.ListAll().Where(x => x.GroupID == "SHIPPER"), "ID", "Name", selectID);
          }
          public void SetViewBag2(long? selectID = null)
          {
               var dao = new StatusOrderDao();
               ViewBag.Status = new SelectList(dao.ListAll(), "Status", "content", selectID);
          }
          public JsonResult ListName(string q)
          {
            var dao = new OrderDao();
            var data = dao.Listsearch(q);
            return Json(new
            {
                data = data,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        //public JsonResult ChangeStatus(int id)
        //{
        //    var result = new OrderDao().ChangeStatus(id);
        //    return Json(new
        //    {
        //        status = result
        //    });
        //}
    }
}