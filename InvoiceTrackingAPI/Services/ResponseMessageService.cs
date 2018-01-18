using InvoiceTrackingAPI.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Web;

namespace InvoiceTrackingAPI.Services
{
    public class ResponseMessageService: HttpResponseMessage
    {
        public ResponseMessageService(HttpStatusCode status,string message, string uri, string exceptionMSG = "None")
        {
            int code = Convert.ToInt32(status);
            Messages messages = new Messages(code.ToString(),message,uri, exceptionMSG);
            this.StatusCode = status;           
            this.Content = new ObjectContent<Messages>(messages, new JsonMediaTypeFormatter());
        }
        
        public static HttpResponseMessage ErrorMessage(string uri, string exception) {
            return new ResponseMessageService(HttpStatusCode.InternalServerError,"The request could not be processed. Contact the system administrator if the problem persists.", uri, exception);
        }
        
    }
}