using InvoiceTrackingAPI.Repositories;
using InvoiceTrackingAPI.Models;
using System.Linq;
using System.Web;
using System;
using System.Collections.Generic;

namespace InvoiceTrackingAPI.Services
{
    public class ViewRoutesService : ViewRoutesRepository
    {
        public List<ViewRoutes> GetSearchedRules(int PageNum, int PageSize, string Searchtext)
        {
            return GetAll().Where(ser => ser.RoleFromName.ToLower().Trim().Contains(Searchtext.ToLower().Trim()) || ser.Enabled.ToString().ToLower().Trim().Contains(Searchtext.ToLower().Trim()) || ser.RoleToName.ToString().ToLower().Trim().Contains(Searchtext.ToLower().Trim()) || ser.RouteName.ToString().ToLower().Trim().Contains(Searchtext.ToLower().Trim()) || ser.Description.ToString().ToLower().Trim().Contains(Searchtext.ToLower().Trim())).ToList();//OrderBy;
        }

    }
}