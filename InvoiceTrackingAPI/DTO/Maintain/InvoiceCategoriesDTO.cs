using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InvoiceTrackingAPI.Models;

namespace InvoiceTrackingAPI.DTO.Maintain
{
    public class InvoiceCategoriesDTO
    {
        public int ID { get; set; }
        public string InvoiceCategory { get; set; }
        public string Description { get; set; }
        public bool EnabledYN { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserCreated { get; set; }
        public int UserUpdated { get; set; }
        public DateTime DateUpdated { get; set; }

        public InvoiceCategoriesDTO(ITS_InvoiceCategories invoicecategories)
        {
            ID = invoicecategories.ID;
            InvoiceCategory = invoicecategories.InvoiceCategory;
            Description = invoicecategories.Description;
            EnabledYN = invoicecategories.EnabledYN;
            DateCreated = invoicecategories.DateCreated;
            UserCreated = invoicecategories.UserCreated;
            UserUpdated = invoicecategories.UserUpdated;
            DateUpdated = invoicecategories.DateUpdated;
        }


    }
}