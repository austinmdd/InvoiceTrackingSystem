using InvoiceTrackingAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceTrackingAPI.DTO.Search
{
    public class PODTO
    {
        public int ID { get; set; }
        public string PONumber { get; set; }
        public DateTime PODate { get; set; }
        public decimal POAmount { get; set; }
        public string Status { get; set; }
        public decimal InvoicedAmount { get; set; }
        public decimal AmountOutstanding { get; set; }

        public PODTO(ITS_PurchaseOrder PO)
        {
            ID = PO.ID;
            PONumber = PO.PONumber;
            POAmount = PO.POAmount;
            PODate = PO.PODate;
            POAmount = PO.POAmount;
            Status = PO.Status;
            foreach (ITS_Submission sub in PO.ITS_Submission.ToList())
            {
                foreach (ITS_SupplierInvoice inv in sub.ITS_SupplierInvoice.ToList())
                {
                    InvoicedAmount += inv.InvoiceAmount;
                }

            }

            AmountOutstanding = POAmount - InvoicedAmount;

        }
    }
}