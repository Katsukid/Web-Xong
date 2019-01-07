using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookMVC.Models;
using BookMVC.Entities;
using PagedList;

namespace BookMVC.Dao
{
     public class AuthorDao
     {
          BookMVCDbContext db = null;
          public AuthorDao()
          {
               db = new BookMVCDbContext();
          }
          //public IEnumerable<Author> ListAll()
          //{
          //     return db.Authors.Join(db.Books, a => a.ID, b => (long)b.Author, (a, b) => new {
          //               author = a,
          //               count = db.Books.Count(x => (long)x.Author == a.ID)})
          //               .OrderByDescending(x => x.count)
          //               .Take(10).Select(x => x.author)
          //               .AsEnumerable();                            
          //}

          public IEnumerable<AuthorViewModel> ListLocal()
          {
               //     return db.Authors.Join(db.Books, a => a.ID, b => (long)b.Author, (a, b) => new {
               //               author = a,
               //               count = db.Books.Count(x => (long)x.Author == a.ID)})
               //               .OrderByDescending(x => x.count)
               //               .Take(10).Select(x => x.author).Where(x => x.Type =="Trong nước")
               //               .AsEnumerable();
               var ls = (from b in db.Books
                         group b by b.Author into grouping
                         join a in db.Authors
                         on (long)grouping.Key equals a.ID
                         where a.Type == "Trong nước"
                         select new AuthorViewModel()
                         {
                              Author = a,
                              numberBooks = grouping.Count(b => (bool)b.Status)
                         })
                        .AsEnumerable();
               return ls;
          }
          public IEnumerable<AuthorViewModel> ListLocal(int numbers)
          {
               var ls = (from b in db.Books
                    group b by b.Author into grouping
                    join a in db.Authors
                    on (long)grouping.Key equals a.ID
                    where a.Type == "Trong nước"
                    select new AuthorViewModel()
                    {
                         Author = a,
                         numberBooks = grouping.Count(b => (bool)b.Status)
                    })
                    .Take(numbers)
                    .AsEnumerable();
               return ls;
          }

          public IEnumerable<AuthorViewModel> ListForgery()
          {
               return db.Books.GroupBy(x => x.Author).Select(x => new 
               {
                    x.Key,
                    numBook = x.Count(b => (bool)b.Status)
               }).Join(db.Authors, g => (long)g.Key, a => a.ID, (g, a) => new { a, g.numBook })
               .OrderByDescending(x => x.numBook)
               .Where(x => x.a.Type == "Nước ngoài")
               .Select(x => new AuthorViewModel()
               {
                    Author = x.a,
                    numberBooks = x.numBook
               }).AsEnumerable()
               .AsEnumerable();
          }
          public IEnumerable<AuthorViewModel> ListForgery(int numbers)
          {
               return db.Books.GroupBy(x => x.Author).Select(x => new
               {
                    x.Key,
                    numBook = x.Count(b => (bool)b.Status)
               }).Join(db.Authors, g => (long)g.Key, a => a.ID, (g, a) => new { a, g.numBook })
               .OrderByDescending(x => x.numBook)
               .Where(x => x.a.Type == "Nước ngoài")
               .Select(x => new AuthorViewModel()
               {
                    Author = x.a,
                    numberBooks = x.numBook
               }).AsEnumerable()
               .Take(numbers)
               .AsEnumerable();
          }

          //public IEnumerable<AuthorViewModel> ListForgery()
          //{
          //     return db.Books.GroupBy(x => x.Author).Select(x => new AuthorViewModel()
          //     {
          //          x.Key,
          //          numBook = x.Count(b => (bool)b.Status)
          //     }).Join(db.Authors, g => (long)g.Key, a => a.ID, (g, a) => new { a, g.numBook })
          //     .OrderByDescending(x => x.numBook)
          //     .Select(x => x.a).AsEnumerable()
          //     .Where(a => a.Type == "Nước ngoài")
          //     .Take(5)
          //     .AsEnumerable();
          //}
          public Author TakeAuthor(long id)
          {
               return db.Authors.Find(id);
          }

          // Admin
          public List<Author> ListAll()
          {
               return db.Authors.ToList();
          }
          public IEnumerable<Author> ListAllPaging(string searchString, int page, int pageSize)
          {
               IQueryable<Author> model = db.Authors;
               if (!string.IsNullOrEmpty(searchString))
               {
                    model = model.Where(x => x.Name.Contains(searchString));

               }
               return model.OrderByDescending(x => x.DateOfBirth).ToPagedList(page, pageSize);

          }

          public bool Update(Author entity)
          {
               try
               {
                    var model = db.Authors.Find(entity.ID);
                    model.Name = entity.Name;
                    model.DateOfBirth = entity.DateOfBirth;
                    model.Description = entity.Description;
                    model.Type = entity.Type;
                    model.Image = entity.Image;
                    db.SaveChanges();
                    return true;


               }
               catch (Exception ex)
               {
                    return false;
               }


          }


          public Author ViewDetail(long id)
          {
               return db.Authors.Find(id);
          }

          public bool Delete(int id)
          {
               try
               {
                    var bk = db.Authors.Find(id);
                    db.Authors.Remove(bk);
                    var book = db.Books.Where(x => x.Author == id).ToList();
                    if (book != null)
                    {
                         foreach (var a in book)
                         {
                              db.Books.Remove(a);
                         }
                    }

                    db.SaveChanges();
                    return true;

               }
               catch (Exception)
               {
                    return false;
               }


          }
          public bool SetNull(int id)
          {
               try
               {
                    var bk = db.Authors.Find(id);
                    var book = db.Books.Where(x => x.Author == id).ToList();
                    if (book != null)
                    {
                         foreach (var a in book)
                         {
                              a.Author = null;
                         }
                    }

                    db.Authors.Remove(bk);
                    db.SaveChanges();
                    return true;

               }
               catch (Exception)
               {
                    return false;
               }


          }

          public bool addAuthor(Author entity)
          {
               try
               {
                    Author model = new Author();
                    model.Name = entity.Name;
                    model.DateOfBirth = entity.DateOfBirth;
                    model.Description = entity.Description;
                    model.Type = entity.Type;
                    model.Image = entity.Image;

                    db.Authors.Add(model);
                    db.SaveChanges();
                    return true;
               }
               catch
               {
                    return false;
               }


          }
          public List<string> Form()
          {
               var listForm = new List<string>();
               listForm.Add("Trong nước");
               listForm.Add("Nước ngoài");

               return listForm;
          }

          public List<string> ListName(string c)
          {
               return db.Authors.Where(x => x.Name.Contains(c)).Select(x => x.Name).ToList();
          }

     }
}