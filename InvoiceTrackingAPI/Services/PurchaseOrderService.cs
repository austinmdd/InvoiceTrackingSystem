using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using InvoiceTrackingAPI.Models;
using InvoiceTrackingAPI.Repositories;
using InvoiceTrackingAPI.DTO;
using System.Collections;
using InvoiceTrackingAPI.DTO.Maintain;

namespace InvoiceTrackingAPI.Services
{
    public class PurchaseOrderService : PurchaseOrderRepository
    {
        SupplierInvoiceService invService = new SupplierInvoiceService();
        DocumentService docService = new DocumentService();
        InvoiceCategories invCatService = new InvoiceCategories();
        POTypeDocTypeLinkService potypeService = new POTypeDocTypeLinkService();

        public void UpdatePO(ITS_PurchaseOrder order)
        {
            order.PODueAmount = invService.CalculateAmountsDue(order);

            try
            {
                Update(order);
                SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public SupplierInvoiceSubmissionDTO MapDTO(ITS_PurchaseOrder PO)
        {
            System.Globalization.CultureInfo ITSCulture = System.Globalization.CultureInfo.CurrentUICulture;

            string DateFormat = ITSCulture.DateTimeFormat.ShortDatePattern;

            List<ITS_InvoiceCategories> lstCategories = new List<ITS_InvoiceCategories>();
            lstCategories = invCatService.GetAll();

            var Categories = new InvoiceCategoriesCountDTO(invCatService.GetAll());

            List<ITS_POTypeDocTypeLink> obj = potypeService.LinkedDocuments(PO.POTypeID);

            PODocumentTypeDTO objDocTypeDTO = new PODocumentTypeDTO();
            List<PODocumentTypeDTO> lst = new List<PODocumentTypeDTO>();

            if (obj != null)
            {

                for (int i = 0; i < obj.Count(); i++)

                    lst.Add(new PODocumentTypeDTO()
                    {
                        DocTypeID = obj[i].DocumentTypeID,
                        DocType = obj[i].ITS_DocumentType.DocumentType,
                        Mandatory = Convert.ToString(obj[i].MandatoryYN)
                    });


            }

            SupplierInvoiceSubmissionDTO sisDTO = new SupplierInvoiceSubmissionDTO();
           
            sisDTO.POID = PO.ID;
            sisDTO.POStatus = PO.Status;
            sisDTO.PONumber = PO.PONumber;
            sisDTO.POAmount = PO.POAmount;
            sisDTO.PODate = PO.DateCreated.ToString(DateFormat);
            sisDTO.DueAmount = PO.PODueAmount;

            sisDTO.Name = PO.ITS_Supplier.Name;
            sisDTO.CSDNumber = PO.ITS_Supplier.CSDNumber;
            sisDTO.VendorCode = PO.ITS_Supplier.VendorCode;

            sisDTO.LinkedDocs = lst;
            if (Categories.Categories.Count != 0)
            {
                sisDTO.InvoiceCat = Categories.Categories;
            }
            
            // sisDTO.UploadedDocs = lstUpload;

            return sisDTO;
        }

        //public ITS_SupplierInvoice GetInvoiceByPO(int POID)
        //{

        //    ITS_Submission objSubmission = subService.FindByPO(POID);

        //    ITS_SupplierInvoice objInvoice = invService.FindBySubmission(objSubmission.ID);

        //    return objInvoice;

        //}

    }
}