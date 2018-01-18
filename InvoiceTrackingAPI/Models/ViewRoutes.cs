namespace InvoiceTrackingAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ViewRoutes
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RoleFrom { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string RoleFromName { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RoleTo { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(50)]
        public string RoleToName { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int WFStatusToID { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(50)]
        public string Description { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Action { get; set; }

        [Key]
        [Column(Order = 8)]
        public bool Enabled { get; set; }

        [Key]
        [Column(Order = 9)]
        public DateTime DateCreated { get; set; }

        [Key]
        [Column(Order = 10)]
        [StringLength(50)]
        public string UserCreated { get; set; }

        [Key]
        [Column(Order = 11)]
        public DateTime DateUpdated { get; set; }

        [Key]
        [Column(Order = 12)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserUpdated { get; set; }

        [Key]
        [Column(Order = 13)]
        [StringLength(50)]
        public string RouteName { get; set; }
    }
}
