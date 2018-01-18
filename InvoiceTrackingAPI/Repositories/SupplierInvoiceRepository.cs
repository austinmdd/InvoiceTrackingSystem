using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InvoiceTrackingAPI.Models;

namespace InvoiceTrackingAPI.Repositories
{
    public class SupplierInvoiceRepository : BaseRepository<ITS_SupplierInvoice>
    {
        enum FLAG
        {
            flagIN= 1, flagIA=2, flagID=3, flagIS=4, flagPO=5, flagSN= 6 
        }
        public ITS_SupplierInvoice FindByInvoiceNumber(string InvoiceNumber)
        {
            return this.DbSet.AsQueryable<ITS_SupplierInvoice>().Where(sinv => sinv.InvoiceNumber.ToLower() == InvoiceNumber.ToLower()).FirstOrDefault();
        }

        public List<ITS_SupplierInvoice> FindBySupplierNamePaging(int PageNum, int PageSize, string Name, ref int Total)
        {
            var result = this.DbSet.AsQueryable<ITS_SupplierInvoice>().Where(sinv =>sinv.ITS_Submission.ITS_Supplier.Name.ToLower().Contains(Name.ToLower()));
            Total = result.Count();
            return result.OrderBy(e => e.ID).Skip((PageNum - 1) * PageSize).Take(PageSize).ToList();
        }

        public ITS_SupplierInvoice FindByType(string Type)
        {
            return GetAll().Where(diouhyoi => diouhyoi.InvoiceNumber.ToLower().Trim().Equals(Type.ToLower().Trim())).FirstOrDefault();
        }

        public List<ITS_SupplierInvoice> FindAllPaging(int PageNum, int PageSize, ref int Total, string InvType="All")
        {
            var result =(InvType.Equals("All")) ? GetAll(): this.DbSet.AsQueryable<ITS_SupplierInvoice>().Where(sinv => sinv.Status == InvType.ToLower()).ToList();
            Total = result.Count();
            return result.OrderByDescending(e => e.InvoiceDate).Skip((PageNum - 1) * PageSize).Take(PageSize).ToList();
        }

        public List<ITS_SupplierInvoice> FindAllSortPaging(int PageNum, int PageSize, ref int Total, string InvType = "All",int flag=3,bool order=true)
        {           
            var result = (InvType.Equals("All")) ? GetAll() : this.DbSet.AsQueryable<ITS_SupplierInvoice>().Where(sinv => sinv.Status == InvType.ToLower()).ToList();
            Total = result.Count();
            switch (flag)
            {
                case (int)FLAG.flagIA:
                    result = (order) ? result.OrderByDescending(e => e.InvoiceAmount).Skip((PageNum - 1) * PageSize).Take(PageSize).ToList() : result.OrderBy(e => e.InvoiceAmount).Skip((PageNum - 1) * PageSize).Take(PageSize).ToList();
                    break;
                case (int)FLAG.flagIN:
                    result = (order) ? result.OrderByDescending(e => e.InvoiceNumber).Skip((PageNum - 1) * PageSize).Take(PageSize).ToList() : result.OrderBy(e => e.InvoiceNumber).Skip((PageNum - 1) * PageSize).Take(PageSize).ToList();
                    break;
                case (int)FLAG.flagIS:
                    result = (order) ? result.OrderByDescending(e => e.Status).Skip((PageNum - 1) * PageSize).Take(PageSize).ToList() : result.OrderBy(e => e.Status).Skip((PageNum - 1) * PageSize).Take(PageSize).ToList();
                    break;
                case (int)FLAG.flagPO:
                    result = (order) ? result.OrderByDescending(e => e.ITS_Submission.ITS_PurchaseOrder.PONumber).Skip((PageNum - 1) * PageSize).Take(PageSize).ToList() : result.OrderBy(e => e.ITS_Submission.ITS_PurchaseOrder.PONumber).Skip((PageNum - 1) * PageSize).Take(PageSize).ToList();
                    break;
                case (int)FLAG.flagSN:
                    result = (order) ? result.OrderByDescending(e => e.ITS_Submission.ITS_Supplier.Name).Skip((PageNum - 1) * PageSize).Take(PageSize).ToList() : result.OrderBy(e => e.ITS_Submission.ITS_Supplier.Name).Skip((PageNum - 1) * PageSize).Take(PageSize).ToList();
                    break;
                default:
                    result = (order) ? result.OrderByDescending(e => e.InvoiceDate).Skip((PageNum - 1) * PageSize).Take(PageSize).ToList() : result.OrderBy(e => e.InvoiceDate).Skip((PageNum - 1) * PageSize).Take(PageSize).ToList();
                    break;
            }
            return result;           
        }

        public List<ITS_SupplierInvoice> FindInvoicesForSubId(int PageNum, int PageSize,int Subid, ref int Total, string InvType = "All", int flag = 3, bool order = true)
        {
           // var result = (InvType.Equals("All")) ? GetAll() : this.DbSet.AsQueryable<ITS_SupplierInvoice>().Where(sinv => sinv.Status == InvType.ToLower()).ToList();
            var result = (InvType.Equals("All")) ? GetAll() : this.DbSet.AsQueryable<ITS_SupplierInvoice>().Where(sinv => sinv.SubmissionID == Subid).ToList();
            Total = result.Count();
            switch (flag)
            {
                case (int)FLAG.flagIA:
                    result = (order) ? result.OrderByDescending(e => e.InvoiceAmount).Skip((PageNum - 1) * PageSize).Take(PageSize).ToList() : result.OrderBy(e => e.InvoiceAmount).Skip((PageNum - 1) * PageSize).Take(PageSize).ToList();
                    break;
                case (int)FLAG.flagIN:
                    result = (order) ? result.OrderByDescending(e => e.InvoiceNumber).Skip((PageNum - 1) * PageSize).Take(PageSize).ToList() : result.OrderBy(e => e.InvoiceNumber).Skip((PageNum - 1) * PageSize).Take(PageSize).ToList();
                    break;
                case (int)FLAG.flagIS:
                    result = (order) ? result.OrderByDescending(e => e.Status).Skip((PageNum - 1) * PageSize).Take(PageSize).ToList() : result.OrderBy(e => e.Status).Skip((PageNum - 1) * PageSize).Take(PageSize).ToList();
                    break;
                case (int)FLAG.flagPO:
                    result = (order) ? result.OrderByDescending(e => e.ITS_Submission.ITS_PurchaseOrder.PONumber).Skip((PageNum - 1) * PageSize).Take(PageSize).ToList() : result.OrderBy(e => e.ITS_Submission.ITS_PurchaseOrder.PONumber).Skip((PageNum - 1) * PageSize).Take(PageSize).ToList();
                    break;
                case (int)FLAG.flagSN:
                    result = (order) ? result.OrderByDescending(e => e.ITS_Submission.ITS_Supplier.Name).Skip((PageNum - 1) * PageSize).Take(PageSize).ToList() : result.OrderBy(e => e.ITS_Submission.ITS_Supplier.Name).Skip((PageNum - 1) * PageSize).Take(PageSize).ToList();
                    break;
               
                default:
                    result = (order) ? result.OrderByDescending(e => e.InvoiceDate).Skip((PageNum - 1) * PageSize).Take(PageSize).ToList() : result.OrderBy(e => e.InvoiceDate).Skip((PageNum - 1) * PageSize).Take(PageSize).ToList();
                    break;
            }
            return result;

        }

        public List<ITS_SupplierInvoice>FindBySubID(int Subid, int supInvID, ref int Total)
        {
            // var result = (InvType.Equals("All")) ? GetAll() : this.DbSet.AsQueryable<ITS_SupplierInvoice>().Where(sinv => sinv.Status == InvType.ToLower()).ToList();
            var result = this.DbSet.AsQueryable<ITS_SupplierInvoice>().Where(sinv => sinv.SubmissionID == Subid && sinv.ID == supInvID).ToList();
            Total = result.Count();
            return result.OrderBy(e => e.ID).ToList();
        }

    }
}