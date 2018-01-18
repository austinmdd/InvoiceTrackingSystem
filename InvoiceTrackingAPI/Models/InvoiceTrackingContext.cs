namespace InvoiceTrackingAPI.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class InvoiceTrackingContext : DbContext
    {
        public InvoiceTrackingContext()
            : base("name=InvoiceTrackingContext")
        {
        }

        public virtual DbSet<ITS_Document> ITS_Document { get; set; }
        public virtual DbSet<ITS_DocumentType> ITS_DocumentType { get; set; }
        public virtual DbSet<ITS_DocumentTypeInvoiceCategoriesLinks> ITS_DocumentTypeInvoiceCategoriesLinks { get; set; }
        public virtual DbSet<ITS_InvoiceCategories> ITS_InvoiceCategories { get; set; }
        public virtual DbSet<ITS_Notes> ITS_Notes { get; set; }
        public virtual DbSet<ITS_POType> ITS_POType { get; set; }
        public virtual DbSet<ITS_POTypeDocTypeLink> ITS_POTypeDocTypeLink { get; set; }
        public virtual DbSet<ITS_PurchaseOrder> ITS_PurchaseOrder { get; set; }
        public virtual DbSet<ITS_Role> ITS_Role { get; set; }
        public virtual DbSet<ITS_Status> ITS_Status { get; set; }
        public virtual DbSet<ITS_Submission> ITS_Submission { get; set; }
        public virtual DbSet<ITS_Supplier> ITS_Supplier { get; set; }
        public virtual DbSet<ITS_SupplierInvoice> ITS_SupplierInvoice { get; set; }
        public virtual DbSet<ITS_SystemSettings> ITS_SystemSettings { get; set; }
        public virtual DbSet<ITS_User> ITS_User { get; set; }
        public virtual DbSet<ITS_WF_Checklist> ITS_WF_Checklist { get; set; }
        public virtual DbSet<ITS_WF_Process> ITS_WF_Process { get; set; }
        public virtual DbSet<ITS_WF_Route> ITS_WF_Route { get; set; }
        public virtual DbSet<ITS_WF_RouteActions> ITS_WF_RouteActions { get; set; }
        public virtual DbSet<ITS_WF_RuleRoleLink> ITS_WF_RuleRoleLink { get; set; }
        public virtual DbSet<ITS_WF_Status> ITS_WF_Status { get; set; }
        public virtual DbSet<ITS_WF_UserRoleLink> ITS_WF_UserRoleLink { get; set; }
        public virtual DbSet<ViewRoutes> ViewRoutes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ITS_Document>()
                .Property(e => e.DocumentName)
                .IsUnicode(false);

            modelBuilder.Entity<ITS_Document>()
                .Property(e => e.DocumentType)
                .IsUnicode(false);

            modelBuilder.Entity<ITS_Document>()
                .Property(e => e.DocumentSize)
                .IsUnicode(false);

            modelBuilder.Entity<ITS_Document>()
                .Property(e => e.EDMSID)
                .IsUnicode(false);

            modelBuilder.Entity<ITS_Document>()
                .Property(e => e.EDMSLocation)
                .IsUnicode(false);

            modelBuilder.Entity<ITS_Document>()
                .Property(e => e.EDMSName)
                .IsUnicode(false);

            modelBuilder.Entity<ITS_Document>()
                .Property(e => e.EDMSLink)
                .IsUnicode(false);

            modelBuilder.Entity<ITS_Document>()
                .Property(e => e.UserCreated)
                .IsUnicode(false);

            modelBuilder.Entity<ITS_DocumentType>()
                .Property(e => e.DocumentType)
                .IsUnicode(false);

            modelBuilder.Entity<ITS_DocumentType>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<ITS_DocumentType>()
                .Property(e => e.UserCreated)
                .IsUnicode(false);

            modelBuilder.Entity<ITS_DocumentType>()
                .HasMany(e => e.ITS_Document)
                .WithRequired(e => e.ITS_DocumentType)
                .HasForeignKey(e => e.DocumentTypeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ITS_DocumentType>()
                .HasMany(e => e.ITS_DocumentTypeInvoiceCategoriesLinks)
                .WithRequired(e => e.ITS_DocumentType)
                .HasForeignKey(e => e.DocumentTypeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ITS_DocumentType>()
                .HasMany(e => e.ITS_POTypeDocTypeLink)
                .WithRequired(e => e.ITS_DocumentType)
                .HasForeignKey(e => e.DocumentTypeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ITS_DocumentTypeInvoiceCategoriesLinks>()
                .Property(e => e.UserCreated)
                .IsUnicode(false);

            modelBuilder.Entity<ITS_InvoiceCategories>()
                .Property(e => e.InvoiceCategory)
                .IsUnicode(false);

            modelBuilder.Entity<ITS_InvoiceCategories>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<ITS_InvoiceCategories>()
                .Property(e => e.UserCreated)
                .IsUnicode(false);

            modelBuilder.Entity<ITS_InvoiceCategories>()
                .HasMany(e => e.ITS_DocumentTypeInvoiceCategoriesLinks)
                .WithRequired(e => e.ITS_InvoiceCategories)
                .HasForeignKey(e => e.InvoiceCategoryID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ITS_Notes>()
                .Property(e => e.UserCreated)
                .IsUnicode(false);

            modelBuilder.Entity<ITS_POType>()
                .Property(e => e.POType)
                .IsUnicode(false);

            modelBuilder.Entity<ITS_POType>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<ITS_POType>()
                .Property(e => e.UserCreated)
                .IsUnicode(false);

            modelBuilder.Entity<ITS_POType>()
                .HasMany(e => e.ITS_PurchaseOrder)
                .WithRequired(e => e.ITS_POType)
                .HasForeignKey(e => e.POTypeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ITS_POType>()
                .HasMany(e => e.ITS_POTypeDocTypeLink)
                .WithRequired(e => e.ITS_POType)
                .HasForeignKey(e => e.POTypeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ITS_POTypeDocTypeLink>()
                .Property(e => e.UserCreated)
                .IsUnicode(false);

            modelBuilder.Entity<ITS_PurchaseOrder>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<ITS_PurchaseOrder>()
                .Property(e => e.PONumber)
                .IsUnicode(false);

            modelBuilder.Entity<ITS_PurchaseOrder>()
                .Property(e => e.POAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<ITS_PurchaseOrder>()
                .Property(e => e.PODueAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<ITS_PurchaseOrder>()
                .Property(e => e.UserCreated)
                .IsUnicode(false);

            modelBuilder.Entity<ITS_PurchaseOrder>()
                .HasMany(e => e.ITS_Submission)
                .WithRequired(e => e.ITS_PurchaseOrder)
                .HasForeignKey(e => e.PurchaseOrderID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ITS_Role>()
                .Property(e => e.Role)
                .IsUnicode(false);

            modelBuilder.Entity<ITS_Role>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<ITS_Role>()
                .Property(e => e.UserCreated)
                .IsUnicode(false);

            modelBuilder.Entity<ITS_Role>()
                .HasMany(e => e.ITS_WF_Route)
                .WithRequired(e => e.ITS_Role)
                .HasForeignKey(e => e.RoleFrom)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ITS_Role>()
                .HasMany(e => e.ITS_WF_RuleRoleLink)
                .WithRequired(e => e.ITS_Role)
                .HasForeignKey(e => e.RoleID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ITS_Role>()
                .HasMany(e => e.ITS_WF_UserRoleLink)
                .WithRequired(e => e.ITS_Role)
                .HasForeignKey(e => e.RoleID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ITS_Role>()
                .HasMany(e => e.ITS_WF_Process)
                .WithRequired(e => e.ITS_Role)
                .HasForeignKey(e => e.RoleID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ITS_Status>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<ITS_Status>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<ITS_Status>()
                .Property(e => e.UserCreated)
                .IsUnicode(false);

            modelBuilder.Entity<ITS_Submission>()
                .Property(e => e.SubmissionNumber)
                .IsUnicode(false);

            modelBuilder.Entity<ITS_Submission>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<ITS_Submission>()
                .Property(e => e.PONumber)
                .IsUnicode(false);

            modelBuilder.Entity<ITS_Submission>()
                .Property(e => e.POAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<ITS_Submission>()
                .Property(e => e.UserCreated)
                .IsUnicode(false);

            modelBuilder.Entity<ITS_Submission>()
                .HasMany(e => e.ITS_WF_Process)
                .WithRequired(e => e.ITS_Submission)
                .HasForeignKey(e => e.SubmissionID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ITS_Submission>()
                .HasMany(e => e.ITS_SupplierInvoice)
                .WithRequired(e => e.ITS_Submission)
                .HasForeignKey(e => e.SubmissionID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ITS_Supplier>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<ITS_Supplier>()
                .Property(e => e.RegNumber)
                .IsUnicode(false);

            modelBuilder.Entity<ITS_Supplier>()
                .Property(e => e.VatRegisteredYN)
                .IsUnicode(false);

            modelBuilder.Entity<ITS_Supplier>()
                .Property(e => e.CSDNumber)
                .IsUnicode(false);

            modelBuilder.Entity<ITS_Supplier>()
                .Property(e => e.VendorCode)
                .IsUnicode(false);

            modelBuilder.Entity<ITS_Supplier>()
                .Property(e => e.VendorPortalID)
                .IsUnicode(false);

            modelBuilder.Entity<ITS_Supplier>()
                .Property(e => e.EmailAddress)
                .IsUnicode(false);

            modelBuilder.Entity<ITS_Supplier>()
                .Property(e => e.UserCreated)
                .IsUnicode(false);

            modelBuilder.Entity<ITS_Supplier>()
                .HasMany(e => e.ITS_PurchaseOrder)
                .WithRequired(e => e.ITS_Supplier)
                .HasForeignKey(e => e.SupplierID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ITS_Supplier>()
                .HasMany(e => e.ITS_Submission)
                .WithRequired(e => e.ITS_Supplier)
                .HasForeignKey(e => e.SupplierID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ITS_SupplierInvoice>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<ITS_SupplierInvoice>()
                .Property(e => e.InvoiceNumber)
                .IsUnicode(false);

            modelBuilder.Entity<ITS_SupplierInvoice>()
                .Property(e => e.InvoiceAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<ITS_SupplierInvoice>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<ITS_SupplierInvoice>()
                .Property(e => e.UserCreated)
                .IsUnicode(false);

            modelBuilder.Entity<ITS_SupplierInvoice>()
                .HasMany(e => e.ITS_Document)
                .WithRequired(e => e.ITS_SupplierInvoice)
                .HasForeignKey(e => e.SupplierInvoiceID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ITS_SupplierInvoice>()
                .HasMany(e => e.ITS_Notes)
                .WithRequired(e => e.ITS_SupplierInvoice)
                .HasForeignKey(e => e.SupplierInvoiceID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ITS_SupplierInvoice>()
                .HasMany(e => e.ITS_WF_Process)
                .WithRequired(e => e.ITS_SupplierInvoice)
                .HasForeignKey(e => e.SupplierInvoiceID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ITS_SystemSettings>()
                .Property(e => e.Key)
                .IsUnicode(false);

            modelBuilder.Entity<ITS_SystemSettings>()
                .Property(e => e.Value)
                .IsUnicode(false);

            modelBuilder.Entity<ITS_SystemSettings>()
                .Property(e => e.UserCreated)
                .IsUnicode(false);

            modelBuilder.Entity<ITS_User>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<ITS_User>()
                .Property(e => e.UserCreated)
                .IsUnicode(false);

            modelBuilder.Entity<ITS_User>()
                .HasMany(e => e.ITS_WF_UserRoleLink)
                .WithRequired(e => e.ITS_User)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ITS_User>()
                .HasMany(e => e.ITS_WF_Process)
                .WithRequired(e => e.ITS_User)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ITS_WF_Checklist>()
                .Property(e => e.ChecklistName)
                .IsUnicode(false);

            modelBuilder.Entity<ITS_WF_Checklist>()
                .Property(e => e.UserCreated)
                .IsUnicode(false);

            modelBuilder.Entity<ITS_WF_Checklist>()
                .HasMany(e => e.ITS_WF_RuleRoleLink)
                .WithRequired(e => e.ITS_WF_Checklist)
                .HasForeignKey(e => e.RuleID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ITS_WF_Route>()
                .Property(e => e.UserCreated)
                .IsUnicode(false);

            modelBuilder.Entity<ITS_WF_Route>()
                .HasMany(e => e.ITS_WF_Process)
                .WithRequired(e => e.ITS_WF_Route)
                .HasForeignKey(e => e.RouteID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ITS_WF_RouteActions>()
                .Property(e => e.RouteName)
                .IsUnicode(false);

            modelBuilder.Entity<ITS_WF_RouteActions>()
                .Property(e => e.UserCreated)
                .IsUnicode(false);

            modelBuilder.Entity<ITS_WF_RouteActions>()
                .HasMany(e => e.ITS_WF_Route)
                .WithRequired(e => e.ITS_WF_RouteActions)
                .HasForeignKey(e => e.Action)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ITS_WF_RuleRoleLink>()
                .Property(e => e.UserCreated)
                .IsUnicode(false);

            modelBuilder.Entity<ITS_WF_Status>()
                .Property(e => e.WorkflowStatus)
                .IsUnicode(false);

            modelBuilder.Entity<ITS_WF_Status>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<ITS_WF_Status>()
                .Property(e => e.UserCreated)
                .IsUnicode(false);

            modelBuilder.Entity<ITS_WF_Status>()
                .HasMany(e => e.ITS_WF_Process)
                .WithRequired(e => e.ITS_WF_Status)
                .HasForeignKey(e => e.WF_StatusID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ITS_WF_Status>()
                .HasMany(e => e.ITS_WF_Route)
                .WithRequired(e => e.ITS_WF_Status)
                .HasForeignKey(e => e.WFStatusToID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ITS_WF_UserRoleLink>()
                .Property(e => e.UserCreated)
                .IsUnicode(false);

            modelBuilder.Entity<ViewRoutes>()
                .Property(e => e.RoleFromName)
                .IsUnicode(false);

            modelBuilder.Entity<ViewRoutes>()
                .Property(e => e.RoleToName)
                .IsUnicode(false);

            modelBuilder.Entity<ViewRoutes>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<ViewRoutes>()
                .Property(e => e.UserCreated)
                .IsUnicode(false);

            modelBuilder.Entity<ViewRoutes>()
                .Property(e => e.RouteName)
                .IsUnicode(false);
        }
    }
}
