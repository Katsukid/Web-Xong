using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookMVC.Models;
using BookMVC.Entities;
using System.Data.SqlClient;
using System.Data;

namespace BookMVC.Dao
{
     public class DomainDao
     {
          public BookMVCDbContext db ;
          public DomainDao()
          {
              db = new BookMVCDbContext();
          }
          //public string connectionString = "Data Source=KATSUKID ;Initial Catalog=BookShop;integrated security=true;Trusted_Connection=True;";
          //public List<BookViewModel> Search(string keyword)
          //{

          //     return db.
          //}
          public List<BookViewModel> Filtered(long? author, long? bookcategory, DateTime? publishdate, decimal? lowprice, decimal? highprice,long? publisher)
          {
               if (author == 0) author = null;
               if (bookcategory == 0) bookcategory = null;
               if (publisher == 0) publisher = null;
               if (highprice < lowprice || highprice == 0) highprice = lowprice = null;
               bool check = false;
               string query = "SELECT b.ID AS ID,b.Name AS Name,a.Name AS Author, r.Name AS Released,"
                              + "p.Name AS Publisher, b.CategoryID AS CategoryID,"
                              + "b.Price AS Price,b.PromotionPrice AS PromotionPrice,"
                              + "b.ViewCount AS ViewCount,b.Inventory AS Inventory,"
                              + "b.Author AS AuthorID, b.Released AS ReleasedID, b.Publisher AS PublisherID,"
                              + "b.Buys AS Buys, b.NumberPage AS NumberPage, b.[Weight] AS [Weight],"
                              + "b.[Image] AS [Image], b.PublishDate AS PublishDate, b.[Description] AS [Description] "
                              + "FROM Book AS b "
                              + "JOIN Author AS a ON b.Author=a.ID "
                              + "JOIN BookCategory AS bc ON b.CategoryID=bc.ID "
                              + "JOIN Publisher AS p ON b.Publisher = p.ID "
                              + "JOIN Publisher AS r ON b.Released = r.ID ";
               if (author != null || bookcategory != null || publishdate != null || (lowprice != null && highprice != null) || publisher != null)
                    query += " WHERE";
               //     Nếu có yêu cầu tác giả
               if (author != null)
               {
                    check = true;
                    query += " b.Author = @author";
               }
               //     Nếu có yêu cầu loại sách
               if (bookcategory != null)
               {
                    if (check)
                    {
                         query += " AND";
                    }
                    else
                    {
                         check = true;
                    }
                    query += " b.CategoryID = @bookcategory";
               }
               //     Nếu có yêu cầu ngày xuất bản
               if (publishdate != null)
               {
                    if (check)
                    {
                         query += " AND";
                    }
                    else
                    {
                         check = true;
                    }
                    query += " b.PublishDate > @publishdate";
               }
               //     Nếu có yêu cầu khoảng giá
               if (lowprice != null && highprice != null)
               {
                    if (check)
                    {
                         query += " AND";
                    }
                    else
                    {
                         check = true;
                    }
                    query += " b.PromotionPrice BETWEEN @lowprice AND @highprice";
               }
               //     Nếu có yêu cầu nhà xuất bản
               if (publisher != null)
               {
                    if (check)
                    {
                         query += " AND";
                    }
                    else
                    {
                         check = true;
                    }
                    query += " b.Publisher = @publisher";
               }
               bool check2 = false;
               var pra_author = new[] { new SqlParameter("@author", author) };
               var pra_bookcategory = new[] { new SqlParameter("@bookcategory", bookcategory) };
               var pra_publishdate = new[] { new SqlParameter("@publishdate", publishdate) };
               var pra_lowprice = new[] { new SqlParameter("@lowprice", lowprice) };
               var pra_highprice = new[]{ new SqlParameter("@highprice", highprice) };
               var pra_publisher = new[] { new SqlParameter("@publisher", publisher) };
               SqlParameter[] parameters = new SqlParameter[1];
               if (author != null)
               {
                    check2 = true;
                    parameters = pra_author;
               }
               if(bookcategory != null)
               {
                    if (check2)
                         parameters = parameters.Concat(pra_bookcategory).ToArray();
                    else
                    {
                         check2 = true;
                         parameters = pra_bookcategory;
                    }
               }
               if (publishdate != null)
               {
                    if (check2)
                         parameters = parameters.Concat(pra_publishdate).ToArray();
                    else
                    {
                         check2 = true;
                         parameters = pra_publishdate;
                    }
               }
               if (lowprice != null && highprice != null)
               {
                    if (check2)
                    {
                         parameters = parameters.Concat(pra_lowprice).ToArray();
                         parameters = parameters.Concat(pra_highprice).ToArray();
                    }                         
                    else
                    {
                         check2 = true;
                         parameters = pra_lowprice;
                         parameters = parameters.Concat(pra_highprice).ToArray();
                    }
               }
               if (publisher != null)
               {
                    if (check2)
                         parameters = parameters.Concat(pra_publisher).ToArray();
                    else
                    {
                         check2 = true;
                         parameters = pra_publisher;
                    }
               }
               var lsBook = db.Database.SqlQuery<BookViewModel>(query, parameters);
               

               return lsBook.ToList();
          }

          public List<BookViewModel> Filtered2(long? author, long? bookcategory, DateTime? publishdate, decimal? lowprice, decimal? highprice,string keyword)
          {
               if (author == 0) author = null;
               if (bookcategory == 0) bookcategory = null;
               if (highprice < lowprice || highprice == 0) highprice = lowprice = null;
               string query = "SELECT b.ID AS ID,b.Name AS Name,a.Name AS Author, r.Name AS Released,"
                              + "p.Name AS Publisher, b.CategoryID AS CategoryID,"
                              + "b.Price AS Price,b.PromotionPrice AS PromotionPrice,"
                              + "b.ViewCount AS ViewCount,b.Inventory AS Inventory,"
                              + "b.Author AS AuthorID, b.Released AS ReleasedID, b.Publisher AS PublisherID,"
                              + "b.Buys AS Buys, b.NumberPage AS NumberPage, b.[Weight] AS [Weight],"
                              + "b.[Image] AS [Image], b.PublishDate AS PublishDate, b.[Description] AS [Description] "
                              + "FROM Book AS b "
                              + "JOIN Author AS a ON b.Author=a.ID "
                              + "JOIN BookCategory AS bc ON b.CategoryID=bc.ID "
                              + "JOIN Publisher AS p ON b.Publisher = p.ID "
                              + "JOIN Publisher AS r ON b.Released = r.ID "
                              + "WHERE (b.Author = @author OR @author = 0 ) "
                              + "AND (b.CategoryID = @bookcategory OR @bookcategory = 0) "
                              + "AND (b.PublishDate > @publishdate OR @publishdate = '01-01-1800') "
                              + "AND ((b.PromotionPrice BETWEEN @lowprice AND @highprice) OR (@lowprice = 0 OR @highprice = 0))";
               long nul = 0;
               DateTime nuldate = new DateTime(1800,1,1);
               var pra_author = new[] { new SqlParameter("@author", author) };
               var pra_author_null = new[] { new SqlParameter("@author", nul) };
               var pra_bookcategory = new[] { new SqlParameter("@bookcategory", bookcategory) };
               var pra_bookcategory_null = new[] { new SqlParameter("@bookcategory", nul) };
               var pra_publishdate = new[] { new SqlParameter("@publishdate", publishdate) };
               var pra_publishdate_null = new[] { new SqlParameter("@publishdate", nuldate) };
               var pra_lowprice = new[] { new SqlParameter("@lowprice", lowprice) };
               var pra_lowprice_null = new[] { new SqlParameter("@lowprice", nul) };
               var pra_highprice = new[] { new SqlParameter("@highprice", highprice) };
               var pra_highprice_null = new[] { new SqlParameter("@highprice", nul) };
               SqlParameter[] parameters = new SqlParameter[1];
               if (author != null)
               {
                    parameters = pra_author;
               }
               else
               {
                    parameters = pra_author_null;
               }
               if (bookcategory != null)
               {                    
                         parameters = parameters.Concat(pra_bookcategory).ToArray();                   
               }
               else
               {
                    parameters = parameters.Concat(pra_bookcategory_null).ToArray();
               }
               if (publishdate != null)
               {
                    parameters = parameters.Concat(pra_publishdate).ToArray();
               }
               else
               {
                    parameters = parameters.Concat(pra_publishdate_null).ToArray();
               }
               if (lowprice != null && highprice != null)
               {
                    parameters = parameters.Concat(pra_lowprice).ToArray();
                    parameters = parameters.Concat(pra_highprice).ToArray();
               }
               else
               {
                    parameters = parameters.Concat(pra_lowprice_null).ToArray();
                    parameters = parameters.Concat(pra_highprice_null).ToArray();
               }
               var lsBook = db.Database.SqlQuery<BookViewModel>(query, parameters);


               return lsBook.ToList();
          }
     }
}