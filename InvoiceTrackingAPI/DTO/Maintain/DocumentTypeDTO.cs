using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InvoiceTrackingAPI.Models;

namespace InvoiceTrackingAPI.DTO.Maintain
{
    public class DocumentTypeDTO
    {
        public int ID { get; set; }
        public string DocumentType { get; set; }
        public string Description { get; set; }
        public bool EnabledYN { get; set; }
        public DateTime DateCreated { get; set; }     
        public string UserCreated { get; set; }
        public int UserUpdated { get; set; }
        public DateTime DateUpdated { get; set; }

        public DocumentTypeDTO(ITS_DocumentType docs)
        {
            ID = docs.ID;
            DocumentType = docs.DocumentType;
            Description = docs.Description;
            EnabledYN = docs.EnabledYN;
            DateCreated = docs.DateCreated;
            UserCreated = docs.UserCreated;
            UserUpdated = docs.UserUpdated;
            DateUpdated = docs.DateUpdated;
        }
        public DocumentTypeDTO()
        {

        }
    }
}