using InvoiceTrackingAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceTrackingAPI.Repositories
{
    public class DocumentTypeRepository: BaseRepository<ITS_DocumentType>
    {
        public ITS_DocumentType FindByType(string Type)
        {
            return GetAll().Where(doc => doc.DocumentType.ToLower().Trim().Equals(Type.ToLower().Trim())).FirstOrDefault();
        }

        public List<ITS_DocumentType> GetInvoiceCatUnlinkedDocTypes(int invCatId)
        {
            return GetAll().Except(GetInvoiceCatLinkedDocTypes(invCatId)).ToList();
        }

        public List<ITS_DocumentType> GetInvoiceCatLinkedDocTypes(int invCatId)
        {
            return this.DbSet.AsQueryable<ITS_DocumentType>().Where(doctype => doctype.ITS_DocumentTypeInvoiceCategoriesLinks.Any(link => link.InvoiceCategoryID == invCatId)).ToList();
        }



        public List<ITS_DocumentType> GetAllFixed(int page, int pageSize)
        {
            return GetAll().OrderBy(e => e.ID).Skip((page - 1) * pageSize).Take(pageSize).ToList();

        }
    }

}