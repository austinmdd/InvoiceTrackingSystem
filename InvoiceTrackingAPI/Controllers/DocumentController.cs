using InvoiceTrackingAPI.Models;
using InvoiceTrackingAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using InvoiceTrackingAPI.DTO;
using System.Threading.Tasks;

namespace InvoiceTrackingAPI.Controllers
{
    public class DocumentController : ApiController
    {
        DocumentService svr = new DocumentService();

        [Route("api/document"), HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                var listDocuments = svr.GetAll();

                if (listDocuments != null)
                {

                    return Ok(listDocuments);
                }
                else
                {
                    return BadRequest("No Documents Found");
                }
            }
            catch (Exception ex)
            {
                throw ex as Exception;
            }
        }

        [Route("api/document/{id}"), HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var Document = svr.Get(id);

                if (Document != null)
                {

                    return Ok(Document);
                }
                else
                {
                    return BadRequest("Document Not Found");
                }
            }
            catch (Exception ex)
            {
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest);
                httpResponseMessage.Content = new StringContent(ex.Message);
                throw new HttpResponseException(httpResponseMessage);
            }
        }

        [Route("api/document"), HttpPost]
        public async Task<IHttpActionResult> Post()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("\\temp");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);

                int SupplierInvoiceID = svr.MapFormData(provider);
                var lstUploadedDocs = svr.GetDocumentsByInvoice(SupplierInvoiceID);
                
                return Ok(lstUploadedDocs);
            }
            catch (Exception ex)
            {
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest);
                httpResponseMessage.Content = new StringContent(ex.Message);
                throw new HttpResponseException(httpResponseMessage);
            }

        }

        [Route("api/document/{id}"), HttpPut]
        public IHttpActionResult Put([FromBody]int id)
        {
            return Ok();
        }


        [Route("api/document/{id}"), HttpDelete]
        public IHttpActionResult Delete(int id)
        {

            try
            {
                bool result = svr.DeleteDocument(id);
            
                return Ok("Document successfully deleted");
            }
            catch (Exception ex)
            {
           
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest);
                httpResponseMessage.Content = new StringContent(ex.Message);
                throw new HttpResponseException(httpResponseMessage);
            }

        }

        [Route("api/document/list/{id}"), HttpGet]
        public IHttpActionResult List(int id)
        {
            List<DocumentsDTO> lstDocs = svr.GetDocumentsByInvoice(id);
            return Ok(lstDocs);
        }

        [Route("api/document/find/{id}"), HttpGet]
        public IHttpActionResult GetDocument(string id)
        {
            var document = svr.DownloadDocument(id);
            return Ok(document);
        }

        [Route("api/document/destroy"), HttpPost]
        public IHttpActionResult DestroyTempFile([FromBody] dynamic Doc)
        {
            bool result = svr.DestroyFile(Doc.Name.ToString());
            return Ok(result);
        }
    }
}
