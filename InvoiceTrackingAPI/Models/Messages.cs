using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceTrackingAPI.Models
{
    public class Messages
    {
        public Messages(string status, string message, string uRI, string exceptionMSG = null)
        {
            Status = status;
            Message = message;
            Uri = uRI;
            ExceptionMSG = exceptionMSG;
        }

        public string Status { get; set; }
        public string Message { get; set; }
        public string Uri { get; set; }
        public string ExceptionMSG { get; set; }

    }
}