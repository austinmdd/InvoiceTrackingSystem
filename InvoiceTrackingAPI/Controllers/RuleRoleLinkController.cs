using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using InvoiceTrackingAPI.DTO.Workflow;
using InvoiceTrackingAPI.Services;
using InvoiceTrackingAPI.Models;

namespace InvoiceTrackingAPI.Controllers
{
    public class RuleRoleLinkController : ApiController
    {
        RuleRoleLinkService Service = new RuleRoleLinkService();
        UtilsService<RuleRoleLinkDTO> Utils = new UtilsService<RuleRoleLinkDTO>();

       

        public IHttpActionResult GetbyRuleID(int id)
        {
            try
            {
                return Ok((Service.GetbyRuleId(id) != null) ? new { Deletable = false } : new { Deletable = true });
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
