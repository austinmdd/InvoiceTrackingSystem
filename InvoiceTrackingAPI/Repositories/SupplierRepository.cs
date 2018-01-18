using InvoiceTrackingAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

namespace InvoiceTrackingAPI.Repositories
{

    public class SupplierRepository : BaseRepository<ITS_Supplier>
    {
        
        public ITS_Supplier FindByPO(string PO)
        {
            return this.DbSet.AsQueryable<ITS_Supplier>().Where(supp => supp.ITS_PurchaseOrder.Any(po => po.PONumber.ToLower() == PO.ToLower())).FirstOrDefault();           
        }
        public List<ITS_Supplier> FindByName(string Name) {
            return this.DbSet.AsQueryable<ITS_Supplier>().Where(supp => supp.Name.ToLower().Contains(Name.ToLower())).ToList();

        }

        public List<ITS_Supplier> FindByNamePaging(int PageNum, int PageSize, string Name, ref int Total) {
            var result = this.DbSet.AsQueryable<ITS_Supplier>().Where(supp => supp.Name.ToLower().Contains(Name.ToLower()));
            Total = result.Count();

            return result.OrderBy(e=>e.ID).Skip((PageNum - 1) * PageSize).Take(PageSize).ToList();
        }
       
    }
}