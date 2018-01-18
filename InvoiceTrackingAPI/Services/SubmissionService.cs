using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InvoiceTrackingAPI.Repositories;

using InvoiceTrackingAPI.Models;
using InvoiceTrackingAPI.DTO;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Collections;

namespace InvoiceTrackingAPI.Services
{
    public class SubmissionService : SubmissionRepository
    {
        SupplierInvoiceService invService = new SupplierInvoiceService();
        PurchaseOrderService orderService = new PurchaseOrderService();
        SupplierService suppService = new SupplierService();

        public ITS_Submission FindByPO(int PO)
        {
            return GetAll().Where(s => s.PurchaseOrderID == PO).FirstOrDefault();
        }

        public bool InvoiceNumberUnique(string invoicenumber, string vendorcode)
        {

            int supplierid = suppService.GetAll().Where(s => s.VendorCode == vendorcode).First().ID;

            List<ITS_Submission>  AllSupplierSubmissions = GetAll().Where(s => s.SupplierID == supplierid).ToList();

            for (int i=0; i < AllSupplierSubmissions.Count();i++)
            {

                var invoice = invService.GetAll().Where(v => v.SubmissionID == AllSupplierSubmissions[i].ID && v.InvoiceNumber == invoicenumber).FirstOrDefault();

                if(invoice != null)
                {
                    return false;
                }
            }

            return true;

        }
        

        public bool ValidEntity(SupplierInvoiceSubmissionDTO entity)
        {


            if (entity.InvoiceDate == Convert.ToDateTime("0001/01/01 12:00:00 AM"))
            {
                return false;

            }
            else 
            {
                return true;
            }

        }

        public string SubmitInvoice(SupplierInvoiceSubmissionDTO entity)
        {
            
            //Save the submission and get ID to save invoice
            ITS_Submission subObj = new ITS_Submission();
            subObj = FindByPO(entity.POID);

            var submission = SaveSubmission(subObj, entity);

            //Save invoice

            var invoiceID = invService.SaveInvoices(entity, submission.ID);

            //Update PO to pending payment and due amount. 
            ITS_PurchaseOrder poObj = new ITS_PurchaseOrder();
            poObj = orderService.Get(entity.POID);

            orderService.UpdatePO(poObj);

            if (entity.FileUploadType == "") //Check if we are saving for file upload or saving permanantly
            {
                //Finally updated the submission number
                UpdateSubmissionNumber(submission);

                return submission.SubmissionNumber;
            }
            else
            {
                return Convert.ToString(invoiceID);
            }
        }

        private ITS_Submission SaveSubmission(ITS_Submission submission, SupplierInvoiceSubmissionDTO entity)
        {

            ITS_Submission obj = new ITS_Submission();


           var supplier = suppService.FindByPO(entity.PONumber);

            if (submission != null)
            {
                //Base class and 
                //obj = Get(_SubmissionID);
                submission.DateUpdated = System.DateTime.Now;
                submission.UserUpdated = 1;

                submission = Update(submission);
            }
            else
            {
                obj.SubmissionNumber = "";
                obj.PurchaseOrderID = entity.POID;
                obj.SupplierID = supplier.ID;
                obj.Status = EnumService.SubmissionStatus.Saved.ToString();
                obj.PONumber = entity.PONumber;
                obj.POAmount = entity.POAmount;
                obj.DateCreated = System.DateTime.Now;
                obj.UserCreated = "Bongani"; //Get user from session
                obj.UserUpdated = 1; //Get user from session
                obj.DateUpdated = System.DateTime.Now;

               

                try
                {
                    submission = Add(obj);
                    SaveChanges();

                }
                catch (Exception ex)
                {
                    throw ex;

                }
               
            }


            return submission;
        }
        private void UpdateSubmissionNumber(ITS_Submission submission)
        {
            HelperService helper = new HelperService();
            string subNumber;
            int counter;

            //Reset submission number counter
            var TodaysSub = GetAll().Where(p => p.DateCreated.Date == DateTime.Now.Date).ToList();

            if (TodaysSub != null)
            {
                counter = TodaysSub.Count();
            }
            else
            {
                counter = 1;
            }

            try
            {
                subNumber = helper.GenerateSubmissionNumber(counter);
            }
            catch (Exception ex )
            {
                
                subNumber = "";
                throw ex;
            }

            submission.SubmissionNumber = subNumber;

            Update(submission);
        }

    }

    }
