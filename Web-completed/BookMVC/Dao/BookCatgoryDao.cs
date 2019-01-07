using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookMVC.Entities;
namespace BookMVC.Dao
{
     public class BookCatgoryDao
     {
          BookMVCDbContext db = null;

          public BookCatgoryDao()
          {
               db = new BookMVCDbContext();
          }
          public List<BookCategory> ListAll()
          {
               return db.BookCategories.Where(x => x.Status == true).OrderBy(x => x.DisplayOrder).ToList();
          }
          public BookCategory FindByID(long id)
          {
               return db.BookCategories.Find(id);
          }
          public List<BookCategory> ListByCategory(long id)
          {
               return db.BookCategories.Where(x => x.ParentID == id).ToList();
          }

          // Admin
          public BookCategory ViewDetail(long id)
          {
               return FindByID(id);
          }

          public List<BookCategory> ListBookCategory(long id)
          {
               return ListByCategory(id);
          }
     }
}