using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InvoiceTrackingAPI.Models;

namespace InvoiceTrackingAPI.Repositories
{
    public class RouteRepository : BaseRepository<ITS_WF_Route>
    {
        public ITS_WF_Route FindRoute(int roleFrom)
        {
            return GetAll().Where(route => route.RoleFrom.Equals(roleFrom)).FirstOrDefault();
        }

        public List<ITS_WF_Route> GetAllByRoleIDs(int id)
        {
            return this.DbSet.AsQueryable<ITS_WF_Route>().Where(route => route.RoleFrom == id).ToList();
        }
        public List<ITS_WF_Route> FindAllRoutes(int PageNum, int PageSize, ref int Total, bool order = true)
        {
            var result = GetAll().OrderBy(rut => rut.ID).Skip((PageNum - 1) * PageSize).Take(PageSize).ToList();
            Total = result.Count();
            return result;

        }
        public ITS_WF_Route SearchDuplicate(int rolefrom, int roleto, int acton, int status)
        {
            return GetAll().Where(dat => dat.RoleFrom == rolefrom && dat.RoleTo == roleto && dat.Action == acton && dat.WFStatusToID == status).FirstOrDefault();
            
        }
    }
}