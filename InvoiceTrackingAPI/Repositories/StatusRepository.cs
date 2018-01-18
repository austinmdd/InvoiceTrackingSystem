using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InvoiceTrackingAPI.Models;

namespace InvoiceTrackingAPI.Repositories
{
    public class StatusRepository:BaseRepository<ITS_Status>
    {
        public ITS_Status FindByStatus(string Status)
        {
            return GetAll().Where(status => status.Status.ToLower().Trim().Equals(Status.ToLower().Trim())).FirstOrDefault();
        }
    }
}