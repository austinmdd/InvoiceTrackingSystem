using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceTrackingAPI.DTO
{
    public class PODocumentTypeDTO
    {

        public double DocTypeID { get; set; }
        public string DocType { get; set; }
        public string Mandatory { get; set; }

        public byte[] UploadedDocument { get; set; }

        
    }
}