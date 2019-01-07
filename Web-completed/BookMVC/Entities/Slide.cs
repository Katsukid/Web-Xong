namespace BookMVC.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Slide")]
    public partial class Slide
    {
          [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          public int ID { get; set; }

        [StringLength(255)]
        [Display(Name = "ảnh")]
        public string Image { get; set; }
        [Display(Name = "Thứ tự hiển thị")]
        public int? DisplayOrder { get; set; }
        [Display(Name = "Tên sách")]
        public long? BookID { get; set; }

        [StringLength(255)]
        public string Link { get; set; }

        [StringLength(255)]
        [Display(Name = "Vị trí")]
        public string Where { get; set; }

        public bool Status { get; set; }

        [StringLength(50)]
        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        [StringLength(255)]
        public string MetaKeyword { get; set; }

        [StringLength(255)]
        public string MetaDescription { get; set; }
    }
}
