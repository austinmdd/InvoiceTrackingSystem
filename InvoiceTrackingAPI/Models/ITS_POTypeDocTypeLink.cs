namespace InvoiceTrackingAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ITS_POTypeDocTypeLink
    {
        public int ID { get; set; }

        public int DocumentTypeID { get; set; }

        public int POTypeID { get; set; }

        public bool MandatoryYN { get; set; }

        public DateTime DateCreated { get; set; }

        [Required]
        [StringLength(50)]
        public string UserCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        public int UserUpdated { get; set; }

        public virtual ITS_DocumentType ITS_DocumentType { get; set; }

        public virtual ITS_POType ITS_POType { get; set; }
    }
}
