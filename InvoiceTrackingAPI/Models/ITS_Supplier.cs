namespace InvoiceTrackingAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ITS_Supplier
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ITS_Supplier()
        {
            ITS_PurchaseOrder = new HashSet<ITS_PurchaseOrder>();
            ITS_Submission = new HashSet<ITS_Submission>();
        }

        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string RegNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string VatRegisteredYN { get; set; }

        [Required]
        [StringLength(50)]
        public string CSDNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string VendorCode { get; set; }

        public bool TaxCompliant { get; set; }

        [Required]
        [StringLength(50)]
        public string VendorPortalID { get; set; }

        [Required]
        [StringLength(50)]
        public string EmailAddress { get; set; }

        public int UserUpdated { get; set; }

        [Required]
        [StringLength(50)]
        public string UserCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        public DateTime DateCreated { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ITS_PurchaseOrder> ITS_PurchaseOrder { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ITS_Submission> ITS_Submission { get; set; }
    }
}
