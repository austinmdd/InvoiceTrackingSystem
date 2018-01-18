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
    public class WorkflowRuleController : ApiController
    {
        
        WorflowRuleService service = new WorflowRuleService();
        UtilsService<RuleDTO> Utils = new UtilsService<RuleDTO>();
        //UtilsService<RuleCountDTO> Util = new UtilsService<RuleCountDTO>();


        public IHttpActionResult Get()
        {
            try
            {
                var ruls = new RuleDTO(service.GetAll());
                if (ruls.Allrules.Count != 0)
                {
                    return Ok(ruls.Allrules);
                }
                return ResponseMessage(new ResponseMessageService(HttpStatusCode.NotFound, "No rules found.", Request.RequestUri.AbsoluteUri));
            }
            catch (Exception e)
            {
                return ExceptionMSG(e);
            }
            
        }


        [Route("api/workflowrule/{page:int}/{pageSize:int}")]
        public IHttpActionResult GetAllFixed(int page, int pageSize)
        {
            try
            {
                var rules = new RuleDTO(service.GetAllFixed(page, pageSize));

                if (rules.Allrules.Count != 0)
                {
                    return Ok(rules.Allrules);
                }
                return ResponseMessage(new ResponseMessageService(HttpStatusCode.NotFound, "No rules found.", Request.RequestUri.AbsoluteUri));
            }
            catch (Exception e)
            {
                return ExceptionMSG(e);
            }
        }
        [Route("api/workflowrule/byname/{rule}")]
        public IHttpActionResult GetFindByStatus(string rule)
        {
            try
            {
                return Ok((service.FindByRuleName(rule) != null) ? new { Status = true } : new { Status = false });
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
                return Utils.Response(new RuleDTO(service.Get(id)), HttpStatusCode.NotFound, string.Format("rule with Id {0} not found.", id), Request.RequestUri.AbsoluteUri);
            }
            catch (Exception e)
            {

                return ExceptionMSG(e);
            }
        }


        // POST api/<controller>        
        public IHttpActionResult Post([FromBody]ITS_WF_Checklist rule)
        {
            try
            {
                return Utils.Response(new RuleDTO(service.Add(rule)), HttpStatusCode.InternalServerError, string.Format("Could not create rule {0}.", rule.ChecklistName), Request.RequestUri.AbsoluteUri);

            }
            catch (Exception e)
            {

                return ExceptionMSG(e);
            }
        }
       public IHttpActionResult Put([FromBody]ITS_WF_Checklist rule)
        {
            try
            {
                return Utils.Response(new RuleDTO(service.Update(rule)), HttpStatusCode.NotFound, string.Format("Could not update rule {0}.", rule.ChecklistName), Request.RequestUri.AbsoluteUri);
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
                return Utils.Response(new RuleDTO(service.Delete(id)), HttpStatusCode.NotFound, $"Could not delete rule with id {id}.", Request.RequestUri.AbsoluteUri);
            }
            catch (Exception e)
            {
                return ExceptionMSG(e);
            }
        }
        [Route("api/workflowrule/search/{PageNum:int}/{PageSize:int}/{Searchtext?}")]

        public IHttpActionResult GetSearchResults(int PageNum, int PageSize, string Searchtext)
        {


            try
            {
                var search = new RuleDTO(service.GetSearchedRules(PageNum, PageSize, Searchtext));

                if (search.Allrules.Count != 0)
                {
                    return Ok(search.Allrules);
                }
                return ResponseMessage(new ResponseMessageService(HttpStatusCode.NotFound, "No rules found.", Request.RequestUri.AbsoluteUri));
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
