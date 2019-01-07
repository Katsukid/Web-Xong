namespace BookMVC.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NewsType")]
    public partial class NewsType
    {
          [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          public long ID { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        public decimal? Order { get; set; }

        public bool? Status { get; set; }
    }
}
