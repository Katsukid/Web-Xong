// Không dùng



using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookMVC.Entities;
namespace BookMVC.Dao
{
     public class AdminDao
     {
          BookMVCDbContext db;
          public AdminDao()
          {
               db = new BookMVCDbContext();
          }
          public List<Admin> ListAll()
          {
               return db.Admins.Where(x => x.Status == true).ToList();
          }
          public bool LoginAdmin(string user, string pass)
          {
               var ad = db.Admins.SingleOrDefault(x => x.UserName == user && x.PassWord == pass);
               if (ad == null)
               {
                    return false;
               }
               return true;
          }
     }
}