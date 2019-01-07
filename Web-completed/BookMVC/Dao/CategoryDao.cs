using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookMVC.Entities;
using BookMVC.Models;
using PagedList;

namespace BookMVC.Dao
{
     public class CategoryDao
     {
          BookMVCDbContext db;
          public CategoryDao()
          {
               db = new BookMVCDbContext();
          }
          // Lay danh sach
          public List<CategoryViewModel> _ListAll()
          {
               //return  db.Books.GroupBy(x => x.CategoryID)
               //          .Select(x => new { x.Key, numBook = x.Count(y => (bool)y.Status) })
               //          .Join(db.BookCategories, x => x.Key, y => y.ID, 
               //               (x,y) => new { y, numBooks = x.numBook })
               //          .GroupBy(z => z.y.ParentID)
               //          .DefaultIfEmpty()
               //          .Join(db.Categories, b => (long)b.Key, c => c.ID, (b, c) => new CategoryViewModel
               //          {
               //               Category = c,
               //               lsBookCategory = b.Select(x => x.y).ToList(),
               //               numberBooks = b.Sum(x => x.numBooks)
               //          })
               //          .ToList();
               var numberBooks = db.Books.GroupBy(x => x.CategoryID)
                    .Select(x => new { x.Key, numBook = x.Count(y => (bool)y.Status) });
               var temp2 = db.BookCategories
                         .GroupJoin(numberBooks, l => l.ID, r => r.Key,
                              (l, r) => new { bookCate = l, NumBook = r.FirstOrDefault() })
                         .Select(x => new BookCateVIewModel
                         {
                              bookCategory = x.bookCate,
                              numberBooks = x.NumBook != null ? x.NumBook.numBook : 0
                         })
                         .GroupBy(x => x.bookCategory.ParentID)
                         .Select(x => new
                         {
                              x.Key,
                              ls = x.ToList()
                         }).AsEnumerable();
               var temp3 = db.Categories
                    .Join(temp2, x => x.ID, y => y.Key,
                    (x, y) => new CategoryViewModel
                    {
                         Category = x,
                         lsBookCategory = y.ls,
                         numberBooks = y.ls.Sum(b => b.numberBooks)
                    })
                    .ToList();
               return temp3;                    
          }

          //public List<Category> FindID(long id)
          //{
          //     return db.Categories.Where(x => x.ID == id).ToList();
          //}
          public Category FindByID(long id)
          {
               return db.Categories.Find(id);
          }

          public List<Category> ListImage()
          {
               return db.Categories.Where(x => x.SeoTitle != null).ToList();
          }
          public List<BookCategory> ListBookCate(long idCate)
          {
               return db.BookCategories.Where(x => x.ParentID == idCate).ToList();
          }


          /*
           * Admin
           * 
           * 
           * 
           * 
           * 
           * 
           */
          public List<Category> ListAll()
          {
               return db.Categories.Where(x => x.Status == true).ToList();
          }
          public IEnumerable<Category> ListAllpage(string searching, int page, int pagesize)
          {
               IQueryable<Category> model = db.Categories;
               if (!string.IsNullOrEmpty(searching))
               {
                    model = model.Where(x => x.Name.Contains(searching));
               }
               return model.OrderByDescending(x => x.Name).ToPagedList(page, pagesize);
          }
          public bool ChangeStatus(int id)
          {
               var category = db.Categories.Find(id);
               category.Status = !category.Status;
               db.SaveChanges();
               return category.Status;


          }
          public bool Update(Category entity)
          {
               try
               {
                    var model = db.Categories.Find(entity.ID);
                    model.Name = entity.Name;
                    model.MetaTitle = Str_Metatitle(entity.Name);
                    //model.DisplayOrder = entity.DisplayOrder;
                    db.SaveChanges();
                    return true;


               }
               catch (Exception ex)
               {
                    return false;
               }


          }
          public bool add(Category entity, string a)
          {
               try
               {
                    var model = new Category();
                    model.Name = entity.Name;
                    model.MetaTitle = Str_Metatitle(entity.Name);
                    //model.DisplayOrder = entity.DisplayOrder;
                    model.CreatedDate = DateTime.Now;
                    model.CreatedBy = a;
                    model.Status = true;

                    db.Categories.Add(model);
                    db.SaveChanges();
                    return true;
               }
               catch
               {
                    return false;
               }


          }
          public List<string> Listsearch(string search)
          {
               return db.Categories.Where(x => x.Name.Contains(search)).Select(x => x.Name).ToList();
          }
          public Category ViewDetail(long id)
          {
               return db.Categories.Find(id);
          }
          public bool Delete(int id)
          {
               try
               {
                    var bk = db.Categories.Find(id);
                    db.Categories.Remove(bk);
                    db.SaveChanges();
                    return true;

               }
               catch (Exception)
               {
                    return false;
               }


          }
          public string Str_Metatitle(string str)
          {
               string[] VietNamChar = new string[]
               {
                "aAeEoOuUiIdDyY",
                "áàạảãâấầậẩẫăắằặẳẵ",
                "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
                "éèẹẻẽêếềệểễ",
                "ÉÈẸẺẼÊẾỀỆỂỄ",
                "óòọỏõôốồộổỗơớờợởỡ",
                "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
                "úùụủũưứừựửữ",
                "ÚÙỤỦŨƯỨỪỰỬỮ",
                "íìịỉĩ",
                "ÍÌỊỈĨ",
                "đ",
                "Đ",
                "ýỳỵỷỹ",
                "ÝỲỴỶỸ:"
               };
               //Thay thế và lọc dấu từng char      
               for (int i = 1; i < VietNamChar.Length; i++)
               {
                    for (int j = 0; j < VietNamChar[i].Length; j++)
                         str = str.Replace(VietNamChar[i][j], VietNamChar[0][i - 1]);
               }
               string str1 = str.ToLower();
               string[] name = str1.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
               string meta = null;
               //Thêm dấu '-'
               foreach (var item in name)
               {
                    meta = meta + item + "-";
               }
               return meta;
          }

     }
}