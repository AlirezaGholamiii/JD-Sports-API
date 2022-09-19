using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JdRunner.Models.Customs
{
   
    public class Product
    {

        /* public int Ticket { get; set; }
         public string Status { get; set; }
         public string Requester { get; set; }
         public string Runner { get; set; }
         public string Description { get; set; }
         public string Color { get; set; }
         public string Size { get; set; }
         public string ReqTime { get; set; }*/

        public string Departmentdesc { get; set; }

        public string Classdesc { get; set; }

        public string Subclassdesc { get; set; }

        public string Styleno { get; set; }

        public string Styledesc { get; set; }

        public string Barcode { get; set; }
        public string Colordesc { get; set; }
        public string Sizedesc { get; set; }
        public string Dimensiondesc { get; set; }

        public string Earnloyalty { get; set; }

        public string Redeemloyalty { get; set; }

        public string Promotions { get; set; }

        public string Discounts { get; set; }

        public string Createdby { get; set; }
        public DateTime? Createddate { get; set; }

        public string Modifiedby { get; set; }
        public DateTime? Modifieddate { get; set; }

        public string Uniqueexcludegroup { get; set; }

        public string Uniquesku { get; set; }

        public string Uniquestyleno { get; set; }

        public string Uniquedepartment { get; set; }

        public string Uniqueclass { get; set; }

        public string Uniquesubclass { get; set; }

    }
}
