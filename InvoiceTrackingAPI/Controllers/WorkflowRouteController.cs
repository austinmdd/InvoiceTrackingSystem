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
    public class WorkflowRouteController : ApiController
    {
        WorkflowRouteService Service = new WorkflowRouteService();
        UtilsService<WorkflowRouteDTO> Utils = new UtilsService<WorkflowRouteDTO>();
        //UtilsService<WorkflowRouteCountDTO> Util = new UtilsService<WorkflowRouteCountDTO>();


        /*public IHttpActionResult Get()
        {
            try
            {
                var routes = new WorkflowRouteDTO(Service.GetAll());
                var newroutes = new List<WorkflowRouteDTO>(routes.Routes).ToList();
                if (newroutes.Count != 0)
                {
                    return Ok(newroutes);
                }
                return ResponseMessage(new ResponseMessageService(HttpStatusCode.NotFound, "No invoice categories found.", Request.RequestUri.AbsoluteUri));
            }
            catch (Exception e)
            {
                return ExceptionMSG(e);
            }
        }*/


        [Route("api/WorkflowRoute/{role:int}")]
        public IHttpActionResult Get(int role)
        {
            try
            {
                //return Utils.Response(new WorkflowRouteDTO(Service.GetAllByRoleIDs(role)), HttpStatusCode.NotFound, string.Format("Process with Id {0} not found.", role), Request.RequestUri.AbsoluteUri);
                var routes = new WorkflowRouteDTO(Service.GetAllByRoleIDs(role));
                var newroutes = new List<WorkflowRouteDTO>(routes.Routes).ToList();
                if (newroutes.Count != 0)
                {
                    return Ok(newroutes);
                }
                return ResponseMessage(new ResponseMessageService(HttpStatusCode.NotFound, "No invoice categories found.", Request.RequestUri.AbsoluteUri));

            }
            catch (Exception e)
            {
                return ExceptionMSG(e);
            }
        }

        /*[Route("api/routing/{page:int}/{pageSize:int}")]
        public IHttpActionResult GetAllFixed(int page, int pageSize)
        {
            try
            {
                var routes = new WorkflowRouteCountDTO(Service.GetAllFixed(page, pageSize));
                if (routes.Routes.Count != 0)
                {
                    return Ok(routes.Routes);
                }
                return ResponseMessage(new ResponseMessageService(HttpStatusCode.NotFound, "No invoice categories found.", Request.RequestUri.AbsoluteUri));
            }
            catch (Exception e)
            {
                return ExceptionMSG(e);
            }
        }*/
        public IHttpActionResult Put([FromBody]ITS_WF_Route rut)
        {
            try
            {
                return Utils.Response(new WorkflowRouteDTO(Service.Update(rut)), HttpStatusCode.NotFound, string.Format("Could not update route{0}.", rut.RoleTo), Request.RequestUri.AbsoluteUri);
            }
            catch (Exception e)
            {

                return ExceptionMSG(e);
            }
        }

        public IHttpActionResult Post([FromBody]ITS_WF_Route rut)
        {
            try
            {
                return Utils.Response(new WorkflowRouteDTO(Service.Add(rut)), HttpStatusCode.InternalServerError, string.Format("Could not create rut {0}.", rut.RoleFrom), Request.RequestUri.AbsoluteUri);

            }
            catch (Exception e)
            {

                return ExceptionMSG(e);
            }
        }

        [Route("api/workflowroute/allroutes/{PageNum:int}/{PageSize:int}/{order?}")]
        public IHttpActionResult GetAllRoutes(int PageNum, int PageSize, bool order = true)
        {
            var routes = Service.GetAllRoutes(PageNum, PageSize, order);

            if (routes != null)
            {
                return Ok(routes);
            }
            return ResponseMessage(new ResponseMessageService(HttpStatusCode.NotFound, string.Format("No rules found."), Request.RequestUri.AbsoluteUri));
        }
        [AcceptVerbs("GET","POST")]
        [Route("api/workflowroute/dupefind/{rolefrom:int}/{roleto:int}/{actin:int}/{status:int}")]
        public IHttpActionResult DuplicatesFind(int rolefrom,int roleto,int actin,int status)
        {
        
            return Ok((Service.SearchDuplicate(rolefrom, roleto,actin,status) != null) ? new { Duplicate = true } : new { Duplicate = false });

        }


        private IHttpActionResult ExceptionMSG(Exception e)
        {
            return Utils.Error(e.Message, Request.RequestUri.AbsoluteUri);
        }
    }
}
