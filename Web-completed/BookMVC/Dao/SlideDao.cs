using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookMVC.Entities;
using BookMVC.Dao;
using BookMVC.Areas.admins.Models;
using PagedList;

namespace BookMVC.Dao
{
     public class SlideDao
     {
          BookMVCDbContext db = null;
          public SlideDao()
          {
               db = new BookMVCDbContext();
          }
          public List<Slide> TakeSlide(int count,string where)
          {
               return db.Slides.Where(x => x.Where == where).Take(count).ToList();
          }
          public List<string> ListWhere()
          {
               var list = (from a in db.Slides
                           group a by a.Where into g
                           select new
                           {
                                listwhere = g.Key
                           }).Select( x => x.listwhere).ToList();
               return list;
          }


          /* Admin
           * 
           * 
           * 
           * 
           * 
           */
          public IEnumerable<Slide> ListAllPaging(int page, int pageSize)
          {
               return db.Slides.OrderByDescending(x => x.DisplayOrder).ToPagedList(page, pageSize);

          }
          public IEnumerable<SlideModel> List(string searching, int page, int pagesize)
          {
               var slide = (from a in db.Slides
                            join b in db.Books
                            on a.BookID equals b.ID
                            select new SlideModel
                            {
                                 slide = a,
                                 NameBook = b.Name
                            });
               if (!string.IsNullOrEmpty(searching))
               {
                    slide = slide.Where(x => x.NameBook.Contains(searching));
               }
               return slide.OrderByDescending(x => x.NameBook).ToPagedList(page, pagesize);
          }
          public bool AddSlide(Slide entity)
          {
               try
               {
                    var slide = new Slide();
                    slide.BookID = entity.BookID;
                    slide.CreatedDate = DateTime.Now;
                    slide.DisplayOrder = 1;
                    slide.Image = entity.Image;
                    slide.Where = entity.Where;
                    slide.Status = true;
                    db.Slides.Add(slide);
                    db.SaveChanges();
                    return true;
               }
               catch
               {
                    return false;
               }
          }
          public Slide FindID(long id)
          {
               return db.Slides.Find(id);
          }
          //Sửa slide
          public bool EditSlide(Slide entity)
          {
               try
               {
                    var slide = FindID(entity.ID);
                    slide.Image = entity.Image;
                    slide.BookID = entity.BookID;
                    slide.Where = entity.Where;
                    slide.DisplayOrder =entity.DisplayOrder;
                    slide.CreatedDate = DateTime.Now;
                    slide.Status = true;
                    db.SaveChanges();
                    return true;
               }
               catch
               {
                    return false;
               }
          }
          public bool DeleteSlide(long id)
          {
               try
               {
                    var slide = db.Slides.Find(id);
                    db.Slides.Remove(slide);
                    db.SaveChanges();
                    return true;
               }
               catch (Exception e)
               {
                    return false;
               }
          }
          public bool ChangeStatus(long id)
          {
               var slide = db.Slides.Find(id);
               slide.Status = !slide.Status;
               db.SaveChanges();
               return (bool)slide.Status;
          }
          public List<string> ListAll()
          {
               //var list = (from a in db.Slide
               //            group a by a.Where into g
               //            select new
               //            {
               //                listwhere = g.Key
               //            }).Select(x => x.listwhere).ToList();
               //return list;
               var listForm = new List<string>();
               listForm.Add("top");
               listForm.Add("bot");
               listForm.Add("left");
               listForm.Add("right");

               return listForm;
          }
          public List<string> ListName(string c)
          {
               var slide = (from a in db.Slides
                            join b in db.Books
                            on a.BookID equals b.ID
                            select new SlideModel
                            {
                                 slide = a,
                                 NameBook = b.Name
                            });
               return slide.Where(x => x.NameBook.Contains(c)).Select(x => x.NameBook).ToList();
          }
     }
}