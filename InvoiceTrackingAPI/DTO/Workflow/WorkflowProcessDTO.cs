using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InvoiceTrackingAPI.Models;

namespace InvoiceTrackingAPI.DTO
{
    public class WorkflowProcessDTO
    {

        public int ID { get; set; }

        public int SubmissionID { get; set; }
        public int SupplierInvoiceID { get; set; }

        public int RoleID { get; set; }

        public int UserID { get; set; }

        public int WF_StatusID { get; set; }

        public DateTime DateAssigned { get; set; }

        public DateTime DateCompleted { get; set; }

        public int RouteID { get; set; }

        public bool? Status { get; set; }

        public WorkflowProcessDTO(ITS_WF_Process workflowprocess)
        {
            ID = workflowprocess.ID;
            SubmissionID = workflowprocess.SubmissionID;
            SupplierInvoiceID = workflowprocess.SupplierInvoiceID;
            RoleID = workflowprocess.RoleID;
            UserID = workflowprocess.UserID;
            WF_StatusID = workflowprocess.WF_StatusID;
            DateAssigned = workflowprocess.DateAssigned;
            DateCompleted = workflowprocess.DateCompleted;
            RouteID = workflowprocess.RouteID;
            Status = workflowprocess.Status;
        }

        public List<WorkflowProcessDTO> Allprocesses = new List<WorkflowProcessDTO>();
        public WorkflowProcessDTO(List<ITS_WF_Process> processes, int records = 0)
        {
            foreach (ITS_WF_Process process in processes)
            {
                Allprocesses.Add(new WorkflowProcessDTO(process));
            }
        }

    }
}