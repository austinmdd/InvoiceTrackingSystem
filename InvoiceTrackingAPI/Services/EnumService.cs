using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceTrackingAPI.Services
{
    public class EnumService
    {
        public enum SubmissionStatus
        {
           
            Saved = 04,
            Submitted = 05,
            Verification = 06,
            Approved = 04,
            Rejected = 05,
            Cancelled = 06,
            InProgress = 07,
            Completed = 08
        }

        public enum POStatus
        {
            Pending_Payment = 00,
            Not_Paid = 01,
            Partly_Paid = 02,
            Paid = 03
        }

    }
}