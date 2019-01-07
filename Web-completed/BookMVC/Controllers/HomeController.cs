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
     public class HomeController : Controller
     {
          public ActionResult Index()
          {
               return View();
          }
          [HttpPost]
          public ActionResult Search(string word)
          {
               var list = new SearchDao().List(word);
               return PartialView(list);
          }

          public ActionResult Contact()
          {
               return View(); 
          }

          // Partial View
          public ActionResult TopNavBar()
          {
               var bookdao = new BookDao();
               var category = new CategoryDao()._ListAll();
               var special = (new SpecialBookCategory[]
               {
                    new SpecialBookCategory(1,bookdao.ListHotBook(40)),
                    new SpecialBookCategory(2,bookdao.ListNewBook(60)),
                    new SpecialBookCategory(3,bookdao.ListFutureBook()),
                    //new SpecialBookCategory(4,bookdao.ListPromotionBook())
               }).ToList();
               ViewBag.Special = special;
               ViewBag.Category = category;
               ViewBag.AuthorLocal = new AuthorDao().ListLocal(5);
               ViewBag.AuthorForgery = new AuthorDao().ListForgery(5);
               ViewBag.Publisher = new PublisherDao()._ListAll();
               return PartialView();
          }
          // Banner top
          public ActionResult Banner(int count,int number)
          {
               var banner = new SlideDao().TakeSlide(count,"top");
               return PartialView(banner);
          }
          public ActionResult BannerBottom(int count, int number)
          {
               var banner = new SlideDao().TakeSlide(count,"bottom");
               return PartialView("Banner",banner);
          }
          // Bình luận của khách
          public  ActionResult Feedback()
          {
               return PartialView();
          }
          // Báo lá cải
          public ActionResult News()
          {
               return PartialView();
          }
         
          // Blog
          public ActionResult Blog()
          {
               var lsBlog = new NewDao().TakeNews(2);
               return PartialView(lsBlog);
          }
          public ActionResult BlogDetail(long id)
          {
               var blog = new NewDao().TakeNews(id);
               return View(blog);
          }
     }
}