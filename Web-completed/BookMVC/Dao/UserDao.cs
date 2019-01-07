using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookMVC.Entities;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using PagedList;
using BookMVC.Common;

namespace BookMVC.Dao
{
    public class UserDao
    {
        BookMVCDbContext db;
        public UserDao()
        {
            db = new BookMVCDbContext();
        }
        public bool _Login(string EmailOrUserName, string Password)
        {
            var rs = db.Users.Count(x => x.Email == EmailOrUserName && x.Password == Password);
            if (rs > 0)
                return true;
            rs = db.Users.Count(x => x.UserName == EmailOrUserName && x.Password == Password);
            if (rs > 0)
                return true;
            return false;
        }

        public User _TakeUser(string EmailOrUserName, string Password)
        {
            User user = db.Users.Where(x => x.Email == EmailOrUserName && x.Password == Password).SingleOrDefault();
            if (user != null)
                return user;
            user = db.Users.Where(x => x.UserName == EmailOrUserName && x.Password == Password).SingleOrDefault();
            return user;
        }

        // Lay tai khoan bang ID
        public User GetUser(long ID) => db.Users.Where(x => x.ID == ID).SingleOrDefault();

        // Them nguoi dung moi
        public bool AddUser(User us)
        {
            try
            {
                //.ToString("yyyy-MM-dd HH:mm:ss.fff")
                us.CreatedDate = DateTime.Now;
                us.BookCount = 0;
                us.OrderCancel = 0;
                us.OrderCount = 0;
                us.GroupID = "MEMEBER";
                us.Status = true;
                db.Users.Add(us);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        // Kiem tra email va UserName trung lap
        public bool ExistedEmail(string email) => db.Users.Count(x => x.Email == email) > 0 ? true : false;
        public bool ExistedUserName(string UserName) => db.Users.Count(x => x.UserName == UserName) > 0 ? true : false;

        //public bool ValidEmail(string email)
        //{
        //     using (WebClient webclient = new WebClient())
        //     {
        //          string url = "http://verify-email.org";
        //          var formdata = new NameValueCollection(){
        //                { "email", email }
        //          };
        //          byte[] responsebyte = webclient.UploadValues(url, "POST", formdata);
        //          string reponse = Encoding.ASCII.GetString(responsebyte);
        //          if (reponse.Contains("Result: Ok"))
        //               return true;
        //          return false;
        //     }
        //}
        //public bool ChangeStatus(long id)
        //{
        //     var user = db.Users.Find(id);
        //     user.Status = !user.Status;
        //     db.SaveChanges();
        //     return (bool)user.Status;
        //}
        public bool ChangePassword(long id, string newpassword)
        {
            try
            {
                var user = GetUser(id);
                user.Password = newpassword;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool ChangeProfile(long id, User user)
        {
            try
            {
                var us = GetUser(id);
                us.Name = user.Name;
                us.Email = user.Email;
                us.UserName = user.UserName;
                us.Address = user.Address;
                us.DayOfBirth = user.DayOfBirth;
                us.Phone = user.Phone;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /* Admin
         * 
         * 
         * 
         * 
         * 
         */
        public bool Insert(User entity)
        {
            try
            {
                var user = new User();
                user.Name = entity.Name;
                user.GroupID = entity.GroupID;
                user.Address = entity.Address;
                user.UserName = entity.UserName;
                user.DayOfBirth = entity.DayOfBirth;
                user.Email = entity.Email;
                user.Phone = entity.Phone;
                user.Password = entity.Password;
                user.CreatedDate = DateTime.Now;
                user.Status = true;
                user.GroupID = entity.GroupID;
                db.Users.Add(user);
                db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public IEnumerable<User> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<User> model = db.Users;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.UserName.Contains(searchString));

            }
            return model.OrderByDescending(x => x.Name).ToPagedList(page, pageSize);

        }
        public bool Update(User entity)
        {
            try
            {
                var user = db.Users.Find(entity.ID);
                user.Name = entity.Name;
                user.GroupID = entity.GroupID;
                user.Address = entity.Address;
                user.UserName = entity.UserName;
                user.DayOfBirth = entity.DayOfBirth;
                user.Email = entity.Email;
                user.Phone = entity.Phone;
                user.Password = entity.Password;
                user.GroupID = entity.GroupID;
                user.ModifiedBy = entity.ModifiedBy;
                user.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }


        }

        public User GetById(string userName)
        {
            return db.Users.SingleOrDefault(x => x.UserName == userName);
        }
        public User ViewDetail(int id)
        {
            return db.Users.Find(id);
        }

        public int Login(string userName, string passWord, bool isLoginAdmin = false)
        {
            var result = db.Users.SingleOrDefault(x => x.UserName == userName);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (isLoginAdmin == true)
                {
                    if (result.GroupID == CommonConstants.ADMIN_GROUP || result.GroupID == CommonConstants.MOD_GROUP || result.GroupID == CommonConstants.SHIP_GROUP)
                    {
                        if (result.Status == false)
                        {
                            return -1;
                        }
                        else
                        {
                            if (result.Password == passWord)
                                return 1;
                            else
                                return -2;
                        }
                    }
                    else
                    {
                        return -3;
                    }
                }
                else
                {
                    if (result.Status == false)
                    {
                        return -1;
                    }
                    else
                    {
                        if (result.Password == passWord)
                            return 1;
                        else
                            return -2;
                    }
                }
            }
        }

        public List<string> GetListCredential(string userName)
        {
            var user = db.Users.Single(x => x.UserName == userName);
            var data = (from a in db.Credentials
                        join b in db.UserGroups on a.UserGroupID equals b.ID
                        join c in db.Roles on a.RoleID equals c.ID
                        where b.ID == user.GroupID
                        select new
                        {
                            RoleID = a.RoleID,
                            UserGroupID = a.UserGroupID
                        }).AsEnumerable().Select(x => new Credential()
                        {
                            RoleID = x.RoleID,
                            UserGroupID = x.UserGroupID
                        });
            return data.Select(x => x.RoleID).ToList();

        }
        public int Delete(long id, long us)
        {
            try
            {
                var user = db.Users.Find(id);
                if (id != us)
                {
                    db.Users.Remove(user);
                    db.SaveChanges();
                    return 1;
                }
                else
                {
                    return 0;
                }


            }
            catch (Exception)
            {
                return -1;
            }


        }

        public bool ChangeStatus(long id)
        {
            var user = db.Users.Find(id);
            user.Status = !user.Status;
            db.SaveChanges();
            return (bool)user.Status;
        }
        public User TakeUser(string EmailOrUserName, string password)
        {
            var user = db.Users.Where(x => x.UserName == EmailOrUserName && x.Password == password).SingleOrDefault();
            return user;
        }

        //public bool Login(string UserName, string Password)
        //{
        //     var rs = db.Users.Count(x => x.UserName == UserName && x.Password == Password);
        //     if (rs > 0) return true;

        //     return false;
        //}
        public List<User> ListAll()
        {
            return db.Users.ToList();
        }
        public List<string> Listsearch(string search)
        {

            var ls = (from x in db.Users
                      where x.UserName.Contains(search)
                      select x.UserName).ToList();
            var lsUserName = (from x in db.Users
                              where x.Name.Contains(search)
                              select x.Name).AsEnumerable();
            ls.AddRange(lsUserName);
            return ls;
        }
    }
}