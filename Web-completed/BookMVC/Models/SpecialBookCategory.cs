using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookMVC.Dao;
namespace BookMVC.Models
{
     public class SpecialBookCategory
     {
          public long ID { get; set; }
          public string Name { get; set; }
          public int numberBooks { get; set; }
          public List<BookViewModel> lsBook { get; set; }
          public SpecialBookCategory(long id, List<BookViewModel> ls)
          {
               ID = id;
               Name = new BookCatgoryDao().FindByID(id).Name;
               lsBook = ls;
               numberBooks = ls.Count();
          }
     }
}