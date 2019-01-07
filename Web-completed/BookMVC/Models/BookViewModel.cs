using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookMVC.Entities;
namespace BookMVC.Models
{
     public class BookViewModel
     {
          public long ID { get; set; }

          [StringLength(50)]
          public string Code { get; set; }

          [StringLength(250)]
          public string Name { get; set; }
          
          public string Author { get; set; }
          public int? AuthorID { get; set; }

          public string Released { get; set; }
          public int? ReleasedID { get; set; }

          public string Publisher { get; set; }
          public int? PublisherID { get; set; }

          public int? NumberPage { get; set; }

          public int? Weight { get; set; }

          [StringLength(20)]
          public string Form { get; set; }

          [Column(TypeName = "date")]
          public DateTime? PublishDate { get; set; }

          public int? Buys { get; set; }

          public decimal? Price { get; set; }

          public decimal? PromotionPrice { get; set; }

          public long? CategoryID { get; set; }

          public int? ViewCount { get; set; }

          public int? LikeCount { get; set; }

          public decimal? Inventory { get; set; }

          public bool? Status { get; set; }

          [StringLength(250)]
          public string Image { get; set; }

          public string Description { get; set; }
     }
}