﻿namespace BookMVC.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Category")]
    public partial class Category
    {
          [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          public long ID { get; set; }

        [StringLength(250)]
        [Display(Name = "Tên loại sách")]
        public string Name { get; set; }

        [StringLength(50)]
        public string MetaTitle { get; set; }

        public int? DisplayOrder { get; set; }

        [StringLength(2505)]
        public string SeoTitle { get; set; }

        public bool Status { get; set; }

        public long? ParentID { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        [StringLength(250)]
        public string MetaKeyword { get; set; }

        [StringLength(250)]
        public string MetaDescription { get; set; }
    }
}
