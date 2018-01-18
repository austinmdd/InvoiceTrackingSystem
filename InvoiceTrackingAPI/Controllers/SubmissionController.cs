using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using InvoiceTrackingAPI.DTO;
using InvoiceTrackingAPI.Services;
using Newtonsoft.Json;
using System.Data.Entity.Validation;
using System.Web;
using System.IO;
using System.Threading.Tasks;

namespace InvoiceTrackingAPI.Controllers
{
    public class SubmissionController : ApiController
    {

        SubmissionService svr = new SubmissionService();
        HelperService helper = new HelperService();

        [Route("api/submission"), HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                var listPO = svr.GetAll();

                if (listPO != null)
                {
                    return Ok(listPO);
                }
                else
                {
                    return BadRequest("Submission(s) Not Found");
                }
            }
            catch (Exception ex)
            {
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest);
                httpResponseMessage.Content = new StringContent(ex.Message);
                throw new HttpResponseException(httpResponseMessage);
            }
        }

        [Route("api/submission/{id}"), HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var PO = svr.Get(id);

                if (PO != null)
                {
                    return Ok(PO);
                }
                else
                {
                    return BadRequest("Submission Not Found");
                }
            }
            catch (Exception ex)
            {
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest);
                httpResponseMessage.Content = new StringContent(ex.Message);
                throw new HttpResponseException(httpResponseMessage);
            }
        }


        [Route("api/submission"), HttpPost]
        public IHttpActionResult Post([FromBody] SupplierInvoiceSubmissionDTO entity)
        {
                string subNumber;

                if (svr.ValidEntity(entity) == true)
                {
                if (svr.InvoiceNumberUnique(entity.InvoiceNumber, entity.VendorCode) == true)
                {
                    try
                    {
                        subNumber = svr.SubmitInvoice(entity);
                        return Ok(subNumber);
                    }
                    catch (Exception ex)
                    {

                        string ContentMessage = helper.ExceptionHelper(ex);

                        HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                        httpResponseMessage.Content = new StringContent(ContentMessage);
                        throw new HttpResponseException(httpResponseMessage);
                    }
                }
                else
                {
                    string ContentMessage = "Invoice number *"+ entity.InvoiceNumber + " already been used.";

                    HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                    httpResponseMessage.Content = new StringContent(ContentMessage);
                    throw new HttpResponseException(httpResponseMessage);
                }
                }
                else
                {
                    HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                    httpResponseMessage.Content = new StringContent("Invalid Entity");
                    throw new HttpResponseException(httpResponseMessage);
                }
            
        }

        [Route("api/submission/{id}"), HttpPut]
        public IHttpActionResult Put([FromBody]Object PO)
        {
            return Ok();
        }

        [Route("api/submission/{id}"), HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            return Ok();
        }

    }
}
