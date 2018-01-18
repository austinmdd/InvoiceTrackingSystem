using InvoiceTrackingAPI.Models;
using InvoiceTrackingAPI.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using InvoiceTrackingAPI.DTO.Maintain;

namespace InvoiceTrackingAPI.Controllers
{
    public class InvoiceCategoriesController : ApiController
    {
        InvoiceCategories Service = new InvoiceCategories();
        //UtilsService<ITS_InvoiceCategories> Utils = new UtilsService<ITS_InvoiceCategories>();
        UtilsService<InvoiceCategoriesDTO> Utils = new UtilsService<InvoiceCategoriesDTO>();
        UtilsService<InvoiceCategoriesCountDTO> Util = new UtilsService<InvoiceCategoriesCountDTO>();

        public IHttpActionResult Get()
        {
            try
            {
                var invVategories = new InvoiceCategoriesCountDTO(Service.GetAll());
                if (invVategories.Categories.Count != 0)
                {
                    return Ok(invVategories.Categories);
                }
                return ResponseMessage(new ResponseMessageService(HttpStatusCode.NotFound, "No invoice categories found.", Request.RequestUri.AbsoluteUri));
            }
            catch (Exception e)
            {
                return ExceptionMSG(e);
            }
        }


        [Route("api/invoicecategories/{page:int}/{pageSize:int}")]
        public IHttpActionResult GetAllFixed(int page, int pageSize)
        {
            try
            {
                var invCategories = new InvoiceCategoriesCountDTO(Service.GetAllFixed(page, pageSize));
                if (invCategories.Categories.Count != 0)
                {
                    return Ok(invCategories.Categories);
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
                return Utils.Response(new InvoiceCategoriesDTO(Service.Get(id)), HttpStatusCode.NotFound, string.Format("Invoice category with Id {0} not found.", id), Request.RequestUri.AbsoluteUri);
            }
            catch (Exception e)
            {

                return ExceptionMSG(e);
            }
        }


        // POST api/<controller>        
        public IHttpActionResult Post([FromBody]ITS_InvoiceCategories supplier)
        {
            try
            {
                return Utils.Response(new InvoiceCategoriesDTO(Service.Add(supplier)), HttpStatusCode.InternalServerError, string.Format("Could not create invoice category {0}.", supplier.InvoiceCategory), Request.RequestUri.AbsoluteUri);

            }
            catch (Exception e)
            {

                return ExceptionMSG(e);
            }
        }


        [Route("api/invoicecategories/bycategory/{Type}")]
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

        public IHttpActionResult Put([FromBody]ITS_InvoiceCategories invCat)
        {
            try
            {
                return Utils.Response(new InvoiceCategoriesDTO(Service.Update(invCat)), HttpStatusCode.NotFound, string.Format("Could not update invoice category {0}.", invCat.InvoiceCategory), Request.RequestUri.AbsoluteUri);
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
                return Utils.Response(new InvoiceCategoriesDTO(Service.Delete(id)), HttpStatusCode.NotFound, $"Could not delete invoice category with id {id}. It might be linked to a document type", Request.RequestUri.AbsoluteUri);
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
