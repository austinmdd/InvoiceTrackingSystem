using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceTrackingAPI.Handlers
{
    public class ExceptionMSG
    {

        public ExceptionMSG(string status, string message, string uRI)
        {
           
            Message = message;
            Status = status;
            Uri = uRI;           
        }

        public string Status { get; set; }
        public string Message { get; set; }
        public string Uri { get; set; }       
    }
}