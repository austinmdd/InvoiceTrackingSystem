using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InvoiceTrackingAPI.Models;
using InvoiceTrackingAPI.Services;

namespace InvoiceTrackingAPI.DTO.Workflow
{
    public class RoleDTO
    {
        public int ID { get; set; }
        public string Role { get; set; }
        public string Description { get; set; }
        public bool Enabled { get; set; }
        public bool IsWorkflowRole { get; set; }
        public int UserUpdated { get; set; }
        public string UserCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime DateCreated { get; set; }
       // public WorkflowRouteDTO route { get; set; }
        public RoleDTO(ITS_Role role)
        {
            ID = role.ID;
            Role = role.Role;
            Description = role.Description;
            Enabled = role.Enabled;
            IsWorkflowRole = role.IsWorkflowRole;
            UserUpdated = role.UserUpdated;
            UserCreated = role.UserCreated;
            DateUpdated = role.DateUpdated;
            DateCreated = role.DateCreated;
        }

        public List<RoleDTO> Roles = new List<RoleDTO>();
        public RoleDTO(List<ITS_Role> roles, int records = 0)
        {
            foreach (ITS_Role role in roles)
            {
                Roles.Add(new RoleDTO(role));
            }
        }
    }
}