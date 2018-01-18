using InvoiceTrackingAPI.Models;
using InvoiceTrackingAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceTrackingAPI.DTO.Search
{
    public class SupplierInvoiceDTO
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
        public SubmissionDTO Submission { get; set; }
        public SupplierDTO Supplier { get; set; }
        public PODTO PurchaseOrder { get; set; }

        public SupplierInvoiceDTO(ITS_SupplierInvoice supplierInvoice, bool AddExtra)
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
            if (AddExtra)
            {
                Supplier = new SupplierDTO(supplierInvoice.ITS_Submission.ITS_Supplier, false);
                Submission = MapService.MapOne<ITS_Submission, SubmissionDTO>(supplierInvoice.ITS_Submission);
                PurchaseOrder = new PODTO(supplierInvoice.ITS_Submission.ITS_PurchaseOrder);
            }                       
        }

        public SupplierInvoiceDTO(ITS_SupplierInvoice supplierInvoice)
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