using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Entity.ViewModels
{
    public class BookViewModel
    {
        public int ID { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public string Publisher { get; set; }

        public string Category { get; set; }

        public int Quantity { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
