using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InvoiceTrackingAPI.Models;

namespace InvoiceTrackingAPI.Repositories
{
    public class WorkflowRuleRepository : BaseRepository<ITS_WF_Checklist>
    {
        public ITS_WF_Checklist FindByType(string Type)
        {
            //return GetAll().Where(dat => dat.InvoiceCategory.ToLower().Trim().Equals(Type.ToLower().Trim())).FirstOrDefault();
            return null;
        }

        public List<ITS_WF_Checklist> GetAllFixed(int page, int pageSize)
        {
            return GetAll().OrderBy(e => e.ID).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
        public ITS_WF_Checklist FindByRuleName(string rule)
        {
            return GetAll().Where(dat => dat.ChecklistName.ToLower().Trim().Equals(rule.ToLower().Trim())).FirstOrDefault();
        }

    }
}