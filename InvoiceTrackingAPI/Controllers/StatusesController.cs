using InvoiceTrackingAPI.DTO.Maintain;
using InvoiceTrackingAPI.Models;
using InvoiceTrackingAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InvoiceTrackingAPI.Controllers
{
    public class StatusesController : ApiController
    {
        StatusService Service = new StatusService();
        UtilsService<ITS_Status> Utils = new UtilsService<ITS_Status>();
        // GET api/<controller>       
        public IHttpActionResult Get()
        {           
                var statuses = Service.GetAll().OrderBy(e => e.Status).ToList();               
                if (statuses.Count != 0)
                {                  
                    return Ok(MapService.MapMany<ITS_Status, StatusDTO>(statuses));
                }
                return ResponseMessage(new ResponseMessageService(HttpStatusCode.NotFound, "No statuses found.", Request.RequestUri.AbsoluteUri));
        }


        public IHttpActionResult Get(int id)
        {            
           return Utils.Response(Service.Get(id), HttpStatusCode.NotFound, string.Format("Status with Id {0} not found.", id), Request.RequestUri.AbsoluteUri);            
        }


        // POST api/<controller>        
        public IHttpActionResult Post([FromBody]ITS_Status status)
        {           
          return Utils.Response(Service.Add(status), HttpStatusCode.InternalServerError, string.Format("Could not create status {0}.", status.Status), Request.RequestUri.AbsoluteUri);            
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put([FromBody]ITS_Status status)
        {            
            return Utils.Response(Service.Update(status), HttpStatusCode.NotFound, string.Format("Could not update status {0}.", status.Status), Request.RequestUri.AbsoluteUri);            
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {           
           return Utils.Response(Service.Delete(id), HttpStatusCode.NotFound, string.Format("Could not delete status with id {0}.", id), Request.RequestUri.AbsoluteUri);            
        }

        [Route("api/statuses/bystatus/{Status}")]
        public IHttpActionResult GetFindByStatus(string Status)
        {                        
          return Ok((Service.FindByStatus(Status) != null) ? new { Status = true } : new { Status = false });                         
        }       

    }
}

