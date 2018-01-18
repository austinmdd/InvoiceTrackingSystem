using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InvoiceTrackingAPI.Repositories;
using InvoiceTrackingAPI.Models;

namespace InvoiceTrackingAPI.Services
{
    public class WorflowRuleService : WorkflowRuleRepository
    {
        public List<ITS_WF_Checklist> GetSearchedRules(int PageNum, int PageSize, string Searchtext)
        {
            return GetAll().Where(ser => ser.ChecklistName.ToLower().Trim().Contains(Searchtext.ToLower().Trim()) || ser.Enabled.ToString().ToLower().Trim().Contains(Searchtext.ToLower().Trim()) || ser.UserCreated.ToString().ToLower().Trim().Contains(Searchtext.ToLower().Trim())|| ser.DateCreated.ToString().ToLower().Trim().Contains(Searchtext.ToLower().Trim())).ToList();//OrderBy;
        }

    }
}