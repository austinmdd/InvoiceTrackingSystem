using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InvoiceTrackingAPI.Models;

namespace InvoiceTrackingAPI.DTO.Workflow
{
    public class ViewRoutesDTO
    {
       
        public int ID { get; set; }
        public int RoleFrom { get; set; }
        public string RoleFromName { get; set; }
        public int RoleTo { get; set; }
        public string RoleToName { get; set; }
        public int WFStatusToID { get; set; }
        public string Description { get; set; }
        public int Action { get; set; }
        public bool Enabled { get; set; }
       public DateTime DateCreated { get; set; }
        public string UserCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public int UserUpdated { get; set; }
        public string RouteName { get; set; }

        public ViewRoutesDTO(ViewRoutes routes)
        {
            ID = routes.ID;
            RoleFrom = routes.RoleFrom;
            RoleFromName = routes.RoleFromName;
            RoleTo = routes.RoleTo;
            RoleToName = routes.RoleToName;
            WFStatusToID = routes.WFStatusToID;
            Description = routes.Description;
            Action = routes.Action;
            Enabled = routes.Enabled;
            DateCreated = routes.DateCreated;
            UserCreated = routes.UserCreated;
            DateUpdated = routes.DateUpdated;
            UserUpdated = routes.UserUpdated;
            RouteName = routes.RouteName;


        }
        public int Count { get; set; }
        public List<ViewRoutesDTO> Routing = new List<ViewRoutesDTO>();

        public ViewRoutesDTO(List<ViewRoutes> rutes, int records = 0)
        {
            Count = records;
            foreach (ViewRoutes rut in rutes)
            {
                Routing.Add(new ViewRoutesDTO(rut));
            }
        }

      
    }
}