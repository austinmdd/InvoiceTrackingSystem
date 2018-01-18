namespace InvoiceTrackingAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ITS_WF_Process
    {
        public int ID { get; set; }

        public int SubmissionID { get; set; }

        public int SupplierInvoiceID { get; set; }

        public int RoleID { get; set; }

        public int UserID { get; set; }

        public int WF_StatusID { get; set; }

        public DateTime DateAssigned { get; set; }

        public DateTime DateCompleted { get; set; }

        public int RouteID { get; set; }

        public bool Status { get; set; }

        public virtual ITS_Role ITS_Role { get; set; }

        public virtual ITS_Submission ITS_Submission { get; set; }

        public virtual ITS_SupplierInvoice ITS_SupplierInvoice { get; set; }

        public virtual ITS_User ITS_User { get; set; }

        public virtual ITS_WF_Route ITS_WF_Route { get; set; }

        public virtual ITS_WF_Status ITS_WF_Status { get; set; }
    }
}
