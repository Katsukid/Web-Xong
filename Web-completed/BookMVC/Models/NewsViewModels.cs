using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace BookMVC.Models
{
     public class NewsViewModels
     {
          [Key]
          public long ID { get; set; }

          [Required]
          [StringLength(250)]
          public string Title { get; set; }

          public long Created { get; set; }

          public DateTime CreatedDate { get; set; }

          public bool isPublic { get; set; }

          [Required]
          public string Content { get; set; }

          [Required]
          [StringLength(10)]
          public string Author { get; set; }

          [Required]
          [StringLength(10)]
          public string isHomePage { get; set; }

          public string NameType { get; set; }
     }
}