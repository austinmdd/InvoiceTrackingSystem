using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InvoiceTrackingAPI.Models;
using InvoiceTrackingAPI.DTO.Workflow;
using InvoiceTrackingAPI.Services;

namespace InvoiceTrackingAPI.DTO
{
    public class WorkflowRouteDTO
    {
        public int ID { get; set; }

        public int RoleFrom { get; set; }

        public int RoleTo { get; set; }

        public int Action { get; set; }

        public int WFStatusToID { get; set; }


        public string UserCreated { get; set; }

        public int UserUpdated { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        public RoleDTO workflowroles { get;set;}

        public WorkflowRouteDTO Supplier { get; set; }


        public WorkflowRouteDTO ( ITS_WF_Route route,bool AddExtra = true)
        {
            ID = route.ID;
            RoleFrom = route.RoleFrom;
            RoleTo = route.RoleTo;
            Action = route.Action;
            WFStatusToID = route.WFStatusToID;
            UserCreated = route.UserCreated;
            UserUpdated = route.UserUpdated;
            DateCreated = route.DateCreated;
            DateUpdated = route.DateUpdated;

           
                if (AddExtra)
                {
                //Supplier = new WorkflowRouteDTO(route.RoleFrom.CompareTo(workflowroles.ID);

            }

            
        }
        public WorkflowRouteDTO(ITS_WF_Route route)
        {
            ID = route.ID;
            RoleFrom = route.RoleFrom;
            RoleTo = route.RoleTo;
            Action = route.Action;
            WFStatusToID = route.WFStatusToID;
            UserCreated = route.UserCreated;
            UserUpdated = route.UserUpdated;
            DateCreated = route.DateCreated;
            DateUpdated = route.DateUpdated;
        }
        public List<WorkflowRouteDTO> Routes = new List<WorkflowRouteDTO>();
        public WorkflowRouteDTO(List<ITS_WF_Route> routes, int records = 0)
        {
            foreach (ITS_WF_Route route in routes)
            {
                Routes.Add(new WorkflowRouteDTO(route));
            }
           
        }

    }
}