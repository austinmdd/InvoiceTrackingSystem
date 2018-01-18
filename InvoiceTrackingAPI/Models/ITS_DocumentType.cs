namespace InvoiceTrackingAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ITS_DocumentType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ITS_DocumentType()
        {
            ITS_Document = new HashSet<ITS_Document>();
            ITS_DocumentTypeInvoiceCategoriesLinks = new HashSet<ITS_DocumentTypeInvoiceCategoriesLinks>();
            ITS_POTypeDocTypeLink = new HashSet<ITS_POTypeDocTypeLink>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(12)]
        public string DocumentType { get; set; }

        public string Description { get; set; }

        public bool EnabledYN { get; set; }

        public DateTime DateCreated { get; set; }

        [Required]
        [StringLength(50)]
        public string UserCreated { get; set; }

        public int UserUpdated { get; set; }

        public DateTime DateUpdated { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ITS_Document> ITS_Document { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ITS_DocumentTypeInvoiceCategoriesLinks> ITS_DocumentTypeInvoiceCategoriesLinks { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ITS_POTypeDocTypeLink> ITS_POTypeDocTypeLink { get; set; }
    }
}
