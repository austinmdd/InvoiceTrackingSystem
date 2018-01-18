namespace InvoiceTrackingAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ITS_SupplierInvoice
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ITS_SupplierInvoice()
        {
            ITS_Document = new HashSet<ITS_Document>();
            ITS_Notes = new HashSet<ITS_Notes>();
            ITS_WF_Process = new HashSet<ITS_WF_Process>();
        }

        public int ID { get; set; }

        public int SubmissionID { get; set; }

        [Required]
        [StringLength(12)]
        public string Status { get; set; }

        [Required]
        [StringLength(50)]
        public string InvoiceNumber { get; set; }

        [Column(TypeName = "money")]
        public decimal InvoiceAmount { get; set; }

        [Column(TypeName = "date")]
        public DateTime InvoiceDate { get; set; }

        public string Description { get; set; }

        public DateTime DateCreated { get; set; }

        [Required]
        [StringLength(50)]
        public string UserCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        public int UserUpdated { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ITS_Document> ITS_Document { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ITS_Notes> ITS_Notes { get; set; }

        public virtual ITS_Submission ITS_Submission { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ITS_WF_Process> ITS_WF_Process { get; set; }
    }
}
