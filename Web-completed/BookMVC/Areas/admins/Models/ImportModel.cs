using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMVC.Areas.admins.Models
{
   public class ImportModel
    {
        public long ID { get; set; }

        [StringLength(250)]
        public string Name { get; set; }
        
        public long quantity { get; set; }
    }
}
