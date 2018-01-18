using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InvoiceTrackingAPI.Models;

namespace InvoiceTrackingAPI.Repositories
{
    public class RuleRoleLinkRepository : BaseRepository<ITS_WF_RuleRoleLink>
    {
        public List<ITS_WF_RuleRoleLink> GetAllByRuleID(int id)
        {
            return this.DbSet.AsQueryable<ITS_WF_RuleRoleLink>().Where(link => link.RoleID == id).ToList();
        }

        public ITS_WF_RuleRoleLink GetbyRuleId(int id)
        {
            return GetAll().Where(dat => dat.RuleID == id).FirstOrDefault();

        }

    }


}