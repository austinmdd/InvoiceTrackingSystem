using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using InvoiceTrackingAPI.Models;
using InvoiceTrackingAPI.Services;
using InvoiceTrackingAPI.DTO;

namespace InvoiceTrackingAPI.Controllers
{
    public class WorkflowProcessController : ApiController
    {
        WorkflowProcessService Service = new WorkflowProcessService();
        //UtilsService<ITS_InvoiceCategories> Utils = new UtilsService<ITS_InvoiceCategories>();
        UtilsService<WorkflowProcessDTO> Utils = new UtilsService<WorkflowProcessDTO>();


        public IHttpActionResult Get()
        {
            try
            {
                var wfprocess = new WorkflowProcessDTO(Service.GetAll());
                if (wfprocess.Allprocesses.Count != 0)
                {
                    return Ok(wfprocess.Allprocesses);
                }
                return ResponseMessage(new ResponseMessageService(HttpStatusCode.NotFound, "No workflow process found.", Request.RequestUri.AbsoluteUri));

                //return Utils.Response(new WorkflowProcessDTO(Service.GetDepartmentWorkflow()), HttpStatusCode.NotFound, string.Format("No dept workflow found."), Request.RequestUri.AbsoluteUri);
            }
            catch (Exception e)
            {

                return ExceptionMSG(e);
            }
        }


        //[Route("api/workflowprocess/{page:int}/{pageSize:int}")]
        //public IHttpActionResult GetAllFixed(int page, int pageSize)
        //{
        //    try
        //    {
        //        var invstatus = new WorkflowProcessCountDTO(Service.GetAllFixed(page, pageSize));

        //        if (invstatus.Allprocesses.Count != 0)
        //        {
        //            return Ok(invstatus.Allprocesses);
        //        }
        //        return ResponseMessage(new ResponseMessageService(HttpStatusCode.NotFound, "No workflow process found.", Request.RequestUri.AbsoluteUri));
        //    }
        //    catch (Exception e)
        //    {
        //        return ExceptionMSG(e);
        //    }
        //}

        public IHttpActionResult Get(int id)
        {
            try
            {
                return Utils.Response(new WorkflowProcessDTO(Service.Get(id)), HttpStatusCode.NotFound, string.Format("Process with Id {0} not found.", id), Request.RequestUri.AbsoluteUri);
            }
            catch (Exception e)
            {

                return ExceptionMSG(e);
            }
        }
        [Route("api/workflowprocess/{page:int}/{pageSize:int}/{roleID:int}")]
        public IHttpActionResult GetDepartmentWorkflow(int page, int pageSize, int roleID)
        {
            
            try
            {
                var invstatus = new WorkflowProcessDTO(Service.GetDepartmentWorkflow(page, pageSize, roleID));

                if (invstatus.Allprocesses.Count != 0)
                {
                    return Ok(invstatus.Allprocesses);
                }
                return ResponseMessage(new ResponseMessageService(HttpStatusCode.NotFound, "No workflow process found.", Request.RequestUri.AbsoluteUri));

                //return Utils.Response(new WorkflowProcessDTO(Service.GetDepartmentWorkflow()), HttpStatusCode.NotFound, string.Format("No dept workflow found."), Request.RequestUri.AbsoluteUri);
            }
            catch (Exception e)
            {

                return ExceptionMSG(e);
            }
        }


        // POST api/<controller>        
        public IHttpActionResult Post([FromBody]ITS_WF_Process process)
        {
            try
            {
                return Utils.Response(new WorkflowProcessDTO(Service.Add(process)), HttpStatusCode.InternalServerError, string.Format("Could not create process {0}.", process.SubmissionID), Request.RequestUri.AbsoluteUri);

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

        public IHttpActionResult Put([FromBody]ITS_WF_Process invProc)
        {
            try
            {
                return Utils.Response(new WorkflowProcessDTO(Service.Update(invProc)), HttpStatusCode.NotFound, string.Format("Could not update work flow process {0}.", invProc.SubmissionID), Request.RequestUri.AbsoluteUri);
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
                return Utils.Response(new WorkflowProcessDTO(Service.Delete(id)), HttpStatusCode.NotFound, $"Could not delete workflow process with id {id}. It might be linked somewhere", Request.RequestUri.AbsoluteUri);
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
