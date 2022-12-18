namespace Base.Entity.MappedEntities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Book")]
    public partial class Book : MappedEntities
    {
        public int ID { get; set; }

        [Required]
        [StringLength(255)]
        public string Code { get; set; }

        [Required]
        [StringLength(511)]
        public string Name { get; set; }

        [StringLength(511)]
        public string Author { get; set; }

        [StringLength(511)]
        public string Publisher { get; set; }

        [StringLength(511)]
        public string Category { get; set; }

        public int Quantity { get; set; }

        public bool IsActive { get; set; }
    }
}
