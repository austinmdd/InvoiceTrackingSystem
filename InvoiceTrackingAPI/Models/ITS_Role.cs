namespace InvoiceTrackingAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ITS_Role
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ITS_Role()
        {
            ITS_WF_Route = new HashSet<ITS_WF_Route>();
            ITS_WF_RuleRoleLink = new HashSet<ITS_WF_RuleRoleLink>();
            ITS_WF_UserRoleLink = new HashSet<ITS_WF_UserRoleLink>();
            ITS_WF_Process = new HashSet<ITS_WF_Process>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Role { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        public bool Enabled { get; set; }

        public bool IsWorkflowRole { get; set; }

        public int UserUpdated { get; set; }

        [Required]
        [StringLength(50)]
        public string UserCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        public DateTime DateCreated { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ITS_WF_Route> ITS_WF_Route { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ITS_WF_RuleRoleLink> ITS_WF_RuleRoleLink { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ITS_WF_UserRoleLink> ITS_WF_UserRoleLink { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ITS_WF_Process> ITS_WF_Process { get; set; }
    }
}
