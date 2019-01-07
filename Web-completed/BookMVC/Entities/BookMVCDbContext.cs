namespace BookMVC.Entities
{
     using System;
     using System.Data.Entity;
     using System.ComponentModel.DataAnnotations.Schema;
     using System.Linq;

     public partial class BookMVCDbContext : DbContext
     {
          public BookMVCDbContext()
              : base("name=BookMVCDbContext")
          {
          }

          public virtual DbSet<Admin> Admins { get; set; }
          public virtual DbSet<Author> Authors { get; set; }
          public virtual DbSet<Book> Books { get; set; }
          public virtual DbSet<BookCategory> BookCategories { get; set; }
          public virtual DbSet<CartItem> CartItems { get; set; }
          public virtual DbSet<Category> Categories { get; set; }
          public virtual DbSet<Contact> Contacts { get; set; }
          public virtual DbSet<Content> Contents { get; set; }
          public virtual DbSet<ContentTag> ContentTags { get; set; }
          public virtual DbSet<Coupon> Coupons { get; set; }
          public virtual DbSet<Credential> Credentials { get; set; }
          public virtual DbSet<Feedback> Feedbacks { get; set; }
          public virtual DbSet<Footer> Footers { get; set; }
          public virtual DbSet<Menu> Menus { get; set; }
          public virtual DbSet<MenuType> MenuTypes { get; set; }
          public virtual DbSet<News> News { get; set; }
          public virtual DbSet<NewsType> NewsTypes { get; set; }
          public virtual DbSet<Order> Orders { get; set; }
          public virtual DbSet<Order_Detail> Order_Detail { get; set; }
          public virtual DbSet<Publisher> Publishers { get; set; }
          public virtual DbSet<Role> Roles { get; set; }
          public virtual DbSet<ShippingType> ShippingTypes { get; set; }
          public virtual DbSet<Slide> Slides { get; set; }
          public virtual DbSet<Tag> Tags { get; set; }
          public virtual DbSet<User> Users { get; set; }
          public virtual DbSet<UserGroup> UserGroups { get; set; }

          protected override void OnModelCreating(DbModelBuilder modelBuilder)
          {
               modelBuilder.Entity<Admin>()
                   .Property(e => e.UserName)
                   .IsFixedLength();

               modelBuilder.Entity<Author>()
                   .Property(e => e.Image)
                   .IsUnicode(false);

               modelBuilder.Entity<Book>()
                   .Property(e => e.Code)
                   .IsUnicode(false);

               modelBuilder.Entity<Book>()
                   .Property(e => e.Price)
                   .HasPrecision(18, 0);

               modelBuilder.Entity<Book>()
                   .Property(e => e.PromotionPrice)
                   .HasPrecision(18, 0);

               modelBuilder.Entity<Book>()
                   .Property(e => e.Inventory)
                   .HasPrecision(18, 0);

               modelBuilder.Entity<Book>()
                   .Property(e => e.MetaTitle)
                   .IsUnicode(false);

               modelBuilder.Entity<Book>()
                   .Property(e => e.ModifiedBy)
                   .IsUnicode(false);

               modelBuilder.Entity<Book>()
                   .Property(e => e.MetaDescription)
                   .IsFixedLength();

               modelBuilder.Entity<BookCategory>()
                   .Property(e => e.MetaTitle)
                   .IsFixedLength();

               modelBuilder.Entity<BookCategory>()
                   .Property(e => e.CreateBy)
                   .IsUnicode(false);

               modelBuilder.Entity<BookCategory>()
                   .Property(e => e.ModifiedBy)
                   .IsUnicode(false);

               modelBuilder.Entity<BookCategory>()
                   .Property(e => e.MetaDescription)
                   .IsFixedLength();

               modelBuilder.Entity<Category>()
                   .Property(e => e.MetaTitle)
                   .IsFixedLength();

               modelBuilder.Entity<Category>()
                   .Property(e => e.SeoTitle)
                   .IsFixedLength();

               modelBuilder.Entity<Category>()
                   .Property(e => e.CreatedBy)
                   .IsUnicode(false);

               modelBuilder.Entity<Category>()
                   .Property(e => e.ModifiedBy)
                   .IsUnicode(false);

               modelBuilder.Entity<Category>()
                   .Property(e => e.MetaDescription)
                   .IsFixedLength();

               modelBuilder.Entity<Content>()
                   .Property(e => e.MetaTitle)
                   .IsUnicode(false);

               modelBuilder.Entity<Content>()
                   .Property(e => e.Description)
                   .IsFixedLength();

               modelBuilder.Entity<Content>()
                   .Property(e => e.CreatedBy)
                   .IsUnicode(false);

               modelBuilder.Entity<Content>()
                   .Property(e => e.ModifiedBy)
                   .IsUnicode(false);

               modelBuilder.Entity<Content>()
                   .Property(e => e.MetaDescription)
                   .IsFixedLength();

               modelBuilder.Entity<ContentTag>()
                   .Property(e => e.TagID)
                   .IsUnicode(false);

               modelBuilder.Entity<Coupon>()
                   .Property(e => e.Serial)
                   .IsUnicode(false);

               modelBuilder.Entity<Coupon>()
                   .Property(e => e.Discount)
                   .HasPrecision(18, 0);

               modelBuilder.Entity<Credential>()
                   .Property(e => e.UserGroupID)
                   .IsUnicode(false);

               modelBuilder.Entity<Credential>()
                   .Property(e => e.RoleID)
                   .IsUnicode(false);

               modelBuilder.Entity<News>()
                   .Property(e => e.isHomePage)
                   .IsFixedLength();

               modelBuilder.Entity<News>()
                   .Property(e => e.Author)
                   .IsFixedLength();

               modelBuilder.Entity<NewsType>()
                   .Property(e => e.Order)
                   .HasPrecision(18, 0);

               modelBuilder.Entity<Order>()
                   .Property(e => e.ShipMobile)
                   .IsUnicode(false);

               modelBuilder.Entity<Order>()
                   .Property(e => e.CouponSerial)
                   .IsUnicode(false);

               modelBuilder.Entity<Order>()
                   .Property(e => e.TotalPrice)
                   .HasPrecision(18, 0);

               modelBuilder.Entity<Order_Detail>()
                   .Property(e => e.Price)
                   .HasPrecision(18, 0);

               modelBuilder.Entity<Publisher>()
                   .Property(e => e.MetaTitle)
                   .IsUnicode(false);

               modelBuilder.Entity<Publisher>()
                   .Property(e => e.Description)
                   .IsFixedLength();

               modelBuilder.Entity<Publisher>()
                   .Property(e => e.CreatedBy)
                   .IsUnicode(false);

               modelBuilder.Entity<Publisher>()
                   .Property(e => e.ModifiedBy)
                   .IsUnicode(false);

               modelBuilder.Entity<Publisher>()
                   .Property(e => e.MetaDescription)
                   .IsFixedLength();

               modelBuilder.Entity<Role>()
                   .Property(e => e.ID)
                   .IsUnicode(false);

               modelBuilder.Entity<ShippingType>()
                   .Property(e => e.Cost)
                   .HasPrecision(18, 0);

               modelBuilder.Entity<Slide>()
                   .Property(e => e.Where)
                   .IsUnicode(false);

               modelBuilder.Entity<Slide>()
                   .Property(e => e.CreatedBy)
                   .IsUnicode(false);

               modelBuilder.Entity<Slide>()
                   .Property(e => e.ModifiedBy)
                   .IsUnicode(false);

               modelBuilder.Entity<Slide>()
                   .Property(e => e.MetaDescription)
                   .IsFixedLength();

               modelBuilder.Entity<Tag>()
                   .Property(e => e.ID)
                   .IsUnicode(false);

               modelBuilder.Entity<User>()
                   .Property(e => e.UserName)
                   .IsUnicode(false);

               modelBuilder.Entity<User>()
                   .Property(e => e.Email)
                   .IsUnicode(false);

               modelBuilder.Entity<User>()
                   .Property(e => e.Password)
                   .IsUnicode(false);

               modelBuilder.Entity<User>()
                   .Property(e => e.Phone)
                   .IsUnicode(false);

               modelBuilder.Entity<User>()
                   .Property(e => e.GroupID)
                   .IsUnicode(false);

               modelBuilder.Entity<UserGroup>()
                   .Property(e => e.ID)
                   .IsUnicode(false);
          }
     }
}
