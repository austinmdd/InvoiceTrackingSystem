using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceTrackingAPI.DTO.Search
{
    public class SubmissionDTO
    {

        public int ID { get; set; }

        public string SubmissionNumber { get; set; }

        public int PurchaseOrderID { get; set; }

        public int SupplierID { get; set; }

       
        public string Status { get; set; }

        
        public string PONumber { get; set; }

       
        public decimal POAmount { get; set; }

        public DateTime DateCreated { get; set; }

       
        public string UserCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        public int UserUpdated { get; set; }

        public SubmissionDTO()
        {

        }
    }
}