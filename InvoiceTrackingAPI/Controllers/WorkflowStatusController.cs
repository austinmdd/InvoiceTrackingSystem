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
    public class WorkflowStatusController : ApiController
    {
        WorflowStatusService Service = new WorflowStatusService();
       
        UtilsService<WorkflowStatusDTO> Utils = new UtilsService<WorkflowStatusDTO>();
       

        public IHttpActionResult Get()
        {
            try
            {
                var statuses = Service.GetAll().OrderBy(e => e.WorkflowStatus).ToList();

                if (statuses.Count != 0)
                {
                    return Ok(statuses);
                }
                return ResponseMessage(new ResponseMessageService(HttpStatusCode.NotFound, "No status types found.", Request.RequestUri.AbsoluteUri));
            }
            catch (Exception e)
            {
                return ExceptionMSG(e);
            }
        }


       [Route("api/workflowstatus/{page:int}/{pageSize:int}")]
        public IHttpActionResult GetAllStatus(int page, int pageSize)
        {
            try
            {
                var invstatus = new WorkflowStatusDTO(Service.GetAllFixed(page, pageSize));

                if (invstatus.Allstatuses.Count != 0)
                {
                    return Ok(invstatus.Allstatuses);
                }
                return ResponseMessage(new ResponseMessageService(HttpStatusCode.NotFound, "No workflow status found.", Request.RequestUri.AbsoluteUri));
            }
            catch (Exception e)
            {
                return ExceptionMSG(e);
            }
        }

        public IHttpActionResult Get(int id)
        {
            try
            {
                return Utils.Response(new WorkflowStatusDTO(Service.Get(id)), HttpStatusCode.NotFound, string.Format("Status with Id {0} not found.", id), Request.RequestUri.AbsoluteUri);
            }
            catch (Exception e)
            {

                return ExceptionMSG(e);
            }
        }


        // POST api/<controller>        
        public IHttpActionResult Post([FromBody]ITS_WF_Status state)
        {
            try
            {
                return Utils.Response(new WorkflowStatusDTO(Service.Add(state)), HttpStatusCode.InternalServerError, string.Format("Could not create status {0}.", state.WorkflowStatus), Request.RequestUri.AbsoluteUri);

            }
            catch (Exception e)
            {

                return ExceptionMSG(e);
            }
        }


        //[Route("api/invoicecategories/bycategory/{Type}")]
        public IHttpActionResult GetFindByStatus(string Type)
        {
            try
            {
                return Ok((Service.FindByType(Type) != null) ? new { Status = true } : new { Status = false });
            }
            catch (Exception e)
            {
                return ExceptionMSG(e);
            }
        }

        public IHttpActionResult Put([FromBody]ITS_WF_Status invStat)
        {
            try
            {
                return Utils.Response(new WorkflowStatusDTO(Service.Update(invStat)), HttpStatusCode.NotFound, string.Format("Could not update work flow status {0}.", invStat.WorkflowStatus), Request.RequestUri.AbsoluteUri);
            }
            catch (Exception e)
            {

                return ExceptionMSG(e);
            }
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                return Utils.Response(new WorkflowStatusDTO(Service.Delete(id)), HttpStatusCode.NotFound, $"Could not delete workflow status with id {id}. It might be linked somewhere", Request.RequestUri.AbsoluteUri);
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
