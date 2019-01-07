using BookMVC.Dao;
using BookMVC.Entities;
using BookMVC.Areas.admins.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using BookMVC.Common;
namespace BookMVC.Areas.admins.Controllers
{
    public class RevenueController : Controller
    {
        // GET: admins/Revenue
        public ActionResult Index()
        {
            return View();
        }
        // tong doanh thu theo thang
        [HasCredential(RoleID = "TK_ORDER")]
        public ActionResult Sum(int? month,int ?year,int page=1,int pagesize=4)
        {
            var dao = new OrderDao();
            decimal revenue = 0;
            IEnumerable<Order> lsOrder;
            if (year!=null)
            {
                lsOrder = dao.TakeInMonthOfYear(month, year,page,pagesize);
                ViewBag.Month = month;
                ViewBag.Year = year;
                
            }
            else
                lsOrder = dao.ListAll(page,pagesize);
            foreach (var o in lsOrder)
            {
                revenue += (decimal)o.TotalPrice;
            }
            ViewBag.Revenue = revenue;
            return View(lsOrder);
        }
        


    }
}