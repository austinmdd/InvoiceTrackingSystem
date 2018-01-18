using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace InvoiceTrackingAPI
{
    public class HelperService
    {

        public string ExceptionHelper(Exception ex)
        {
            string HttpMessage = "";
            string ErrorMessage = "";
            string PropertyName = "";

            if (ex is DbEntityValidationException)
            {

                DbEntityValidationException dbException = ex as DbEntityValidationException;

                var error = dbException.EntityValidationErrors;

                for (int i = 0; i < error.Count(); i++)
                {

                    ErrorMessage = error.ElementAt(i).ValidationErrors.ElementAt(i).ErrorMessage;
                    PropertyName = error.ElementAt(i).ValidationErrors.ElementAt(i).PropertyName;

                }

                return HttpMessage = "Field: " + PropertyName + " Error Message : " + ErrorMessage;

            }
            else if (ex is InvalidOperationException)
            {

                return HttpMessage = "Helow";
            }
            else
            {
                return HttpMessage = "Error Message : " + ex.Message;

            }

        }

        public string GenerateSubmissionNumber(int counter)
        {
            string sYear = DateTime.Now.Year.ToString();

            string sMonth = DateTime.Now.Month.ToString().PadLeft(2, '0');

            string sDay = DateTime.Now.Day.ToString().PadLeft(2, '0');

            string ID = "-" + counter.ToString().PadLeft(4, '0');

            string submissionNumber = "S-" + sYear + sMonth + sDay + ID;

            return submissionNumber;
        }


    }
}