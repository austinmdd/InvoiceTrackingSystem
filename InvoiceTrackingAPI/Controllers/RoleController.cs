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
    public class RoleController : ApiController
    {
        RoleService service = new RoleService();
        UtilsService<RoleDTO> util = new UtilsService<RoleDTO>();

        public IHttpActionResult Get(int id)
        {
            return util.Response(new RoleDTO(service.Get(id)), HttpStatusCode.NotFound, string.Format("Role with ID {0} not found.", id), Request.RequestUri.AbsoluteUri);
        }

        [Route("api/role/{page:int}/{pageSize:int}")]
        public IHttpActionResult GetAllRoles(int page, int pageSize)
        {
            try
            {
                var role = new RoleDTO(service.GetAllRoles(page,pageSize));

                if (role.Roles.Count!= 0)
                {
                    return Ok(role.Roles);
                }
                return ResponseMessage(new ResponseMessageService(HttpStatusCode.NotFound, "No invoice categories found.", Request.RequestUri.AbsoluteUri));
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
