namespace InvoiceTrackingAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ITS_WF_RuleRoleLink
    {
        public int ID { get; set; }

        public int RuleID { get; set; }

        public int RoleID { get; set; }

        [Required]
        [StringLength(50)]
        public string UserCreated { get; set; }

        public int UserUpdated { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        public virtual ITS_Role ITS_Role { get; set; }

        public virtual ITS_WF_Checklist ITS_WF_Checklist { get; set; }
    }
}
