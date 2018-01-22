namespace InvoiceTrackingAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ITS_Navigation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ITS_Navigation()
        {
            ITS_Navigation1 = new HashSet<ITS_Navigation>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int? ParentID { get; set; }

        [Required]
        [StringLength(50)]
        public string Path { get; set; }

        [StringLength(50)]
        public string Component { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DisplayOrder { get; set; }

        [StringLength(50)]
        public string Icon { get; set; }

        public DateTime DateCreated { get; set; }

        [Required]
        [StringLength(50)]
        public string UserCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        [Required]
        [StringLength(50)]
        public string UserUpdated { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ITS_Navigation> ITS_Navigation1 { get; set; }

        public virtual ITS_Navigation ITS_Navigation2 { get; set; }
    }
}
