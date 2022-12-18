using Base.Entity.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Entity.ViewModels
{
    public class OrderViewModel
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public int AudienceID { get; set; }
        public int BookID { get; set; }
        public int Quantity { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime? ActualEndDate { get; set; }

        public OrderState State { get; set; }
        public string StateTitle { 
            get {
                string value = String.Empty;
                switch (State)
                {
                    case OrderState.Renting:
                        value = "Đang mượn";
                        break;
                    case OrderState.Returned:
                        value = "Đã trả";
                        break;
                    default:
                        break;
                }
                return value;
            }
        }

        public string AudienceIdentityCode { get; set; }
        public string AudienceName { get; set; }
        public string BookCode { get; set; }
        public string BookName { get; set; }

    }
}
