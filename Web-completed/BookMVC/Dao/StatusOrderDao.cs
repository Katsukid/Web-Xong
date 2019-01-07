using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookMVC.Entities;
namespace BookMVC.Dao
{
     public class StatusOrderDao
     {
          BookMVCDbContext db = null;
          public StatusOrderDao()
          {
               db = new BookMVCDbContext();
          }
          public List<Contact> ListAll()
          {
               return db.Contacts.ToList();
          }
     }
}