using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using InvoiceTrackingAPI.Models;
using InvoiceTrackingAPI.Services;
using InvoiceTrackingAPI.DTO.Workflow;

namespace InvoiceTrackingAPI.Controllers
{
    public class WorkflowRouteActionsController : ApiController
    {
        WorkflowRouteActionsService service = new WorkflowRouteActionsService();
        
        UtilsService<WorkflowRouteActionsDTO> Utils = new UtilsService<WorkflowRouteActionsDTO>();


        [Route("api/workflowrouteactions/{page:int}/{pageSize:int}")]
        public IHttpActionResult GetAllFixed(int page, int pageSize)
        {
            try
            {
                var actions = new WorkflowRouteActionsDTO(service.GetAllActions(page, pageSize));

                if (actions.Allactions.Count != 0)
                {
                    return Ok(actions.Allactions);
                }
                return ResponseMessage(new ResponseMessageService(HttpStatusCode.NotFound, "No actions status found.", Request.RequestUri.AbsoluteUri));
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
