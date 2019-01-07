using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookMVC.Models;
using BookMVC.Entities;
namespace BookMVC.Dao
{
     public class SearchDao
     {
          public BookMVCDbContext db = new BookMVCDbContext();
          public List<SearchViewModel> List(string word)
          {
               if (word == "" || word == null)
                    return null;
               var temp = new List<SearchViewModel>();
               var book = db.Books
                    .Where(x => x.Name.Contains(word))
                    .Select(x => new SearchViewModel {
                         id = x.ID,
                         KeyWord = x.Name,
                         type = 1
                    }).ToList();
               var author = db.Authors
                    .Where(x => x.Name.Contains(word))
                    .Select(x => new SearchViewModel {
                         id = x.ID,
                         KeyWord = x.Name,
                         type = 2
                    }).ToList();
               var publisher = db.Publishers
                    .Where(x => x.Name.Contains(word))
                    .Select(x => new SearchViewModel {
                         id = x.ID,
                         KeyWord = x.Name,
                         type = 3
                    }).ToList();
               foreach(var b in book)
               {
                    temp.Add(b);
               }
               foreach (var a in author)
               {
                    temp.Add(a);
               }
               foreach (var p in publisher)
               {
                    temp.Add(p);
               }
               return temp.Take(10).ToList();
          }
     }
}