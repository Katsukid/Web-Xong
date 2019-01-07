namespace BookMVC.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Contact")]
    public partial class Contact
    {
          [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          public int ID { get; set; }

        [Column(TypeName = "ntext")]
        public string content { get; set; }

        public int? Status { get; set; }
    }
}
