using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Entity.ViewModels
{
    public class AudienceViewModel
    {
        public int ID { get; set; }

        public string IdentityCode { get; set; }

        public string FullName { get; set; }

        public string Address { get; set; }

        public string PhoneNo { get; set; }
    }
}
