using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InvoiceTrackingAPI.Models;
using InvoiceTrackingAPI.Services;

namespace InvoiceTrackingAPI.DTO.Workflow
{
    public class RuleRoleLinkDTO
    {
        public int ID { get; set; }

        public int RuleID { get; set; }

        public int RoleID { get; set; }
        public string UserCreated { get; set; }

        public int UserUpdated { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }
        public RuleDTO Workflowrule { get; set; }


        public RuleRoleLinkDTO(ITS_WF_RuleRoleLink rolelink, bool AddExtra)
        {
            ID = rolelink.ID;
            RuleID = rolelink.RuleID;
            RoleID = rolelink.RoleID;
            UserCreated = rolelink.UserCreated;
            UserUpdated = rolelink.UserUpdated;
            DateCreated = rolelink.DateCreated;
            DateUpdated = rolelink.DateUpdated;
            if (AddExtra)
            {

                Workflowrule = MapService.MapOne<ITS_WF_Checklist, RuleDTO>(rolelink.ITS_WF_Checklist);

            }
        }
        public RuleRoleLinkDTO(ITS_WF_RuleRoleLink rolelink)
        {
            ID = rolelink.ID;
            RuleID = rolelink.RuleID;
            RoleID = rolelink.RoleID;
            UserCreated = rolelink.UserCreated;
            UserUpdated = rolelink.UserUpdated;
            DateCreated = rolelink.DateCreated;
            DateUpdated = rolelink.DateUpdated;
        }

    }
}



