using InvoiceTrackingAPI.DTO.Search;
using InvoiceTrackingAPI.Models;
using InvoiceTrackingAPI.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InvoiceTrackingAPI.Controllers
{    
    public class SuppliersController : ApiController
    {

        SupplierService Service = new SupplierService();
        SupplierInvoiceService InvoiceService = new SupplierInvoiceService();
        UtilsService<ITS_Supplier> Utils = new UtilsService<ITS_Supplier>();
        UtilsService<SupplierDTO> UDto = new UtilsService<SupplierDTO>();
        UtilsService<SupplierInvoiceDTO> InvUDto = new UtilsService<SupplierInvoiceDTO>();
        // GET api/<controller>       
        public IHttpActionResult Get()
        {           
                var supps = Service.GetAll();                
                if (supps.Count != 0)
                {
                    return Ok(new POSearchDTO(supps));

                }
                return ResponseMessage(new ResponseMessageService(HttpStatusCode.NotFound, "No suppliers found.", Request.RequestUri.AbsoluteUri));           
        }

        
        public IHttpActionResult Get(int id)
        {                           
                return UDto.Response(new SupplierDTO(Service.Get(id)), HttpStatusCode.NotFound, string.Format("Supplier with Id {0} not found.", id), Request.RequestUri.AbsoluteUri);
                    
        }


        [Route("api/suppliers/find/{PO}")]
        public IHttpActionResult GetFindByPO(string PO)
        {                      
                return UDto.Response(Service.GetByPO(PO), HttpStatusCode.NotFound, string.Format("Purchase Order with PO Number {0} not found.", PO), Request.RequestUri.AbsoluteUri);                            
        }

        [Route("api/suppliers/byname/{Name}")]
        public IHttpActionResult GetFindByName(string Name)
        {           
                var supps = Service.FindByName(Name);
                if (supps.Count != 0)
                {
                    return Ok(new POSearchDTO(supps).Suppliers.ToArray());
                }
                return ResponseMessage(new ResponseMessageService(HttpStatusCode.NotFound, string.Format("No supplier named {0} found .",Name), Request.RequestUri.AbsoluteUri));           
        }


        // POST api/<controller>        
        public IHttpActionResult Post([FromBody]ITS_Supplier supplier)
        {            
                return Utils.Response(Service.Add(supplier), HttpStatusCode.InternalServerError, string.Format("Could not create supplier {0}.", supplier.Name), Request.RequestUri.AbsoluteUri);            
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put([FromBody]ITS_Supplier supplier)
        {           
                return Utils.Response(Service.Update(supplier), HttpStatusCode.NotFound, string.Format("Could not update supplier {0}.", supplier.Name), Request.RequestUri.AbsoluteUri);            
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {            
                return Utils.Response(Service.Delete(id), HttpStatusCode.NotFound, string.Format("Could not delete supplier with id {0}.", id), Request.RequestUri.AbsoluteUri);                        
        }

        private IHttpActionResult ExceptionMSG(Exception e)
        {
            return Utils.Error(e.Message, Request.RequestUri.AbsoluteUri);
        }

        [Route("api/suppliers/byname/{PageNum:int}/{PageSize:int}/{Name}")]
        public IHttpActionResult GetFindByNamePaging(int PageNum,int PageSize, string Name)
        {                         
                var supps = Service.GetByNamePaging(PageNum, PageSize, Name);               
                if (supps != null)
                {
                    return Ok(supps);
                }
                return ResponseMessage(new ResponseMessageService(HttpStatusCode.NotFound, string.Format("No supplier named {0} found .", Name), Request.RequestUri.AbsoluteUri));            
        }


        [Route("api/suppliers/find/invoice/{InvNum}")]
        public IHttpActionResult GetFindByInvoiceNumber(string InvNum)
        {                                     
                return InvUDto.Response(InvoiceService.GetInvoiceNumber(InvNum), HttpStatusCode.NotFound, string.Format("Supplier's Invoice with Invoice Number {0} not found.", InvNum), Request.RequestUri.AbsoluteUri);              
        }

        [Route("api/suppliers/invoice/supplier/{PageNum:int}/{PageSize:int}/{Name}")]
        public IHttpActionResult GetFindBySupplierNamePaging(int PageNum, int PageSize, string Name)
        {                         
                var invoices = InvoiceService.GetBySupplierNamePaging(PageNum, PageSize, Name);               
                if (invoices != null)
                {
                    return Ok(invoices);
                }
                return ResponseMessage(new ResponseMessageService(HttpStatusCode.NotFound, string.Format("No supplier named {0} found .", Name), Request.RequestUri.AbsoluteUri));           
        }


    }
}