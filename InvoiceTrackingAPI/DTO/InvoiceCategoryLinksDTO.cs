using InvoiceTrackingAPI.DTO.Maintain;
using InvoiceTrackingAPI.Models;
using InvoiceTrackingAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceTrackingAPI.DTO
{
    public class InvoiceCategoryLinksDTO
    {
       public List<DocumentTypeDTO> unlinked = new List<DocumentTypeDTO>();
       public List<DoctypesLinksDTO> linked = new List<DoctypesLinksDTO>();

        public InvoiceCategoryLinksDTO(List<ITS_DocumentType> unlinks, List<ITS_DocumentType> links)
        {
            unlinked = MapService.MapMany<ITS_DocumentType, DocumentTypeDTO>(unlinks);
            foreach(ITS_DocumentType doctype in links)
            {
                linked.Add(new DoctypesLinksDTO(doctype.DocumentType, doctype.ITS_DocumentTypeInvoiceCategoriesLinks.FirstOrDefault()));
            }

        }
    }
}