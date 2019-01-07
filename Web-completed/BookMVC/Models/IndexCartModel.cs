using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using BookMVC.Entities;
using BookMVC.Models;
namespace BookMVC.Models
{
     public class IndexCartModel
     {
          public decimal? totalPrice { get; set; }
          public decimal? totalPromotion { get; set; }
          public decimal? totalQuantity { get; set; }
          public List<BookViewModel> listHotBook { get; set; }
          public List<CartItemDetail> cart { get; set; }
     }
}