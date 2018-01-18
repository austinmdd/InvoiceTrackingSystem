using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using InvoiceTrackingAPI.DTO;
using InvoiceTrackingAPI.Services;

namespace InvoiceTrackingAPI.Controllers
{
    public class EmailController : ApiController
    {
        EmailService mail = new EmailService();
        UtilsService<EmailDTO> util = new UtilsService<EmailDTO>();

        public IHttpActionResult SendMail([FromBody]EmailDTO maildata)
        {
            try
            {
                mail.SendMail(maildata.Address, maildata.Message);
                return Ok("Message sent");
            }
            catch (Exception e)
            {
                return ExceptionMSG(e);
            }
        }


        private IHttpActionResult ExceptionMSG(Exception e)
        {
            return util.Error(e.Message, Request.RequestUri.AbsoluteUri);
        }



    }
}
