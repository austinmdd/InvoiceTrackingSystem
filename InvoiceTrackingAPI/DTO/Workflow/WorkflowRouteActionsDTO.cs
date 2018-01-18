using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InvoiceTrackingAPI.Models;

namespace InvoiceTrackingAPI.DTO.Workflow
{
    public class WorkflowRouteActionsDTO
    {
        public int ID { get; set; }

        
        public string RouteName { get; set; }

        public int UserUpdated { get; set; }
       public string UserCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        public DateTime DateCreated { get; set; }

        public WorkflowRouteActionsDTO(ITS_WF_RouteActions actions)
        {
            ID = actions.ID;
            RouteName = actions.RouteName;
            UserUpdated = actions.UserUpdated;
            UserCreated = actions.UserCreated;
            DateUpdated = actions.DateUpdated;
            DateCreated = actions.DateCreated;
   }
        public int Count { get; set; }
        public List<WorkflowRouteActionsDTO> Allactions = new List<WorkflowRouteActionsDTO>();

        public WorkflowRouteActionsDTO(List<ITS_WF_RouteActions> actions, int records = 0)
        {
            Count = records;
            foreach (ITS_WF_RouteActions act in actions)
            {
                Allactions.Add(new WorkflowRouteActionsDTO(act));
            }
        }

    }
}