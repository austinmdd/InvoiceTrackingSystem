using InvoiceTrackingAPI.DTO.Maintain;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceTrackingAPI.DTO
{
    public class SupplierInvoiceSubmissionDTO
    {
        //SupplierInvoice
        
        public string InvoiceID { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal InvoiceAmount { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string Description { get; set; }

        //PurchaseOrder
        public int POID { get; set; }
        public string POStatus { get; set; }
        public string PONumber { get; set; }
        public decimal POAmount { get; set; }
        public string PODate { get; set; }
        public decimal DueAmount { get; set; }

        //Supplier
        public string Name { get; set; }
        public string CSDNumber { get; set; }
        public string VendorCode { get; set; }


       public string FileUploadType { get; set; }
        public byte[] FileUpload { get; set; }

        public List<InvoiceCategoriesDTO> InvoiceCat { get; set; }
        public IEnumerable<PODocumentTypeDTO> LinkedDocs { get; set; }
        public IEnumerable<DocumentsDTO> UploadedDocs { get; set; }

    }
}