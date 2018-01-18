using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using InvoiceTrackingAPI.Models;
using InvoiceTrackingAPI.Services;

namespace InvoiceTrackingAPI.Controllers
{
    public class SystemSettingsController : ApiController
    {

        SystemSettingsService Service = new SystemSettingsService();
        UtilsService<ITS_SystemSettings> Utils = new UtilsService<ITS_SystemSettings>();


        public IHttpActionResult Get()
        {
            try
            {
                var doctypes = Service.GetAll().ToList();
                if (doctypes.Count != 0)
                {
                    return Ok(doctypes);
                }
                return ResponseMessage(new ResponseMessageService(HttpStatusCode.NotFound, "No document types found.", Request.RequestUri.AbsoluteUri));
            }
            catch (Exception e)
            {
                return ExceptionMSG(e);
            }
        }

        private IHttpActionResult ExceptionMSG(Exception e)
        {
            return Utils.Error(e.Message, Request.RequestUri.AbsoluteUri);
        }

    }
}
