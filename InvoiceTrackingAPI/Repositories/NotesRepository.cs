using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InvoiceTrackingAPI.Models;

namespace InvoiceTrackingAPI.Repositories
{
    public class NotesRepository : BaseRepository<ITS_Notes>
    {
        public List<ITS_Notes> GetAllNotesByID(int id)
        {
            return this.DbSet.AsQueryable<ITS_Notes>().Where(inv => inv.SupplierInvoiceID == id).ToList();
        }
    }
}