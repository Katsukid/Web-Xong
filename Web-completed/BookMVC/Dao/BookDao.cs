using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using BookMVC.Entities;
using BookMVC.Models;
using PagedList;
using PagedList.Mvc;
using BookMVC.Areas.admins.Models;
using System.Data.Entity.Validation;

namespace BookMVC.Dao
{
     public class BookDao
     {
          BookMVCDbContext db;
          public BookDao()
          {
               db = new BookMVCDbContext();
          }
          // Lay toan bo sach
          public List<BookViewModel> _ListAll()
          {
               return (from b in db.Books
                         join a in db.Authors on b.Author equals a.ID
                         join p in db.Publishers on b.Publisher equals p.ID
                         join r in db.Publishers on b.Released equals r.ID
                         select new BookViewModel()
                         {
                              ID = b.ID,
                              Code = b.Code,
                              Name = b.Name,
                              Author = a.Name,
                              AuthorID = b.Author,
                              Publisher = p.Name,
                              PublisherID = b.Publisher,
                              Released = r.Name,
                              ReleasedID = b.Released,
                              NumberPage = b.NumberPage,
                              Weight = b.Weight,
                              Form = b.Form,
                              PublishDate = b.PublishDate,
                              Buys = b.Buys,
                              Price = b.Price,
                              PromotionPrice = b.PromotionPrice,
                              CategoryID = b.CategoryID,
                              ViewCount = b.ViewCount,
                              LikeCount = b.LikeCount,
                              Inventory = b.Inventory,
                              Image = b.Image,
                              Status = b.Status,
                              Description = b.Description
                         }).ToList();
          }
          // Lay mot vai quyen sach
          public List<BookViewModel> ListSomeBook(int number)
          {
               return (from b in db.Books
                       join a in db.Authors on b.Author equals a.ID
                       join p in db.Publishers on b.Publisher equals p.ID
                       join r in db.Publishers on b.Released equals r.ID
                       select new BookViewModel()
                       {
                            ID = b.ID,
                            Code = b.Code,
                            Name = b.Name,
                            Author = a.Name,
                            AuthorID = b.Author,
                            Publisher = p.Name,
                            PublisherID = b.Publisher,
                            Released = r.Name,
                            ReleasedID = b.Released,
                            NumberPage = b.NumberPage,
                            Weight = b.Weight,
                            Form = b.Form,
                            PublishDate = b.PublishDate,
                            Buys = b.Buys,
                            Price = b.Price,
                            PromotionPrice = b.PromotionPrice,
                            CategoryID = b.CategoryID,
                            ViewCount = b.ViewCount,
                            LikeCount = b.LikeCount,
                            Inventory = b.Inventory,
                            Image = b.Image,
                            Status = b.Status,
                            Description = b.Description
                       }).Take(number).ToList();
          }
          // Lay 1 quyen
          public BookViewModel TakeBook(long id)
          {
               var book = (from b in db.Books
                           join a in db.Authors on b.Author equals a.ID
                           join p in db.Publishers on b.Publisher equals p.ID
                           join r in db.Publishers on b.Released equals r.ID
                           where (b.ID == id)
                           select new BookViewModel()
                           {
                                ID = b.ID,
                                Code = b.Code,
                                Name = b.Name,
                                Author = a.Name,
                                AuthorID = b.Author,
                                Publisher = p.Name,
                                PublisherID = b.Publisher,
                                Released = r.Name,
                                ReleasedID = b.Released,
                                NumberPage = b.NumberPage,
                                Weight = b.Weight,
                                Form = b.Form,
                                PublishDate = b.PublishDate ,
                                Buys = b.Buys,
                                Price = b.Price ,
                                PromotionPrice = b.PromotionPrice,
                                CategoryID = b.CategoryID,
                                ViewCount = b.ViewCount,
                                LikeCount = b.LikeCount,
                                Inventory = b.Inventory,
                                Image = b.Image,
                                Status = b.Status,
                                Description = b.Description
                           }).Single();
               return book;
          }
          // Lay danh sach
          // Sach moi ( < days ngay)
          public List<BookViewModel> ListNewBook(int days)
          {
               //return ListAll().Where(x => DbFunctions.AddDays(DateTime.Now,(int?)-days) <= x.Book.PublishDate).OrderByDescending(x => x.Book.PublishDate).ToList();
               return _ListAll().Where(x => DateTime.Now.AddDays(-days) <= x.PublishDate).ToList();
          }
          public List<BookViewModel> ListNewBook(int days,int count)
          {
               int hour = days * 24;
               return _ListAll().Where(x => DateTime.Now.AddDays(-days) <= x.PublishDate).Take(count).ToList();
               //return ListAll().Where(x => DateTime.Now - x.Book.PublishDate <= ts).OrderByDescending(x => x.Book.PublishDate).Take(count).ToList();
          }
          // Sap phat hanh
          public List<BookViewModel> ListFutureBook()
          {
               return _ListAll().Where(x => x.PublishDate > DateTime.Now).OrderByDescending(x => x.PublishDate).ToList();
          }
          public List<BookViewModel> ListFutureBook(int count)
          {
               return _ListAll().Where(x => x.PublishDate > DateTime.Now).OrderByDescending(x => x.PublishDate).Take(count).ToList();
          }
          // Sach ban chay
          public List<BookViewModel> ListHotBook(int buys)
          {
               return _ListAll().Where(x => x.Buys >= buys).ToList();
          }
          public List<BookViewModel> ListHotBook(int buys,int numbers)
          {
               return _ListAll().Where(x => x.Buys >= buys).OrderByDescending(x => x.Buys).Take(numbers).ToList();
          }
          // Sach khuyen mai
          public List<BookViewModel> ListPromotionBook()
          {
               return _ListAll().Where(x => x.Price != x.PromotionPrice).ToList();
          }
          public List<BookViewModel> ListPromotionBook(int numbers)
          {
               return _ListAll().Where(x => x.Price != x.PromotionPrice).Take(numbers).ToList();
          }
          // Theo tac gia
          public List<BookViewModel> ListByAuthor(int idAuthor)
          {
               return _ListAll().Where(x => x.AuthorID == idAuthor).Take(6).ToList();
          }
          public List<BookViewModel> ListByAuthor(int idAuthor, int count)
          {
               return _ListAll().Where(x => x.AuthorID == idAuthor).Take(6).ToList();
          }
          // Theo danh muc
          public List<BookViewModel> ListByBookCategory(long? id)
          {
               return _ListAll().Where(x => x.CategoryID == id).ToList();
          }
          public List<BookViewModel> ListByBookCategory(long? id, int count)
          {
               return _ListAll().Where(x => x.CategoryID == id).Take(count).ToList();
          }
          public List<BookViewModel> ListByPublisher(int idPublisher)
          {
               return _ListAll().Where(x => x.PublisherID == idPublisher).ToList();
          }
          public List<BookViewModel> ListByPublisher(int idPublisher, int count)
          {
               return _ListAll().Where(x => x.PublisherID == idPublisher).Take(count).ToList();
          }
          // Lay sach
          // Theo ID
          public Book FindByID(long? id)
          {
               return db.Books.Where(x => x.ID == id).SingleOrDefault();
          }
          // Theo ten
          public Book FindByName(string name)
          {
               return db.Books.Where(x => x.Name == name).SingleOrDefault();
          }
          // Theo ma sach
          public Book FindByCode(string code)
          {
               return db.Books.Where(x => x.Code == code).SingleOrDefault();
          }
          
          // Thao tac
          // Them sach
          public bool AddBook(Book b)
          {
               db.Books.Add(b);
               db.SaveChanges();
               return true;
          }
          // Cap nhat so luot xem
          public void UpdateViews(long? id)
          {
               try
               {
                    var book = db.Books.Find(id);
                    book.ViewCount++;
                    db.SaveChanges();
               }
               catch (Exception ex)
               {

               }
          }
          public bool IsNew(long id)
          {
               var b = TakeBook(id);
               int days = 30;
               return (b.PublishDate >= DateTime.Now.AddDays(-days));
          }
          public bool IsSale(long id)
          {
               var b = TakeBook(id);
               return (b.PromotionPrice != b.Price);
          }
          //Kiểm tra số lượng tồn kho
          public decimal getInventory(long id)
          {
               var inventory = FindByID(id).Inventory;
               if (inventory == null || inventory <= 0) return 0;
               return (decimal)inventory;
          }

          /*
           * Admin
           * 
           * 
           * 
           * 
           */

          public IEnumerable<Book> ListAllPaging(string searchString, int page, int pageSize)
          {
               IQueryable<Book> model = db.Books;
               if (!string.IsNullOrEmpty(searchString))
               {
                    model = model.Where(x => x.Name.Contains(searchString));  //|| x.Author.Contains(searchString))

               }
               return model.OrderByDescending(x => x.PublishDate).ToPagedList(page, pageSize);

          }
          public IEnumerable<BookModel> ListAllByTag(string searchString, int page, int pageSize)
          {
               var news = (from a in db.Books
                           join b in db.BookCategories on a.CategoryID equals b.ID into cates
                           from y1 in cates.DefaultIfEmpty()
                           join c in db.Authors on a.Author equals c.ID into authors
                           from y2 in authors.DefaultIfEmpty()
                           join d in db.Publishers on a.Publisher equals d.ID into pubs
                           from y3 in pubs.DefaultIfEmpty()
                           join f in db.Publishers on a.Released equals f.ID into rels
                           from y4 in rels.DefaultIfEmpty()
                           select new BookModel()
                           {
                                Book = a,
                                NameType = y1.Name,
                                NameAuthor = y2.Name,
                                NamePublisher = y3.Name,
                                NameReleased = y4.Name
                           });
               if (!string.IsNullOrEmpty(searchString))
               {
                    news = news.Where(x => x.Book.Name.Contains(searchString) || x.NameAuthor.Contains(searchString) || x.NamePublisher.Contains(searchString) || x.NameReleased.Contains(searchString));

               }
               return news.OrderByDescending(x => x.Book.PublishDate).ToPagedList(page, pageSize);
          }
          public bool Update(Book entity, string s)
          {
               try
               {
                    var book = db.Books.Find(entity.ID);
                    book.Code = entity.Code;
                    book.Name = entity.Name;
                    book.Author = entity.Author;
                    book.Released = entity.Released;
                    book.Publisher = entity.Publisher;
                    book.Weight = entity.Weight;
                    book.Form = entity.Form;
                    book.NumberPage = entity.NumberPage;
                    book.PublishDate = entity.PublishDate;
                    book.MetaTitle = Str_Metatitle(entity.Name);
                    book.Description = entity.Description;
                    book.Image = entity.Image;
                    book.Price = entity.Price;
                    book.PromotionPrice = entity.PromotionPrice;
                    book.CategoryID = entity.CategoryID;
                    book.CreateBy = s;
                    db.SaveChanges();
                    return true;


               }
               catch (Exception ex)
               {
                    return false;
               }


          }

          public Book GetById(string bookName)
          {
               return db.Books.SingleOrDefault(x => x.Name == bookName);
          }
          public Book ViewDetail(long id)
          {
               return db.Books.Find(id);
          }

          public bool Delete(int id)
          {
               try
               {
                    var bk = db.Books.Find(id);
                    db.Books.Remove(bk);
                    db.SaveChanges();
                    return true;

               }
               catch (Exception)
               {
                    return false;
               }


          }
          //Lấy ra hình thức sách
          public List<string> Form()
          {
               var listForm = new List<string>();
               listForm.Add("Bìa mềm");
               listForm.Add("Bìa cứng");

               return listForm;
          }
          public List<Book> ListAll()
          {
               return db.Books.Where(x => x.Status == true).OrderByDescending(x => x.PublishDate < DateTime.Now).ToList();
          }
          public bool ChangeStatus(int id)
          {
               var book = db.Books.Find(id);
               book.Status = !book.Status;
               db.SaveChanges();
               return (bool)book.Status;
          }

          public bool addBook(Book entity, string s)
          {
               try
               {
                    var book = new Book();
                    book.Code = entity.Code;
                    book.Name = entity.Name;
                    book.Author = entity.Author;
                    book.Released = entity.Released;
                    book.Publisher = entity.Publisher;
                    book.Weight = entity.Weight;
                    book.Form = entity.Form;
                    book.NumberPage = entity.NumberPage;
                    book.PublishDate = entity.PublishDate;
                    book.MetaTitle = Str_Metatitle(entity.Name);
                    book.Description = entity.Description;
                    book.Image = entity.Image;
                    book.Price = entity.Price;
                    book.PromotionPrice = entity.PromotionPrice;
                    book.CategoryID = entity.CategoryID;
                    book.CreateDate = DateTime.Now;
                    book.Status = true;
                    book.CreateBy = s;
                    book.ViewCount = 0;
                    book.LikeCount = 0;
                    book.Inventory = 0;
                    db.Books.Add(book);
                    db.SaveChanges();
                    return true;
               }
               catch (DbEntityValidationException e)
               {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                         var x = "Entity of type " + eve.Entry.Entity.GetType().Name + " in state " + eve.Entry.State + " has the following validation errors:";
                         string Property;
                         string Error;
                         foreach (var ve in eve.ValidationErrors)
                         {
                              Property = ve.PropertyName;
                              Error = ve.ErrorMessage;
                         }
                    }
                    throw;
                    return false;
               }


          }
          //Chuyển tên sách thành metatitle
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
          public List<string> Listsearch(string search)
          {
               var model = (from a in db.Books
                            join b in db.BookCategories on a.CategoryID equals b.ID
                            join c in db.Authors on a.Author equals c.ID
                            join d in db.Publishers on a.Publisher equals d.ID
                            join f in db.Publishers on a.Released equals f.ID
                            select new BookModel()
                            {
                                 Book = a,
                                 NameAuthor = c.Name,
                                 NamePublisher = d.Name,
                                 NameReleased = f.Name,
                                 NameType = b.Name
                            });               
               var ls = (from x in model
                         where x.Book.Name.Contains(search)
                         select x.Book.Name).ToList();
               var lsAuthor = (from x in model
                               group x by x.NameAuthor into g
                               where g.Key.Contains(search)
                               select g.Key).AsEnumerable();
               var lsPulisher = (from x in model
                                 group x by x.NamePublisher into g
                                 where g.Key.Contains(search)
                                 select g.Key).AsEnumerable();
               var lsRealed = (from x in model
                               group x by x.NameReleased into g
                               where g.Key.Contains(search)
                               select g.Key).AsEnumerable();
               ls.AddRange(lsAuthor);
               ls.AddRange(lsPulisher);
               ls.AddRange(lsRealed);
               return ls;
          }
          public bool Import(string Code, int quantity)
          {
               var book = db.Books.Where(x => x.Code == Code).SingleOrDefault();
               if (book == null)
                    return false;
               book.Inventory += quantity;
               db.SaveChanges();
               return true;
          }

     }
}