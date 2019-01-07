namespace BookMVC.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ShippingType")]
    public partial class ShippingType
    {
          [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          public long ID { get; set; }

        [Required]
        [StringLength(100)]
        public string TypeShip { get; set; }

        public decimal Cost { get; set; }

        [StringLength(50)]
        public string Time { get; set; }
    }
}
