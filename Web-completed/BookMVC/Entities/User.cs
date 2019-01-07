namespace BookMVC.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
          [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          public long ID { get; set; }

          [Required(ErrorMessage = "Hãy nhập tên đăng nhập!")]
          [StringLength(50)]
          public string UserName { get; set; }

          [Required(ErrorMessage = "Hãy nhập địa chỉ email!")]
          [StringLength(100)]
          [Display(Name = "Thư điện tử")]
          public string Email { get; set; }

          [Required(ErrorMessage = "Hãy nhập mật khẩu đăng nhập!")]
          [StringLength(50)]
          public string Password { get; set; }

          [Required(ErrorMessage = "Xin vui lòng cho biết tên của bạn!")]
          [StringLength(50)]
          [Display(Name = "Họ và tên")]
          public string Name { get; set; }

          [Required(ErrorMessage = "Xin vui lòng cho biết sinh nhật của bạn")]
          [Column(TypeName = "date")]
          [Display(Name = "Ngày sinh")]
          public DateTime? DayOfBirth { get; set; }

          [Required(ErrorMessage = "Xin vui lòng cho biết số điện thoại")]
          [StringLength(50)]
          [Display(Name ="Số điện thoại")]
          public string Phone { get; set; }

          [Required(ErrorMessage = "Xin vui lòng cho biết địa chỉ")]
          [StringLength(255)]
          [Display(Name ="Địa chỉ")]
          public string Address { get; set; }

          public bool Status { get; set; }

          [StringLength(20)]
          public string GroupID { get; set; }

          public int? BookCount { get; set; }

          public int? OrderCount { get; set; }

          public int? OrderCancel { get; set; }

          public DateTime? CreatedDate { get; set; }

          public long? CreatedBy { get; set; }

          public DateTime? ModifiedDate { get; set; }

          public long? ModifiedBy { get; set; }

          [StringLength(250)]
          public string MetaKeyword { get; set; }

          [StringLength(250)]
          public string MetaDescription { get; set; }
    }
}
