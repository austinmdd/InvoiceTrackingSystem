using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InvoiceTrackingAPI.Services
{
    public class CustomAttributes : Attribute
    {

        public class CustomDateRange : RangeAttribute
        {
            public CustomDateRange() : base(typeof(DateTime), "1/1/0001", DateTime.Now.ToString())
            {
                ErrorMessage = "Invoice date cannot be in the future.";
            }
        }
    }
}