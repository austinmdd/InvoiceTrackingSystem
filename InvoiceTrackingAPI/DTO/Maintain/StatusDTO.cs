using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceTrackingAPI.DTO.Maintain
{
    public class StatusDTO
    {
        public int StatusCode { get; set; }
        
        public string Status { get; set; }
       
        public string Description { get; set; }

        public bool SupplierInvoiceYN { get; set; }

        public bool SubmissionYN { get; set; }

        public bool PurchaseOrderYN { get; set; }

        public DateTime DateCreated { get; set; }
        
        public string UserCreated { get; set; }

        public int UserUpdated { get; set; }

        public DateTime DateUpdated { get; set; }
    }
}