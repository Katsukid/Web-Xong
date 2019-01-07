namespace BookMVC.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
          [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          public long ID { get; set; }

          public DateTime? CreateDate { get; set; }

          public long? CreatID { get; set; }

          public long? Shiper { get; set; }

          public long? ShipTypeID { get; set; }
          [Required(ErrorMessage = "Tên người nhận không dược bỏ trống")]
          [Display(Name ="Họ và tên")]
          [StringLength(50)]
          public string ShipName { get; set; }
          [Required(ErrorMessage = "Số điện thoại người nhận không dược bỏ trống")]
          [Display(Name = "Số điện thoại người nhận")]
          [StringLength(50)]
          public string ShipMobile { get; set; }
          [Required(ErrorMessage = "Địa chỉ Email người nhận không dược bỏ trống")]
          [Display(Name = "Thư điện tử người nhận")]
          [StringLength(50)]
          public string ShipEmail { get; set; }
          [Required(ErrorMessage = "Địa chỉ nhận hàng không dược bỏ trống")]
          [Display(Name = "Địa chỉ nhận hàng")]
          [StringLength(255)]
          public string ShipAdress { get; set; }
          [Display(Name = "Mã giảm giá")]
          [StringLength(50)]
          public string CouponSerial { get; set; }
          [Display(Name = "Tình trạng vận chuyên")]
          public int? Status { get; set; }
          [Display(Name = "Ngày nhận hàng")]
          public DateTime? ShippedDate { get; set; }
          [Display(Name = "Tổng tiền")]
          public decimal? TotalPrice { get; set; }
    }
}
