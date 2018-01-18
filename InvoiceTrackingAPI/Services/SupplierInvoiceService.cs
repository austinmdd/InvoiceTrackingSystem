using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InvoiceTrackingAPI.Repositories;
using InvoiceTrackingAPI.Models;
using System.Collections;
using InvoiceTrackingAPI.DTO;
using InvoiceTrackingAPI.DTO.Search;

namespace InvoiceTrackingAPI.Services
{
    public class SupplierInvoiceService : SupplierInvoiceRepository
    {

        public int  SaveInvoices(SupplierInvoiceSubmissionDTO entity, int submissionid)
        {
            ITS_SupplierInvoice invoice = new ITS_SupplierInvoice();
          

                    invoice.SubmissionID = submissionid;
                    invoice.Status = EnumService.SubmissionStatus.Saved.ToString();
                    invoice.InvoiceNumber = entity.InvoiceNumber;
                    invoice.InvoiceAmount = entity.InvoiceAmount;
                    invoice.InvoiceDate = Convert.ToDateTime(entity.InvoiceDate);
                    invoice.Description = entity.Description;
                    invoice.DateCreated = System.DateTime.Now;
                    invoice.UserCreated = "Bongani";//Get user from session
                    invoice.UserUpdated = 1; //Get UserID
                    invoice.DateUpdated = System.DateTime.Now;


            var subInvoice = Add(invoice);
                    SaveChanges();

                    return subInvoice.ID;
        }

        public string DeriveStatus(decimal InvoiceAmount, decimal POAmount)
        {
            string status;


             if (InvoiceAmount < POAmount)
                {
                    return status = EnumService.POStatus.Partly_Paid.ToString();
                   
                }
                else
                {
                    return status = EnumService.POStatus.Paid.ToString();
                   
                }
        }

        public decimal CalculateAmountsDue(ITS_PurchaseOrder PO)
        {
            decimal duetotal = 0;
            decimal invamount = 0;
            SubmissionService subService = new SubmissionService();

            var submission =  subService.FindByPO(PO.ID);

            if (submission != null)
            {
                List<ITS_SupplierInvoice> invObj = new List<ITS_SupplierInvoice>();
                invObj = GetAll().Where(s => s.SubmissionID == submission.ID).ToList();

                foreach (var inv in invObj)
                {
                    invamount = invamount + inv.InvoiceAmount;
                }
            }
            duetotal = PO.POAmount - invamount;

            return duetotal;
        }

        //public ITS_SupplierInvoice FindBySubmission(int SubmissionID)
        //{
        //    return GetAll().Where(s => s.SubmissionID == SubmissionID).FirstOrDefault();
        //}
        public SupplierInvoiceDTO GetInvoiceNumber(string InvNumO)
        {
            var INV = FindByInvoiceNumber(InvNumO);
            return (INV != null) ? new SupplierInvoiceDTO(INV, true) : null;
        }

        public GenericSearchDTO<ITS_SupplierInvoice, SupplierInvoiceDTO> GetBySupplierNamePaging(int PageNum, int PageSize, string Name)
        {            
            return GetPaging(PageNum, PageSize, Name);
        }

        public GenericSearchDTO<ITS_SupplierInvoice, SupplierInvoiceDTO> GetPaging(int PageNum, int PageSize, string Name,bool isAll=false)
        {
            int Count = 0;
            var invoices = (isAll)?FindAllPaging(PageNum, PageSize, ref Count, Name) : FindBySupplierNamePaging(PageNum, PageSize, Name, ref Count);            
            if (invoices.Count != 0)
            {
                return new GenericSearchDTO<ITS_SupplierInvoice, SupplierInvoiceDTO>(invoices, Count);
            }
            return null;
        }

       public  GenericSearchDTO<ITS_SupplierInvoice, SupplierInvoiceDTO> FindAllPaging(int PageNum, int PageSize, string Name) {
            return GetPaging(PageNum,PageSize,Name, true);
        }
       

        public GenericSearchDTO<ITS_SupplierInvoice, SupplierInvoiceDTO> GetAllSortPaging(int PageNum, int PageSize, string InvType = "All", int flag = 3, bool order = true)
        {
            int Count = 0;
            var invoices = FindAllSortPaging(PageNum, PageSize, ref Count, InvType, flag,order);
            if (invoices.Count != 0)
            {
                return new GenericSearchDTO<ITS_SupplierInvoice, SupplierInvoiceDTO>(invoices, Count);
            }
            return null;
        }

        public GenericSearchDTO<ITS_SupplierInvoice,SupplierInvoiceDTO> GetInvoicesForSubId(int PageNum, int PageSize,int Subid, string InvType = "All", int flag = 3, bool order = true)
        {
            int Count = 0;
            var invoices = FindInvoicesForSubId(PageNum, PageSize, Subid, ref Count, InvType, flag, order);

            if(invoices.Count != 0)
            {
                return new GenericSearchDTO<ITS_SupplierInvoice, SupplierInvoiceDTO>(invoices, Count);
            }
            return null;
        }










        public GenericSearchDTO<ITS_SupplierInvoice, SupplierInvoiceDTO> GetInvoicesForSubIdService(int Subid, int supInvID)
        {
            int Count = 0;
            var invoices = FindBySubID(Subid, supInvID, ref Count);

            if (invoices.Count != 0)
            {
                return new GenericSearchDTO<ITS_SupplierInvoice, SupplierInvoiceDTO>(invoices, Count);
            }
            return null;
        }

    }
}