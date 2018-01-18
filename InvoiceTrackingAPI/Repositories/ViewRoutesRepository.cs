using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InvoiceTrackingAPI.Models;

namespace InvoiceTrackingAPI.Repositories
{
    public class ViewRoutesRepository : BaseRepository<ViewRoutes>
    {
        public List<ViewRoutes> GetAllRoutes(int page, int pageSize)
        {

            return GetAll().OrderBy(e => e.ID).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
        public ViewRoutes GetRoute(int id )
        {
            return GetAll().Where(rut => rut.ID == id).FirstOrDefault();

        }

    }
}