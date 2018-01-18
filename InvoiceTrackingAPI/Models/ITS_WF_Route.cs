namespace InvoiceTrackingAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ITS_WF_Route
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ITS_WF_Route()
        {
            ITS_WF_Process = new HashSet<ITS_WF_Process>();
        }

        public int ID { get; set; }

        public int RoleFrom { get; set; }

        public int RoleTo { get; set; }

        public int Action { get; set; }

        public int WFStatusToID { get; set; }

        public bool Enabled { get; set; }

        [Required]
        [StringLength(50)]
        public string UserCreated { get; set; }

        public int UserUpdated { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        public virtual ITS_Role ITS_Role { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ITS_WF_Process> ITS_WF_Process { get; set; }

        public virtual ITS_WF_Status ITS_WF_Status { get; set; }

        public virtual ITS_WF_RouteActions ITS_WF_RouteActions { get; set; }
    }
}
