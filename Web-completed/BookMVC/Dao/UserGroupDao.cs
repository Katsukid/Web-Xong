using BookMVC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookMVC.Dao
{
    public class UserGroupDao
    {

        BookMVCDbContext db;
        public UserGroupDao()
        {
            db = new BookMVCDbContext();
        }
        public List<UserGroup> ListAll()
        {
            return db.UserGroups.ToList();
        }
    }
}