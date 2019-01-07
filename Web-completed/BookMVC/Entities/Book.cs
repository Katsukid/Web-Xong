namespace BookMVC.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
     using System.Web.Mvc;

     [Table("Book")]
    public partial class Book
    {
          [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          public long ID { get; set; }


          [StringLength(50)]
        [Display(Name = "Mã sách")]
        [Required(ErrorMessage = "Bạn phải nhập mã  sách")]
          public string Code { get; set; }

          [Required(ErrorMessage = "Bạn phải nhập tên sách")]
          [StringLength(250)]
        [Display(Name = "Tên sách")]
        public string Name { get; set; }

        //[Required(ErrorMessage = "Bạn phải nhập tên tác giả")]
        [Display(Name = "Tên tác giả")]
        public int? Author { get; set; }

        //[Required(ErrorMessage = "Bạn phải nhập tên nhà phát hành")]
        [Display(Name = "Tên nhà phát hành")]
        public int? Released { get; set; }

          [Required(ErrorMessage = "Bạn phải nhập tên NXB")]
        [Display(Name = "Tên nhà xuất bản")]
        public int? Publisher { get; set; }

          [Required(ErrorMessage = "Bạn phải nhập số trang")]
        [Display(Name = "Số trang sách")]
        public int? NumberPage { get; set; }

          [Required(ErrorMessage = "Bạn phải nhập trọng lượng")]
        [Display(Name = "Trọng lượng sách")]
        public int? Weight { get; set; }


          [StringLength(20)]
        [Display(Name = "Form sách")]
        public string Form { get; set; }

          [Required(ErrorMessage = "Bạn phải nhập ngày xuất bản")]
          [Column(TypeName = "date")]
        [Display(Name = "Ngày xuất bản")]
        public DateTime? PublishDate { get; set; }


          public int? Buys { get; set; }

          [Required(ErrorMessage = "Bạn phải nhập giá sách")]
        [Display(Name = "Gía sách")]
        public decimal? Price { get; set; }

          [Required(ErrorMessage = "Bạn phải nhập giá gốc")]
        [Display(Name = "Gía gốc")]
        public decimal? PromotionPrice { get; set; }

        //[Required(ErrorMessage = "Bạn phải nhập loại sách")]
        [Display(Name = "Loại sách")]
        public long? CategoryID { get; set; }

          public int? ViewCount { get; set; }

          public int? LikeCount { get; set; }
        [Display(Name = "Số lượng")]
        public decimal? Inventory { get; set; }

          [StringLength(250)]
          public string MetaTitle { get; set; }

          public bool? Status { get; set; }


          [StringLength(250)]
        [Display(Name = "ảnh")]
        public string Image { get; set; }


          [AllowHtml]
        [Display(Name = "Mô tả")]
        public string Description { get; set; }

          [Column(TypeName = "xml")]
          public string Modelmage { get; set; }

          public DateTime? CreateDate { get; set; }

          [StringLength(50)]
          public string CreateBy { get; set; }

          public DateTime? ModifiedDate { get; set; }

          [StringLength(50)]
          public string ModifiedBy { get; set; }

          [StringLength(250)]
          public string MetaKeyword { get; set; }

          [StringLength(250)]
          public string MetaDescription { get; set; }
     }
}
