namespace BookMVC.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class News
    {
          [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          public long ID { get; set; }

        [StringLength(250)]
        [Display(Name = "Tiêu đề")]
        public string Title { get; set; }

        public long? Created { get; set; }

        public DateTime? CreatedDate { get; set; }
        [Display(Name = "Loại tin")]
        public long? TypeID { get; set; }

        public bool? isPublic { get; set; }

        [StringLength(10)]
        public string isHomePage { get; set; }

        [StringLength(250)]
        [Display(Name = "ảnh")]
        public string Image { get; set; }

        [StringLength(100)]
        [Display(Name = "Tác giả")]
        public string Author { get; set; }
        [Display(Name = "Nội dung")]
        public string Content { get; set; }
    }
}
