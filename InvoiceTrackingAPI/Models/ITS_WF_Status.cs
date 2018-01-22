namespace InvoiceTrackingAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ITS_WF_Status
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ITS_WF_Status()
        {
            ITS_WF_Process = new HashSet<ITS_WF_Process>();
            ITS_WF_Route = new HashSet<ITS_WF_Route>();
        }

        public int ID { get; set; }

        public int? StatusCode { get; set; }

        [Required]
        [StringLength(50)]
        public string WorkflowStatus { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        [Required]
        [StringLength(50)]
        public string UserCreated { get; set; }

        public int UserUpdated { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        public bool? IsDeleted { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ITS_WF_Process> ITS_WF_Process { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ITS_WF_Route> ITS_WF_Route { get; set; }
    }
}
