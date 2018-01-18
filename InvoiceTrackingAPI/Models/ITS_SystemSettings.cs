namespace InvoiceTrackingAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ITS_SystemSettings
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Key { get; set; }

        [Required]
        public string Value { get; set; }

        public DateTime DateCreated { get; set; }

        [Required]
        [StringLength(50)]
        public string UserCreated { get; set; }

        public int UserUpdated { get; set; }

        public DateTime DateUpdated { get; set; }
    }
}
