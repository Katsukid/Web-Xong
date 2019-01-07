using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookMVC.Entities;
namespace BookMVC.Models
{
     public class BookCateVIewModel
     {
          public BookCategory bookCategory { get; set; }
          public int numberBooks { get; set; }
     }
}