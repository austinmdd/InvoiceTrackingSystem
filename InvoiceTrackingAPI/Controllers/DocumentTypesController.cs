using InvoiceTrackingAPI.DTO;
using InvoiceTrackingAPI.DTO.Maintain;
using InvoiceTrackingAPI.Models;
using InvoiceTrackingAPI.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InvoiceTrackingAPI.Controllers
{    
    public class DocumentTypesController : ApiController
    {

        DocumentTypeService Service = new DocumentTypeService();
        UtilsService<DocumentTypeDTO> Utils = new UtilsService<DocumentTypeDTO>();
        UtilsService<DoctypesLinksDTO> DtUtil = new UtilsService<DoctypesLinksDTO>();
        // GET api/<controller>       

        //UtilsService<ITS_DocumentType> Utils = new UtilsService<ITS_DocumentType>();
        // GET api/<controller>  
        public IHttpActionResult Get()
        {
            try
            {
                var doctypes = Service.GetAll().OrderBy(e => e.DocumentType).ToList();
                if (doctypes.Count != 0)
                {
                    return Ok(doctypes);
                }
                return ResponseMessage(new ResponseMessageService(HttpStatusCode.NotFound, "No document types found.", Request.RequestUri.AbsoluteUri));
            }
            catch (Exception e)
            {
                return ExceptionMSG(e);
            }
        }
     
        // GET api/<controller>     
        [Route("api/documenttypes/{page:int}/{pageSize:int}")]
        public IHttpActionResult GetAllFixed(int page, int pageSize)
        {           
                var doctypes = new DocumentTypeCountDTO(Service.GetAllFixed(page, pageSize));
                if (doctypes.Docs.Count != 0)
                {
                    return Ok(doctypes.Docs);
                }
                return ResponseMessage(new ResponseMessageService(HttpStatusCode.NotFound, "No document types found.", Request.RequestUri.AbsoluteUri));            
        }

        
        public IHttpActionResult Get(int id)
        {            
           return Utils.Response(MapService.MapOne<ITS_DocumentType, DocumentTypeDTO>(Service.Get(id)), HttpStatusCode.NotFound, string.Format("Document type with Id {0} not found.", id), Request.RequestUri.AbsoluteUri);                                
        }
        
        // POST api/<controller>        
        public IHttpActionResult Post([FromBody]ITS_DocumentType supplier)
        {
                 return Utils.Response(MapService.MapOne<ITS_DocumentType, DocumentTypeDTO>(Service.Add(supplier)), HttpStatusCode.InternalServerError, string.Format("Could not create document type {0}.", supplier.DocumentType), Request.RequestUri.AbsoluteUri);            
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put([FromBody]ITS_DocumentType docType)
        {           
           return Utils.Response(MapService.MapOne<ITS_DocumentType, DocumentTypeDTO>(Service.Update(docType)), HttpStatusCode.NotFound, string.Format("Could not update document type {0}.", docType.DocumentType), Request.RequestUri.AbsoluteUri);           
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {            
           return Utils.Response(MapService.MapOne<ITS_DocumentType, DocumentTypeDTO>(Service.Delete(id)), HttpStatusCode.NotFound, string.Format("Could not delete document type with id {0}.", id), Request.RequestUri.AbsoluteUri);            
        }

        [Route("api/documenttypes/bytype/{Type}")]
        public IHttpActionResult GetFindByStatus(string Type)
        {                         
            return Ok((Service.FindByType(Type) != null) ? new { Status = true } : new { Status = false });            
        }

        [Route("api/documenttypes/links")]
        public IHttpActionResult PostlinkedDoctypes([FromBody] List<DoctypesLinksDTO> linked)
        {           
                if (linked.Count != 0)
                {
                    Service.AddLinks(linked);                    
                    return Ok(new { Status = "Added", Count = linked.Count });
                }
                return ResponseMessage(new ResponseMessageService(HttpStatusCode.NotFound, "No document types to link were provided.", Request.RequestUri.AbsoluteUri));            
        }

        [Route("api/documenttypes/links/{InvCatId}")]
        public IHttpActionResult GetLinkedDoctypes(int InvCatId)
        {           
                int Count = 0;

                var doctypes = Service.GetInvoiceCatLinksTypes(InvCatId,ref Count);
                if (Count != 0)
                {
                    return Ok(doctypes);
                }
                return ResponseMessage(new ResponseMessageService(HttpStatusCode.NotFound, "No document types found.", Request.RequestUri.AbsoluteUri));            
        }

        [Route("api/documenttypes/links")]
        public IHttpActionResult PutLinkedDoctype([FromBody] DoctypesLinksDTO docTypeLink)
        {                        
           return DtUtil.Response(Service.UpdateLink(docTypeLink), HttpStatusCode.NotFound, string.Format("Could not update link for document type {0}.", docTypeLink.DocumentType), Request.RequestUri.AbsoluteUri);            
        }

        [Route("api/documenttypes/links/{id}")]
        public IHttpActionResult DeleteLinkedDoctype(int id)
        {           
           return DtUtil.Response(Service.DeleteLink(id), HttpStatusCode.NotFound, string.Format("Could not delete link with id {0}.", id), Request.RequestUri.AbsoluteUri);            
        }

        [Route("api/documenttypes/links/find/{id}")]
        public IHttpActionResult GetByLinkId(int id)
        {            
           return DtUtil.Response(Service.GetLink(id), HttpStatusCode.NotFound, string.Format("Document type link with Id {0} not found.", id), Request.RequestUri.AbsoluteUri);           
        }

        private IHttpActionResult ExceptionMSG(Exception e)
        {
            return Utils.Error(e.StackTrace, Request.RequestUri.AbsoluteUri);
        }

    }
}