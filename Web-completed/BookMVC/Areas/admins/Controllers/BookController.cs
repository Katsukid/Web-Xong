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
    public class BookController : Controller
    {
        [HasCredential(RoleID = "VIEW_BOOK")]
        public ActionResult Index(string searchString,int page = 1, int pageSize = 4)
        {
            var dao = new BookDao();
            var model = dao.ListAllByTag(searchString,page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpGet]
        [HasCredential(RoleID = "EDIT_BOOK")]
        public ActionResult Edit(int id)
        {
            
            ViewBag.Book = new BookDao().ViewDetail(id);
            var book = new BookDao().ViewDetail(id);
            SetViewBag(book.CategoryID);
            SetViewBagAuthor(book.Author);
            SetViewBagPublisher(book.Publisher);
            SetViewBagRelease(book.Released);
            ViewBag.Form = new SelectList(new BookDao().Form());
            return View(book);
        }
        [HttpPost]
        [ValidateInput(false)]
        [HasCredential(RoleID = "EDIT_BOOK")]
        public ActionResult Edit(Book bk)
        {
           
            if (ModelState.IsValid)
            {
               
                var dao = new BookDao();
                ViewBag.Form = new SelectList(new BookDao().Form());
                var createby = Session["UserName"] as string;
                var result = dao.Update(bk,createby);
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
        [HasCredential(RoleID = "ADD_BOOK")]
        public ActionResult Create()
        {
            var book = TempData["Book"] as Book;
            var modelState = TempData["modelState"] as ModelStateDictionary;
            if (modelState != null) 
            foreach(var e in modelState)
            {
                foreach(var err in e.Value.Errors)
                        if( err != null)
                            ModelState.AddModelError(e.Key, err.ErrorMessage);
            }
            ViewBag.Form = new SelectList(new BookDao().Form());
            SetViewBag();
            SetViewBagAuthor();
            SetViewBagPublisher();
            SetViewBagRelease();
            return View(book);
        }
        [HttpPost]
        [ValidateInput(false)]
        [HasCredential(RoleID = "ADD_BOOK")]
        public ActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                var dao = new BookDao();
                var createby = Session["UserName"] as string;
                var id = dao.addBook(book, createby);
                if (id)
                {
                    SetAlert("Thêm mới thành công bởi ", "success");
                    
                    return RedirectToAction("Index");
                }
                else
                {
                    SetAlert("Thêm không thành công", "error");
                    return RedirectToAction("Index");
                }
            }
            else
            {
                SetAlert("Thêm không thành công", "error");
                TempData["modelState"] = ModelState;
                TempData["Book"] = book;
                return RedirectToAction("Create");
            }
           
        }
        [HttpPost]
        [HasCredential(RoleID = "DELETE_BOOK")]
        public ActionResult Delete(int id)
        {
           
            var res = new BookDao().Delete(id);

            if (res)
            {
                return Json(new { status = true });
            }
            else
            {
                return Json(new { status = false });
            }

          
        }
        //Lấy ra thông tin dropdownList cua loai sach
        public void SetViewBag(long? selectID = null)
        {
            var dao = new BookCatgoryDao();
            ViewBag.CategoryID = new SelectList(dao.ListAll(), "ID", "Name", selectID);
        }
        //Lấy ra thông tin dropdownList cua loai sach
        public void SetViewBagAuthor(long? selectID = null)
        {
            var dao = new AuthorDao();
            ViewBag.Author = new SelectList(dao.ListAll(), "ID", "Name", selectID);
        }
        //Lấy ra thông tin dropdownList cua nha phat hanh  
        public void SetViewBagRelease(long? selectID = null)
        {
            var dao = new PublisherDao();
            ViewBag.Released = new SelectList(dao.ListAll(), "ID", "Name", selectID);
        }
        //Lấy ra thông tin dropdownList cua NXB
        public void SetViewBagPublisher(long? selectID = null)
        {
            var dao = new PublisherDao();
            ViewBag.Publisher = new SelectList(dao.ListAll(), "ID", "Name", selectID);
        }
        public void SetViewBagImport()
        {
            var dao = new BookDao();
            ViewBag.Code = new SelectList(dao.ListAll(), "Code","Code");
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
        public JsonResult ChangeStatus(int id)
        {
            var result = new BookDao().ChangeStatus(id);
            return Json(new {
                status = result
            }); 
        }
        public JsonResult ListName(string q)
        {
            var data = new BookDao().Listsearch(q);
            return Json(new
            {
                data = data,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        [HasCredential(RoleID = "IMP_BOOK")]
        public ActionResult Import()
        {
            SetViewBagImport();
            return View();
        }
        [HttpPost]
        [HasCredential(RoleID = "IMP_BOOK")]
        public ActionResult Import(string Code,int Inventory)
        {
            var import = new BookDao().Import(Code, Inventory);
            if (import)
            {
                SetAlert("Cập nhật thành công ", "success");

                return RedirectToAction("Index");
            }
            else
            {
                SetAlert("Cập nhật không thành công", "error");
                return RedirectToAction("Import");
            }
        }
    }
}
