namespace InvoiceTrackingAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ITS_Notes
    {
        public int ID { get; set; }

        public int SupplierInvoiceID { get; set; }

        public string Notes { get; set; }

        public DateTime DateCreated { get; set; }

        [Required]
        [StringLength(50)]
        public string UserCreated { get; set; }

        public int UserUpdated { get; set; }

        public DateTime DateUpdated { get; set; }

        public virtual ITS_SupplierInvoice ITS_SupplierInvoice { get; set; }
    }
}
