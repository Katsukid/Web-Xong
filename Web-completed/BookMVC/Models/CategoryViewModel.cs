using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookMVC.Entities;
namespace BookMVC.Models
{
     public class CategoryViewModel
     {
          public Category Category { get; set; }
          public List<BookCateVIewModel> lsBookCategory { get; set; }
          public int numberBooks { get; set; }
     }
}