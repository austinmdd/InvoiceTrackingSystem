using InvoiceTrackingAPI.Models;
using InvoiceTrackingAPI.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using InvoiceTrackingAPI.DTO;

namespace InvoiceTrackingAPI.Controllers
{
    public class SupplierInvoiceController : ApiController
    {
        SupplierInvoiceService Service = new SupplierInvoiceService();       
        UtilsService<UpdateSupplierInvoiceDTO> Util = new UtilsService<UpdateSupplierInvoiceDTO>();
        SupplierInvoiceService supplierService = new SupplierInvoiceService();


        public IHttpActionResult Get()
        {            
                var doctypes = Service.GetAll();
                if (doctypes.Count != 0)
                {
                    return Ok(doctypes);
                }
                return ResponseMessage(new ResponseMessageService(HttpStatusCode.NotFound, "No supplier invoice found.", Request.RequestUri.AbsoluteUri));            
        }

        /*public IHttpActionResult Get(int id)
        {
            try
            {                
                return Util.Response(new UpdateSupplierInvoiceDTO( Service.Get(id)), HttpStatusCode.NotFound, string.Format("Supplier invoice with Id {0} not found.", id), Request.RequestUri.AbsoluteUri);
            }
            catch (Exception e)
            {
                return ExceptionMSG(e);
            }
        }*/

        public IHttpActionResult Put([FromBody]ITS_SupplierInvoice invUpdate)
        {
            try
            {
                return Util.Response(new UpdateSupplierInvoiceDTO(Service.Update(invUpdate)), HttpStatusCode.NotFound, string.Format("Could not update invoice {0}.", invUpdate.InvoiceNumber), Request.RequestUri.AbsoluteUri);
            }
            catch (Exception e)
            {
                return ExceptionMSG(e);
            }
        }



        [Route("api/supplierinvoice/all/{PageNum:int}/{PageSize:int}/{Type?}")]
        public IHttpActionResult GetFindByNamePaging(int PageNum, int PageSize,string Type= "All")
        {
            var supps = supplierService.FindAllPaging(PageNum, PageSize, Type);
            if (supps != null)
            {
                return Ok(supps);
            }
            return ResponseMessage(new ResponseMessageService(HttpStatusCode.NotFound, string.Format("No supplier invoices found."), Request.RequestUri.AbsoluteUri));
        }


        [Route("api/supplierinvoice/sort/{PageNum:int}/{PageSize:int}/{Type?}/{flag?}/{order?}")]
        public IHttpActionResult GetFindAllSortPaging(int PageNum, int PageSize, string Type = "All", int flag = 3, bool order = true)
        {
            var supps = supplierService.GetAllSortPaging(PageNum, PageSize, Type,flag,order);
            if (supps != null)
            {
                return Ok(supps);
            }
            return ResponseMessage(new ResponseMessageService(HttpStatusCode.NotFound, string.Format("No supplier invoices found."), Request.RequestUri.AbsoluteUri));
        }

        [Route("api/supplierinvoice/wrkflow/{PageNum:int}/{PageSize:int}/{subid?}/{Type?}/{flag?}/{order?}")]

        public IHttpActionResult GetInvoicesBySubmissionID(int PageNum, int PageSize, int subid, string Type = "All", int flag = 3, bool order = true)
        {
            var invoices = supplierService.GetInvoicesForSubId(PageNum, PageSize, subid, Type, flag, order);
            if (invoices != null )
            {
                return Ok(invoices);
            }
            return ResponseMessage(new ResponseMessageService(HttpStatusCode.NotFound, string.Format("No supplier invoices found."), Request.RequestUri.AbsoluteUri));

        }

        [Route("api/supplierinvoice/wrkflow/{id:int}/{supInvID:int}/{oneID:bool}")]
        public IHttpActionResult GetAllBySubID(int id, int supInvID, bool oneID)
        {
            var invoices = supplierService.GetInvoicesForSubIdService(id, supInvID);
            //var inv = invoices.Items[invoices.Items.Count - 1];
            if (invoices != null)
            {
                return Ok(invoices.Items);
            }
            return ResponseMessage(new ResponseMessageService(HttpStatusCode.NotFound, "No supplier invoice found.", Request.RequestUri.AbsoluteUri));
        }


        private IHttpActionResult ExceptionMSG(Exception e)
        {
            return Util.Error(e.StackTrace, Request.RequestUri.AbsoluteUri);
        }
    }
}
