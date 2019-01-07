using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookMVC.Entities;
using BookMVC.Dao;
using BookMVC.Models;
namespace BookMVC.Controllers
{
         public class BookController : Controller
         {
          // GET: Book
          public ActionResult Index()
          {
               return View();
          }

          public ActionResult Card(BookViewModel b)
          {
               if (!ControllerContext.IsChildAction)
                    return RedirectToAction("Index", "Home");
               ViewBag.isNew = new BookDao().IsNew(b.ID);
               return PartialView(b);
          }

          public ActionResult Detail(long id)
          {
               //Lấy sách đó ra
               var bookview = new BookDao().TakeBook(id); // Dùng để lấy danh sách cung loại, cùng tác giả
               var book = new BookDao().FindByID(id);
               // Lay danh muc
               var bookcategory = new BookCatgoryDao().FindByID(book.CategoryID.Value);
               // Cập nhật số lượt xem
               new BookDao().UpdateViews(id);
               bool isNew = new BookDao().IsNew(id);
               bool isSale = new BookDao().IsSale(id);
               // Truyền sang View
               ViewBag.BookView = bookview;
               ViewBag.SameAuthor = new BookDao().ListByAuthor((int)book.Author,4);
               ViewBag.SameCategory = new BookDao().ListByBookCategory(book.CategoryID.Value,4);
               ViewBag.Category = new CategoryDao().FindByID(bookcategory.ParentID.Value);
               ViewBag.BookCategory = bookcategory;
               ViewBag.isNew = isNew;
               ViewBag.iSale = isSale;
               return View();
          }

          public ActionResult ListAll()
          {
               if (!ControllerContext.IsChildAction)
                    return RedirectToAction("Index", "Home");
               return PartialView();
          }
          // Sách bán chạy
          public ActionResult HotBook()
          {
               ViewBag.NamePart = "Sách bán chạy";
               var lsHotBook = new BookDao().ListHotBook(20,4);
               return PartialView("ListBook", lsHotBook);
          }
          // Sách mới
          public ActionResult NewBook()
          {
               ViewBag.NamePart = "Sách mới";
               var lsNewBook = new BookDao().ListNewBook(30,4);
               return PartialView("ListBook", lsNewBook);
          }
          public ActionResult SameAuthor(int idAuthor)
          {
               ViewBag.NamePart = "Sách cùng tác giả";
               var lsSameAuthor = new BookDao().ListByAuthor(idAuthor, 4);
               return PartialView("ListBook", lsSameAuthor);
          }
          public ActionResult SameBookCategory(long idBookCate)
          {
               ViewBag.NamePart = "Sách cùng thể loại";
               var lsSameBookCategory = new BookDao().ListByBookCategory(idBookCate,4);
               return PartialView("ListBook", lsSameBookCategory);
          }
          // Sách theo chủ đề
          public ActionResult ByBookCategory(long idBookCate)
          {
               var lsByCategory = new BookDao().ListByBookCategory(idBookCate);
               ViewBag.NamePart = new BookCatgoryDao().FindByID(idBookCate).Name.ToString();
               return View("ListBook", lsByCategory);
          }
          // Sách theo tác giả
          public ActionResult BookByAuthor(int id)
          {
               var lsByAuthor = new BookDao().ListByAuthor(id);
               ViewBag.NamePart = new AuthorDao().TakeAuthor(id).Name.ToString();
               return View("ListBook",lsByAuthor);
          }
          // Sách theo nhà xuất bản
          public ActionResult BookByPublisher(int id)
          {
               var lsByPublisher = new BookDao().ListByPublisher(id);
               ViewBag.NamePart = new PublisherDao().TakePublisher(id).Name.ToString();
               return View("ListBook", lsByPublisher);
          }
     }
}