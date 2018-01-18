using InvoiceTrackingAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceTrackingAPI.DTO.Search
{
    public class SupplierDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string RegNumber { get; set; }
        public bool TaxCompliant { get; set; }
        public string CSDNumber { get; set; }
        public string VendorCode { get; set; }
        public string VendorPortalID { get; set; }
        public List<PODTO> PurchaseOrders = new List<PODTO>();
        public SupplierDTO(ITS_Supplier supplier,bool AddExtra=true)
        {
            ID = supplier.ID;
            Name = supplier.Name;
            RegNumber = supplier.RegNumber;
            TaxCompliant = supplier.TaxCompliant;
            CSDNumber = supplier.CSDNumber;
            VendorCode = supplier.VendorCode;
            VendorPortalID = supplier.VendorPortalID;
            if (AddExtra)
            {
                foreach (ITS_PurchaseOrder PO in supplier.ITS_PurchaseOrder.ToList())
                {
                    PurchaseOrders.Add(new PODTO(PO));
                } 
            }

        }
        public SupplierDTO()
        {

        }
    }
}