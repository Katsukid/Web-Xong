using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace BookMVC.Models
{
     public class AccountModelView
     {
          [Required]
          public string Email { get; set; }

          [Required]
          public string Password { get; set; }

          [DefaultValue(true)]
          [Display (Name ="Nhớ tài khoản?")]
          public bool RememberMe { get; set; }
     }
}