namespace InvoiceTrackingAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ITS_InvoiceCategories
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ITS_InvoiceCategories()
        {
            ITS_DocumentTypeInvoiceCategoriesLinks = new HashSet<ITS_DocumentTypeInvoiceCategoriesLinks>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(12)]
        public string InvoiceCategory { get; set; }

        public string Description { get; set; }

        public bool EnabledYN { get; set; }

        public DateTime DateCreated { get; set; }

        [Required]
        [StringLength(50)]
        public string UserCreated { get; set; }

        public int UserUpdated { get; set; }

        public DateTime DateUpdated { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ITS_DocumentTypeInvoiceCategoriesLinks> ITS_DocumentTypeInvoiceCategoriesLinks { get; set; }
    }
}
