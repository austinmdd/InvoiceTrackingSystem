using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InvoiceTrackingAPI.Models;

namespace InvoiceTrackingAPI.DTO.Maintain
{
    public class DocumentTypeCountDTO
    {
        public int Count { get; set; }
        public List<DocumentTypeDTO> Docs = new List<DocumentTypeDTO>();
        public DocumentTypeCountDTO(List<ITS_DocumentType> docs, int records = 0)
        {
            Count = records;
            foreach (ITS_DocumentType doc in docs)
            {
                Docs.Add(new DocumentTypeDTO(doc));
            }
        }
    }
}