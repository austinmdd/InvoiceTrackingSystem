using InvoiceTrackingAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceTrackingAPI.DTO
{
    public class DoctypesLinksDTO
    {
        public int ID { get; set; }
        public int DocumentTypeID { get; set; }

        public int InvoiceCategoryID { get; set; }

        public string DocumentType { get; set; }
        
        public bool Mandatory { get; set; }        
        public bool Optional { get; set; }

        public string LinkType { get; set; }

        public DateTime? DateCreated { get; set; }
        
        public string UserCreated { get; set; }

        public int? UserUpdated { get; set; }

        public DateTime? DateUpdated { get; set; }


        public DoctypesLinksDTO(string documentType, ITS_DocumentTypeInvoiceCategoriesLinks links)
        {
            ID = links.ID;
            InvoiceCategoryID = links.InvoiceCategoryID;
            DocumentTypeID = links.DocumentTypeID;
            DocumentType = documentType;
            Mandatory = links.Mandatory;
            Optional = links.Optional;
            LinkType = (links.Mandatory) ? "Mandatory" : "Optional";
        }
        public DoctypesLinksDTO()
        {

        }

    }
}