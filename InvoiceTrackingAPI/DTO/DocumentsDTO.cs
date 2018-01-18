using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceTrackingAPI.DTO
{
    public class DocumentsDTO
    {
        public int ID { get; set; }
       
        public int DocTypeID { get; set; }
        public string DocTypeName { get; set; }
        public string DocTypeMandatory{ get; set; }
        public string DocName { get; set; }
        public string DocType { get; set; }
        public string DocSize { get; set; }

        public string ObjectID { get; set; }
        public string Link { get; set; }
    }
}