using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using InvoiceTrackingAPI.DTO.Search;
using InvoiceTrackingAPI.Models;
using InvoiceTrackingAPI.Services;

namespace InvoiceTrackingAPI.Controllers
{
    public class NotesController : ApiController
    {
        NotesService service = new NotesService();
        UtilsService<NotesDTO> Util = new UtilsService<NotesDTO>();


        public IHttpActionResult Post([FromBody]ITS_Notes notes)
        {
            try
            {
                return Util.Response(new NotesDTO(service.Add(notes)), HttpStatusCode.InternalServerError, string.Format("Could not create invoice notes {0}.", notes.SupplierInvoiceID), Request.RequestUri.AbsoluteUri);
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
                var notes = new NotesCountDTO(service.GetAllNotesByID(id));
                var newnotes = new List<NotesDTO>(notes.Notes.OrderByDescending(o => o.DateCreated).ToList());
                if (newnotes.Count != 0)
                {
                    return Ok(newnotes);
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
            return Util.Error(e.Message, Request.RequestUri.AbsoluteUri);
        }

    }
}
