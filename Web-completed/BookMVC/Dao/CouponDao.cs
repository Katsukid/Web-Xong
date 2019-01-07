using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookMVC.Entities;
namespace BookMVC.Dao
{
     public class CouponDao
     {
          BookMVCDbContext db;
          public CouponDao()
          {
               db = new BookMVCDbContext();
          }
          public List<Coupon> ListAll()
          {
               return db.Coupons.ToList();
          }
          public Coupon TakeCoupon(string Serial)
          {
               return db.Coupons.Where(x => x.Serial == Serial).SingleOrDefault();
          }
     } 
}