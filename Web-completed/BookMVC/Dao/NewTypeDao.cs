using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMVC.Entities;

namespace BookMVC.Dao
{
    public class NewTypeDao
    {
        BookMVCDbContext db = null;
        public NewTypeDao()
        {
            db = new BookMVCDbContext();
        }
        public List<NewsType> ListAll()
        {
            return db.NewsTypes.Where(x => x.Status == true).ToList();
        }
        
    }
}
