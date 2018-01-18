using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InvoiceTrackingAPI.Models;

namespace InvoiceTrackingAPI.Repositories
{
    public class WorkflowProcessRepository : BaseRepository<ITS_WF_Process>
    {
        public ITS_WF_Process FindByType(string Type)
        {
            return GetAll().Where(dat => dat.SubmissionID.Equals(Type.ToLower().Trim())).FirstOrDefault();
        }

        public List<ITS_WF_Process> GetAllFixed(int page, int pageSize)
        {
            
            return GetAll().OrderBy(e => e.ID).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
        public List<ITS_WF_Process> GetDepartmentWorkflow(int page, int pageSize, int roleID)
        {           
            return GetAll().Where(dat => dat.RoleID == roleID).OrderBy(e => e.ID).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            //return GetAll().OrderBy(e => e.ID).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            //return GetAll().Where(dat => dat.WF_StatusID.Equals(wkflowstatusid)).FirstOrDefault();
        }


    }
}