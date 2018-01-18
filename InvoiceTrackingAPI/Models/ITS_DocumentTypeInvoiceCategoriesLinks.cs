namespace InvoiceTrackingAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ITS_DocumentTypeInvoiceCategoriesLinks
    {
        public int ID { get; set; }

        public int DocumentTypeID { get; set; }

        public int InvoiceCategoryID { get; set; }

        public bool Mandatory { get; set; }

        public bool Optional { get; set; }

        public DateTime? DateCreated { get; set; }

        [StringLength(50)]
        public string UserCreated { get; set; }

        public int? UserUpdated { get; set; }

        public DateTime? DateUpdated { get; set; }

        public virtual ITS_DocumentType ITS_DocumentType { get; set; }

        public virtual ITS_InvoiceCategories ITS_InvoiceCategories { get; set; }
    }
}
