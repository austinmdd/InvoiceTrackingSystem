using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InvoiceTrackingAPI.Models;

namespace InvoiceTrackingAPI.Repositories
{
    public class WorkflowStatusRepository : BaseRepository<ITS_WF_Status>
    {
        public ITS_WF_Status FindByType(string Type)
        {
            return GetAll().Where(dat => dat.WorkflowStatus.ToLower().Trim().Equals(Type.ToLower().Trim())).FirstOrDefault();
        }

        public List<ITS_WF_Status> GetAllFixed(int page, int pageSize)
        {
            return GetAll().OrderBy(e => e.ID).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

    }
}