using InvoiceTrackingAPI.Models;
using InvoiceTrackingAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceTrackingAPI.DTO.Search
{
    public class InvoiceSearchDTO
    {
        public int Count { get; set; }
        public List<SupplierInvoiceDTO> Invoices = new List<SupplierInvoiceDTO>();
        
        public InvoiceSearchDTO(List<ITS_SupplierInvoice> types, int records = 0)
        {
            Count = records;
            foreach (ITS_SupplierInvoice type in types)
            {
               //var obj =  (D)Activator.CreateInstance(typeof(D), type);                
                //Invoices.Add(new SupplierInvoiceDTO(type));
            }

        }
    }
}