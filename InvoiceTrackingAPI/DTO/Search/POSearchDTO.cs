using InvoiceTrackingAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceTrackingAPI.DTO.Search
{
    public class POSearchDTO
    {
        public int Count { get; set; }
        public List<SupplierDTO> Suppliers = new List<SupplierDTO>();
        public POSearchDTO(List<ITS_Supplier> suppliers, int records=0)
        {
            Count = records;
            foreach(ITS_Supplier supplier in suppliers)
            {
                Suppliers.Add(new SupplierDTO(supplier));
            }

        }
    }
}