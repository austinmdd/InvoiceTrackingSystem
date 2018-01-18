namespace InvoiceTrackingAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ITS_Document
    {
        public int ID { get; set; }

        public int SupplierInvoiceID { get; set; }

        public int DocumentTypeID { get; set; }

        [Required]
        public string DocumentName { get; set; }

        [Required]
        public string DocumentType { get; set; }

        [StringLength(50)]
        public string DocumentSize { get; set; }

        [Required]
        public string EDMSID { get; set; }

        public string EDMSLocation { get; set; }

        public string EDMSName { get; set; }

        public string EDMSLink { get; set; }

        public DateTime DateCreated { get; set; }

        [Required]
        [StringLength(50)]
        public string UserCreated { get; set; }

        public int UserUpdated { get; set; }

        public DateTime DateUpdated { get; set; }

        public virtual ITS_DocumentType ITS_DocumentType { get; set; }

        public virtual ITS_SupplierInvoice ITS_SupplierInvoice { get; set; }
    }
}
