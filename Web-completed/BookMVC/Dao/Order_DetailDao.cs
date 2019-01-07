using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookMVC.Entities;
using BookMVC.Models;
namespace BookMVC.Dao
{
     public class Order_DetailDao
     {
          BookMVCDbContext db;
          public Order_DetailDao()
          {
               db = new BookMVCDbContext();
          }
          public List<OrderDetailViewModel> OrderItems(List<CartItemDetail> lsSelected)
          {
               var ls = new List<OrderDetailViewModel>();
               foreach(var i in lsSelected) {
                    if (i.Selected)
                    {
                         var item = new OrderDetailViewModel() {
                              book = new BookDao().FindByID(i.ItemID),
                              orderdetail = new Order_Detail()
                              {
                                   BookID = i.ItemID,
                                   Quantity = i.Quantity,
                                   Price = i.PromotionPrice
                              }
                         };
                         ls.Add(item);
                    }                    
               }
               return ls;
          }

          public Order_Detail TakeOderItem(long orderID,long ItemID)
          {
              return db.Order_Detail.Where(x => x.OrderID == orderID && x.BookID == ItemID).SingleOrDefault();
          }
          
          //public OrderDetailViewModel TakeOrderDetail() {

          //}
          public bool SaveOrder_Detail(long orderID,Order_Detail Item)
          {
               try
               {
                    Item.OrderID = orderID;
                    db.Order_Detail.Add(Item);
                    db.SaveChanges();
                    return true;
               }
               catch
               {
                    return false;
               }
               
          }

          public void DeleteOrderItem(long orderID, long ItemID)
          {
               var item = TakeOderItem(orderID, ItemID);
               db.Order_Detail.Remove(item);
               db.SaveChanges();
          }
     }
}