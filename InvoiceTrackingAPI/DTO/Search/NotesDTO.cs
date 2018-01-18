using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InvoiceTrackingAPI.Models;

namespace InvoiceTrackingAPI.DTO.Search
{
    public class NotesDTO
    {
        public int ID { get; set; }
        public int SupplierInvoiceID { get; set; }
        public string Notes { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserCreated { get; set; }
        public int UserUpdated { get; set; }
        public DateTime DateUpdated { get; set; }
        
        public NotesDTO(ITS_Notes notes)
        {
            ID = notes.ID;
            SupplierInvoiceID = notes.SupplierInvoiceID;
            Notes = notes.Notes;
            DateCreated = notes.DateCreated;
            UserCreated = notes.UserCreated;
            UserUpdated = notes.UserUpdated;
            DateUpdated = notes.DateUpdated;
        }

    }

 }