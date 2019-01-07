

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookMVC.Entities;
using BookMVC.Dao;
using BookMVC.Models;
using BookMVC.Areas.admins.Models;
using PagedList;
namespace BookMVC.Dao
{
     public class OrderDao
     {
          BookMVCDbContext db = null;
          public OrderDao()
          {
               db = new BookMVCDbContext();
          }

          public bool checkOrderExsit(long orderID) => TakeOrder(orderID) != null ? true : false;

          public Order CreateOrder(long UserID)
          {
               var user = new UserDao().GetUser(UserID);
               var order = new Order()
               {
                    CreateDate = DateTime.Now,
                    CreatID = UserID,
                    ShipName = user.Name,
                    ShipMobile = user.Phone,
                    ShipAdress = user.Address,
                    ShipEmail = user.Email,
                    Status = 0,
                    ShipTypeID = null,
                    ShippedDate = null,
                    Shiper = null
               };
               return order;
          }

          public Order TakeOrder(long orderID)
          {
               return db.Orders.Where(x => x.ID == orderID).SingleOrDefault();
          }

          

          public long SaveOrder(Order order)
          {
               db.Orders.Add(order);
               db.SaveChanges();
               var orderID = db.Orders.AsEnumerable().Last().ID;
               return orderID;
          }

          public bool DeleteOrder(long orderID)
          {
               if (checkOrderExsit(orderID))
               {
                    var order = TakeOrder(orderID);
                    db.Orders.Remove(order);
                    db.SaveChanges();
                    return true;
               }
               return false;
          }

          public List<Order> ListOrders(long userID)
          {
               return db.Orders.Where(x => x.CreatID == userID).ToList();
          }
          public List<OrderDetailViewModel> ListOrderItems(long orderID)
          {
               return db.Orders
                    .Where(x => x.ID == orderID)
                    .Join(db.Order_Detail, o => o.ID, od => od.OrderID, (o, od) => od)
                    .Join(db.Books, od => od.BookID, b => b.ID, (od,b) => new OrderDetailViewModel() { orderdetail = od,book = b})
                    .ToList();
          }
          public bool CancelOrder(long orderID)
          {
               try
               {
                    var order = db.Orders.Where(x => x.ID == orderID).SingleOrDefault();
                    if (order == null)
                         return false;
                    order.Status = -1;
                    db.SaveChanges();
                    return true;
               }
               catch
               {
                    return false;
               }
          }
          public bool UnDisplayOrder(long orderID)
          {
               var order = db.Orders.Where(x => x.ID == orderID).SingleOrDefault();
               if (order == null)
                    return false;
               order.Status = 5;
               db.SaveChanges();
               return true;
          }
          /* 
           * Admin
           * 
           * 
           * 
           * 
           */
          public Order FindID(long id)
          {
               return db.Orders.Where(x => x.ID == id).SingleOrDefault();
          }
          public IEnumerable<Order> ListAll(int page, int pagesize)
          {
               return db.Orders.OrderByDescending(x => x.CreateDate).ToPagedList(page, pagesize);
          }
          public IEnumerable<OrderModel> ListAllPage(string searching, int page, int pageSize)
          {
               var model = (from a in db.Orders
                            join b in db.ShippingTypes
                            on a.ShipTypeID equals b.ID
                            join c in db.Users
                            on a.Shiper equals c.ID into g
                            from y in g.DefaultIfEmpty()
                            select new OrderModel()
                            {
                                 order = a,
                                 TypeShip = b.TypeShip,
                                 NameShipper = y.Name
                            });


               if (!string.IsNullOrEmpty(searching))
               {
                    model = model.Where(x => x.order.ShipName.Contains(searching));
               }
               return model.OrderByDescending(x => x.order.CreateDate).ToPagedList(page, pageSize);


          }
          public bool AddOrder(Order entity)
          {
               var order = new Order();
               try
               {
                    order.ID = entity.ID;
                    order.ShipName = entity.ShipName;
                    order.CreatID = entity.CreatID;
                    order.Shiper = entity.Shiper;
                    order.ShipAdress = entity.ShipAdress;
                    order.ShipEmail = entity.ShipEmail;
                    order.ShipMobile = entity.ShipMobile;
                    order.ShipTypeID = entity.ShipTypeID;
                    order.CouponSerial = entity.CouponSerial;
                    order.CreateDate = entity.CreateDate;
                    order.CreatID = entity.CreatID;
                    order.Status = entity.Status;
                    order.TotalPrice = entity.TotalPrice;
                    order.ShippedDate = entity.ShippedDate;
                    db.Orders.Add(order);
                    db.SaveChanges();
                    return true;
               }
               catch
               {

                    return false;
               }
          }
          public bool EditOrder(Order entity)
          {
               var order = new Order();
               order = FindID(entity.ID);
               order.Shiper = entity.Shiper;
               order.ShipTypeID = entity.ShipTypeID;
               order.Status = entity.Status;
               order.ShippedDate = null;
               if (entity.Status == 3)
               {
                    order.ShippedDate = DateTime.Now;
               }
               db.SaveChanges();
               return true;

          }
          public bool Delete(int id)
          {
               try
               {
                    var bk = db.Orders.Find(id);
                    db.Orders.Remove(bk);
                    db.SaveChanges();
                    return true;

               }
               catch (Exception)
               {
                    return false;
               }


          }
          // tong doanh thu theo thang
          public decimal? Sum(int month, int year)
          {
               return db.Orders.Where(x => x.CreateDate.Value.Month == month && x.CreateDate.Value.Year == year).Sum(x => x.TotalPrice);
          }
          public IEnumerable<Order> TakeInMonthOfYear(int? Month, int? Year, int page, int pagesize)
          {
               return db.Orders.Where(x => x.ShippedDate.Value.Month == Month && x.ShippedDate.Value.Year == Year).OrderByDescending(x => x.CreateDate).ToPagedList(page, pagesize);
          }
          public List<string> Listsearch(string search)
          {
               return db.Orders.Where(x => x.ShipName.Contains(search)).Select(x => x.ShipName).ToList();
          }
          //public bool ChangeStatus(int id)
          //{
          //    var k = db.Order.Find(id);
          //    k.Status = !k.Status;
          //    db.SaveChanges();
          //    return (bool)k.Status;
          //}

     }
}