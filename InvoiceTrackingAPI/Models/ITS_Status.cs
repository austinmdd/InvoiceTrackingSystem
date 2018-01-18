namespace InvoiceTrackingAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ITS_Status
    {
        [Key]
        public int StatusCode { get; set; }

        [Required]
        [StringLength(12)]
        public string Status { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        public bool SupplierInvoiceYN { get; set; }

        public bool SubmissionYN { get; set; }

        public bool PurchaseOrderYN { get; set; }

        public DateTime DateCreated { get; set; }

        [Required]
        [StringLength(50)]
        public string UserCreated { get; set; }

        public int UserUpdated { get; set; }

        public DateTime DateUpdated { get; set; }
    }
}
