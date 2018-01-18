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
    public class ViewRoutesController : ApiController
    {
        ViewRoutesService service = new ViewRoutesService();
        UtilsService<ViewRoutesDTO> Utils = new UtilsService<ViewRoutesDTO>();


        [Route("api/viewroutes/{page:int}/{pageSize:int}")]
        public IHttpActionResult GetAllRoutes(int page, int pageSize)
        {
            try
            {
                var ruts = new ViewRoutesDTO(service.GetAllRoutes(page, pageSize));
                if (ruts.Routing.Count != 0)
                {
                    return Ok(ruts.Routing);
                }
                return ResponseMessage(new ResponseMessageService(HttpStatusCode.NotFound, "No invoice categories found.", Request.RequestUri.AbsoluteUri));
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
                return Utils.Response(new ViewRoutesDTO(service.GetRoute(id)), HttpStatusCode.NotFound, string.Format("route with Id {0} not found.", id), Request.RequestUri.AbsoluteUri);
            }
            catch (Exception e)
            {

                return ExceptionMSG(e);
            }
        }
        [Route("api/viewroutes/search/{PageNum:int}/{PageSize:int}/{Searchtext?}")]

        public IHttpActionResult GetSearchResults(int PageNum, int PageSize, string Searchtext)
        {


            try
            {
                var search = new ViewRoutesDTO(service.GetSearchedRules(PageNum, PageSize, Searchtext));

                if (search.Routing.Count != 0)
                {
                    return Ok(search.Routing);
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
