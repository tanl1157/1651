namespace Base.Entity.MappedEntities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Audience")]
    public partial class Audience : MappedEntities
    {
        public int ID { get; set; }

        [Required]
        [StringLength(255)]
        public string IdentityCode { get; set; }

        [Required]
        [StringLength(255)]
        public string FullName { get; set; }

        [Required]
        [StringLength(255)]
        public string Address { get; set; }

        [Required]
        [StringLength(255)]
        public string PhoneNo { get; set; }
    }
}
