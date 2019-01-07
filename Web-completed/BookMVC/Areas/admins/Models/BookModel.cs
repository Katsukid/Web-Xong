using BookMVC.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMVC.Areas.admins.Models
{
   public class BookModel
    {
        public Book Book { get; set; }
        public string NameAuthor { get; set; }
       
        public string NameReleased { get; set; }
        public string NamePublisher { get; set; }
        public string NameType { get; set; }
    }
}
