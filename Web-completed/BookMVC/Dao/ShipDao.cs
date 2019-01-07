using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookMVC.Entities;
using PagedList;

namespace BookMVC.Dao
{
     public class ShipDao
     {
          BookMVCDbContext db;
          public ShipDao()
          {
               db = new BookMVCDbContext();
          }
          public List<ShippingType> ListAll()
          {
               return db.ShippingTypes.OrderByDescending(x => x.Cost).ToList();
          }
          public ShippingType TakeByID(long id)
          {
               return db.ShippingTypes.Find(id);
          }


          /* Admin
           * 
           * 
           * 
           * 
           * 
           */

          public IEnumerable<ShippingType> ListAllPaging(string searchString, int page, int pageSize)
          {
               IQueryable<ShippingType> model = db.ShippingTypes;
               if (!string.IsNullOrEmpty(searchString))
               {
                    model = model.Where(x => x.TypeShip.Contains(searchString));

               }
               return model.OrderByDescending(x => x.Cost).ToPagedList(page, pageSize);

          }

          public bool AddShip(ShippingType entity)
          {
               try
               {
                    var ship = new ShippingType();
                    ship.ID = entity.ID;
                    ship.TypeShip = entity.TypeShip;
                    ship.Cost = entity.Cost;
                    ship.Time = entity.Time;
                    db.ShippingTypes.Add(ship);
                    db.SaveChanges();
                    return true;
               }
               catch
               {
                    return false;
               }
          }
          public ShippingType FindID(long id)
          {
               return db.ShippingTypes.Find(id);
          }

          public bool EditShip(ShippingType entity)
          {
               try
               {
                    var ship = FindID(entity.ID);
                    ship.ID = entity.ID;
                    ship.TypeShip = entity.TypeShip;
                    ship.Cost = entity.Cost;
                    ship.Time = entity.Time;
                    db.SaveChanges();
                    return true;
               }
               catch
               {
                    return false;
               }
          }
          public bool Deleteship(long id)
          {
               try
               {
                    var slide = db.ShippingTypes.Find(id);
                    db.ShippingTypes.Remove(slide);
                    db.SaveChanges();
                    return true;
               }
               catch (Exception e)
               {
                    return false;
               }
          }
          //public ShippingType ViewDetail(int id)
          //{
          //    return db.ShippingType.Find(id);
          //}
          public List<string> Listsearch(string search)
          {
               return db.ShippingTypes.Where(x => x.TypeShip.Contains(search)).Select(x => x.TypeShip).ToList();
          }
          //public List<ShippingType> ListAll()
          //{
          //     return db.ShippingTypes.ToList();
          //}
     }
}