using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InvoiceTrackingAPI.Models;

namespace InvoiceTrackingAPI.Repositories
{
    public class WorkflowRouteActionsRepository : BaseRepository<ITS_WF_RouteActions>
    {
        public List<ITS_WF_RouteActions> GetAllActions(int page, int pageSize)
        {

            return GetAll().OrderBy(e => e.ID).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

    }
}