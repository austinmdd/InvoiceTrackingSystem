using InvoiceTrackingAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceTrackingAPI.Repositories
{
    public class InvoiceCategoriesRepository : BaseRepository<ITS_InvoiceCategories>
    {
        public ITS_InvoiceCategories FindByType(string Type)
        {
            return GetAll().Where(dat => dat.InvoiceCategory.ToLower().Trim().Equals(Type.ToLower().Trim())).FirstOrDefault();
        }

        public List<ITS_InvoiceCategories> GetAllFixed(int page, int pageSize)
        {
            return GetAll().OrderBy(e => e.ID).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}