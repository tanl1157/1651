namespace Base.Entity.MappedEntities
{
    using Base.Entity.Shared;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Globalization;

    [Table("Order")]
    public partial class Order : MappedEntities
    {
        public int ID { get; set; }

        [StringLength(255)]
        public string Code { get; set; }
        public int AudienceID { get; set; }

        public int BookID { get; set; }

        public int Quantity { get; set; }

        public DateTime StartDate { get; set; }
        [NotMapped]
        public string StrStartDate { get; set; }

        public DateTime EndDate { get; set; }
        [NotMapped]
        public string StrEndDate { get; set; }

        public DateTime? ActualEndDate { get; set; }
        [NotMapped]
        public string StrActualEndDate { get; set; }

        public OrderState State { get; set; }

        public void RebindDate()
        {
            if(!String.IsNullOrEmpty(StrStartDate))
            {
                StartDate = DateTime.ParseExact(StrStartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }

            if (!String.IsNullOrEmpty(StrEndDate))
            {
                EndDate = DateTime.ParseExact(StrEndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }

            if (!String.IsNullOrEmpty(StrActualEndDate))
            {
                ActualEndDate = DateTime.ParseExact(StrActualEndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
        }
    }
}
