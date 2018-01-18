using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InvoiceTrackingAPI.Repositories;
using InvoiceTrackingAPI.DTO;
using InvoiceTrackingAPI.DTO.Search;
using InvoiceTrackingAPI.Models;

namespace InvoiceTrackingAPI.Services
{
    public class WorkflowRouteService : RouteRepository
    {
        public GenericSearchDTO<ITS_WF_Route, WorkflowRouteDTO> GetAllRoutes(int PageNum, int PageSize, bool order = true)
        {
            int Count = 0;
            var routes = FindAllRoutes(PageNum, PageSize, ref Count, order);

            if (routes.Count != 0)
            {
                return new GenericSearchDTO<ITS_WF_Route, WorkflowRouteDTO>(routes, Count);
            }
            return null;

        }

    }
}