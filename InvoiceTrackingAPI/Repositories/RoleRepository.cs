using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InvoiceTrackingAPI.Models;

namespace InvoiceTrackingAPI.Repositories
{
    public class RoleRepository : BaseRepository<ITS_Role>
    {
     
        public List<ITS_Role> GetAllRoles(int page, int pageSize)
        {

            return GetAll().OrderBy(e => e.ID).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}