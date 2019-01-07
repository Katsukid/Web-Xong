using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookMVC.Entities;
using BookMVC.Models;
using BookMVC.Areas.admins.Models;
using PagedList;

namespace BookMVC.Dao
{
     public class NewDao
     {
          BookMVCDbContext db;
          public NewDao()
          {
               db = new BookMVCDbContext();
          }
          public List<NewsViewModels> TakeNews(int count)
          {
               var ls = (from b in db.News
                         join t in db.NewsTypes
                         on b.TypeID equals t.ID
                         orderby b.CreatedDate
                         select new NewsViewModels
                         {
                              ID = b.ID,
                              Author = b.Author,
                              Content = b.Content,
                              Created = (long)b.Created,
                              CreatedDate = (DateTime)b.CreatedDate,
                              isHomePage = b.isHomePage,
                              isPublic = (bool)b.isPublic,
                              Title = b.Title,
                              NameType = t.Name
                         }).Take(count).ToList();
               return ls;
          }
          public NewsViewModels TakeNews(long id)
          {
               return db.News.Join(db.NewsTypes, n => n.TypeID, nt => nt.ID, (n, nt) => new NewsViewModels()
               {
                    ID = n.ID,
                    Author = n.Author,
                    Content = n.Content,
                    Created = (long)n.Created,
                    CreatedDate = (DateTime)n.CreatedDate,
                    isHomePage = n.isHomePage,
                    isPublic = (bool)n.isPublic,
                    Title = n.Title,
                    NameType = nt.Name
               }).Where(x => x.ID == id).SingleOrDefault();
          }


          /*
           * Admin
           * 
           * 
           * 
           * 
           * 
           */

          public IEnumerable<News> ListAllPaging(string searchString, int page, int pageSize)
          {
               IQueryable<News> model = db.News;
               if (!string.IsNullOrEmpty(searchString))
               {
                    model = model.Where(x => x.Title.Contains(searchString));

               }
               return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);

          }
          public IEnumerable<NewsViewModel> ListAllByTag(string searchString, int page, int pageSize)
          {
               var news = (from a in db.News
                           join b in db.NewsTypes
                           on a.TypeID equals b.ID
                           join c in db.Admins
                           on a.Created equals c.ID
                           select new NewsViewModel
                           {
                                news = a,
                                NameAdmin = c.UserName,
                                NameType = b.Name
                           });
               if (!string.IsNullOrEmpty(searchString))
               {
                    news = news.Where(x => x.news.Title.Contains(searchString) || x.NameType.Contains(searchString));

               }
               return news.OrderByDescending(x => x.news.CreatedDate).ToPagedList(page, pageSize);
          }

          public bool AddNew(News entity, long u)
          {
               try
               {
                    var news = new News();
                    news.ID = entity.ID;
                    news.Title = entity.Title;
                    news.Created = u;
                    news.CreatedDate = DateTime.Now;
                    news.isPublic = true;
                    news.Author = entity.Author;
                    news.Content = entity.Content;
                    news.isHomePage = entity.isHomePage;
                    news.Image = entity.Image;
                    news.TypeID = entity.TypeID;
                    db.News.Add(news);
                    db.SaveChanges();
                    return true;
               }
               catch
               {
                    return false;
               }
          }
          public News FindID(long id)
          {
               return db.News.Find(id);
          }

          public bool EditNew(News entity)
          {
               try
               {
                    var news = FindID(entity.ID);
                    news.ID = entity.ID;
                    news.Title = entity.Title;

                    news.CreatedDate = DateTime.Now;
                    news.isPublic = true;
                    news.Author = entity.Author;
                    news.Content = entity.Content;
                    news.isHomePage = entity.isHomePage;
                    news.Image = entity.Image;
                    news.TypeID = entity.TypeID;

                    db.SaveChanges();
                    return true;
               }
               catch
               {
                    return false;
               }
          }
          public bool DeleteNew(long id)
          {
               try
               {
                    var slide = db.News.Find(id);
                    db.News.Remove(slide);
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
               var slide = db.News.Find(id);
               slide.isPublic = !slide.isPublic;
               db.SaveChanges();
               return (bool)slide.isPublic;
          }
          public News ViewDetail(int id)
          {
               return db.News.Find(id);
          }
          public List<string> Listsearch(string search)
          {
               var model = (from a in db.News
                            join b in db.NewsTypes
                            on a.TypeID equals b.ID
                            join c in db.Admins
                            on a.Created equals c.ID
                            select new NewsViewModel
                            {
                                 news = a,
                                 NameAdmin = c.UserName,
                                 NameType = b.Name
                            });
               var ls = (from x in model
                         where x.news.Title.Contains(search)
                         select x.news.Title).ToList();
               var lsNameType = (from x in model
                                 group x by x.NameType into g
                                 where g.Key.Contains(search)
                                 select g.Key).AsEnumerable();
               ls.AddRange(lsNameType);
               return ls;
          }
     }
}