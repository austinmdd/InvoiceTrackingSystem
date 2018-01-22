using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InvoiceTrackingAPI.Models;

namespace InvoiceTrackingAPI.DTO.Workflow
{
    public class WorkflowStatusDTO
    {
        public int ID { get; set; }

        public int? StatusCode { get; set; }


        public string WorkflowStatus { get; set; }

    
        public string Description { get; set; }

      
        public string UserCreated { get; set; }

        public int UserUpdated { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        public WorkflowStatusDTO(ITS_WF_Status workflowstatus)
        {
            ID = workflowstatus.ID;
            StatusCode = workflowstatus.StatusCode;
            WorkflowStatus = workflowstatus.WorkflowStatus;
            Description = workflowstatus.Description;
            UserCreated = workflowstatus.UserCreated;
            UserUpdated = workflowstatus.UserUpdated;
            DateCreated = workflowstatus.DateCreated;
            DateUpdated = workflowstatus.DateUpdated;
        }
        public int Count { get; set; }
        public List<WorkflowStatusDTO> Allstatuses = new List<WorkflowStatusDTO>();
        public WorkflowStatusDTO(List<ITS_WF_Status> statuses, int records = 0)
        {
            Count = records;
            foreach (ITS_WF_Status status in statuses)
            {
                Allstatuses.Add(new WorkflowStatusDTO(status));
            }
        }

    }
}