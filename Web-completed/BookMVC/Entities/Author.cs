namespace BookMVC.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Author")]
    public partial class Author
    {
          [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          public int ID { get; set; }

        [StringLength(255)]
        [Display(Name = "Tên tác giả")]
        public string Name { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateOfBirth { get; set; }
        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [StringLength(50)]
        public string Type { get; set; }

        [StringLength(255)]
        public string Image { get; set; }
    }
}
