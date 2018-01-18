using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InvoiceTrackingAPI.Models;

namespace InvoiceTrackingAPI.DTO
{
    public class UpdateSupplierInvoiceDTO
    {
        public int ID { get; set; }

        public int SubmissionID { get; set; }
        public string Status { get; set; }


        public string InvoiceNumber { get; set; }


        public decimal InvoiceAmount { get; set; }

  
        public DateTime InvoiceDate { get; set; }

        public string Description { get; set; }

        public DateTime DateCreated { get; set; }

        public string UserCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        public int UserUpdated { get; set; }

        public UpdateSupplierInvoiceDTO(ITS_SupplierInvoice supplierInvoice)
        {
            ID = supplierInvoice.ID;
            SubmissionID = supplierInvoice.SubmissionID;
            Status = supplierInvoice.Status;
            InvoiceNumber = supplierInvoice.InvoiceNumber;
            InvoiceAmount = supplierInvoice.InvoiceAmount;
            InvoiceDate = supplierInvoice.InvoiceDate;
            Description = supplierInvoice.Description;
            DateCreated = supplierInvoice.DateCreated;
            UserCreated = supplierInvoice.UserCreated;
            DateUpdated = supplierInvoice.DateUpdated;
            UserUpdated = supplierInvoice.UserUpdated;
        }


    }
}