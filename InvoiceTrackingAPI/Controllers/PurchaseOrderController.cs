using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using InvoiceTrackingAPI.Models;
using InvoiceTrackingAPI.Services;

namespace InvoiceTrackingAPI.Controllers
{
    public class PurchaseOrderController : ApiController
    {
        PurchaseOrderService svr = new PurchaseOrderService();

        public PurchaseOrderController()
        {

        }

        [Route("api/purchaseorder"), HttpGet]
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
                    return BadRequest("Purchase Order(s) Not Found");
                }
            }
            catch (Exception ex)
            {
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest);
                httpResponseMessage.Content = new StringContent(ex.Message);
                throw new HttpResponseException(httpResponseMessage);
            }
        }

        [Route("api/purchaseorder/{id}"), HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var PO = svr.Get(id);

                if (PO != null)
                {
                    var sisDTO = svr.MapDTO(PO);

                    return Ok(sisDTO);
                }
                else
                {
                    return BadRequest("Purchase Order Not Found");
                }
            }
            catch (Exception ex)
            {
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest);
                httpResponseMessage.Content = new StringContent(ex.Message);
                throw new HttpResponseException(httpResponseMessage);
            }
        }


        [Route("api/purchaseorder"), HttpPost]
        public IHttpActionResult Post([FromBody]Object PO)
        {
            return Ok();
        }

        [Route("api/purchaseorder/{id}"), HttpPut]
        public IHttpActionResult Put([FromBody]ITS_PurchaseOrder PO)
        {
            return Ok();
        }

        [Route("api/purchaseorder/{id}"), HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            return Ok();
        }
    }
}
