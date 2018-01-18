namespace InvoiceTrackingAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ITS_Submission
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ITS_Submission()
        {
            ITS_WF_Process = new HashSet<ITS_WF_Process>();
            ITS_SupplierInvoice = new HashSet<ITS_SupplierInvoice>();
        }

        public int ID { get; set; }

        public string SubmissionNumber { get; set; }

        public int PurchaseOrderID { get; set; }

        public int SupplierID { get; set; }

        [Required]
        [StringLength(12)]
        public string Status { get; set; }

        [Required]
        [StringLength(50)]
        public string PONumber { get; set; }

        [Column(TypeName = "money")]
        public decimal POAmount { get; set; }

        public DateTime DateCreated { get; set; }

        [Required]
        [StringLength(50)]
        public string UserCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        public int UserUpdated { get; set; }

        public virtual ITS_PurchaseOrder ITS_PurchaseOrder { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ITS_WF_Process> ITS_WF_Process { get; set; }

        public virtual ITS_Supplier ITS_Supplier { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ITS_SupplierInvoice> ITS_SupplierInvoice { get; set; }
    }
}
