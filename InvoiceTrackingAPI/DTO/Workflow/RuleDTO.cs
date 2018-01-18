using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InvoiceTrackingAPI.Models;


namespace InvoiceTrackingAPI.DTO.Workflow
{
    public class RuleDTO
    {
        public int ID { get; set; }
        public string ChecklistName { get; set; }

        public bool? Enabled { get; set; }

        public int UserUpdated { get; set; }


        public string UserCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        public DateTime DateCreated { get; set; }

        public RuleDTO(ITS_WF_Checklist rule)
        {
            ID = rule.ID;
            ChecklistName = rule.ChecklistName;
            Enabled = rule.Enabled;
            UserUpdated = rule.UserUpdated;
            UserCreated = rule.UserCreated;
            DateUpdated = rule.DateUpdated;
            DateCreated = rule.DateCreated;


        }

        public int Count { get; set; }
        public List<RuleDTO> Allrules = new List<RuleDTO>();

        public RuleDTO(List<ITS_WF_Checklist> rules, int records = 0)
        {
            Count = records;
            foreach (ITS_WF_Checklist rule in rules)
            {
                Allrules.Add(new RuleDTO(rule));
            }
        }
    }
}


