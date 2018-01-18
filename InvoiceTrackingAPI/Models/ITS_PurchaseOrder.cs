namespace InvoiceTrackingAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ITS_PurchaseOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ITS_PurchaseOrder()
        {
            ITS_Submission = new HashSet<ITS_Submission>();
        }

        public int ID { get; set; }

        public int SupplierID { get; set; }

        public int POTypeID { get; set; }

        [Required]
        [StringLength(12)]
        public string Status { get; set; }

        [Required]
        [StringLength(50)]
        public string PONumber { get; set; }

        [Column(TypeName = "money")]
        public decimal POAmount { get; set; }

        [Column(TypeName = "date")]
        public DateTime PODate { get; set; }

        [Column(TypeName = "money")]
        public decimal PODueAmount { get; set; }

        public DateTime DateCreated { get; set; }

        [Required]
        [StringLength(50)]
        public string UserCreated { get; set; }

        public int UserUpdated { get; set; }

        public DateTime DateUpdated { get; set; }

        public virtual ITS_POType ITS_POType { get; set; }

        public virtual ITS_Supplier ITS_Supplier { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ITS_Submission> ITS_Submission { get; set; }
    }
}
