using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InvoiceTrackingAPI.Models;

namespace InvoiceTrackingAPI.DTO.Maintain
{
    public class InvoiceCategoriesCountDTO
    {
        public int Count { get; set; }
        public List<InvoiceCategoriesDTO> Categories = new List<InvoiceCategoriesDTO>();
        public InvoiceCategoriesCountDTO(List<ITS_InvoiceCategories> categories, int records = 0)
        {
            Count = records;
            foreach (ITS_InvoiceCategories category in categories)
            {
                Categories.Add(new InvoiceCategoriesDTO(category));
            }
        }
    }
}