namespace InvoiceTrackingAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ITS_WF_Checklist
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ITS_WF_Checklist()
        {
            ITS_WF_RuleRoleLink = new HashSet<ITS_WF_RuleRoleLink>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string ChecklistName { get; set; }

        public bool Enabled { get; set; }

        public int UserUpdated { get; set; }

        [Required]
        [StringLength(50)]
        public string UserCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        public DateTime DateCreated { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ITS_WF_RuleRoleLink> ITS_WF_RuleRoleLink { get; set; }
    }
}
