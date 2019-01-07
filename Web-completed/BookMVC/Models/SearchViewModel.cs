using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookMVC.Models
{
     public class SearchViewModel
     {
          public string KeyWord { get; set; }
          public int type { get; set; }
          public long id;
     }
}