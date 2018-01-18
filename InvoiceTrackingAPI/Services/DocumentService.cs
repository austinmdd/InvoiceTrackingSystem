using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InvoiceTrackingAPI.Repositories;
using InvoiceTrackingAPI.Models;
using InvoiceTrackingAPI.DTO;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using System.IO;

namespace InvoiceTrackingAPI.Services
{
    public class DocumentService: DocumentRepository
    {
        AlfrescoService alfService = new AlfrescoService();
        SupplierInvoiceService invService = new SupplierInvoiceService();
        DocumentTypeService docTypeService = new DocumentTypeService();

        public int MapFormData(System.Net.Http.MultipartFormDataStreamProvider provider)
        {

            string DocumentName = provider.FormData.GetValues("FileName").SingleOrDefault();
            string DocumentType = provider.FormData.GetValues("TypeOfFile").SingleOrDefault();
            string DocumentSize = provider.FormData.GetValues("FileSize").SingleOrDefault();
            int DocumentTypeID = Convert.ToInt32(provider.FormData.GetValues("FileTypeID").SingleOrDefault());
            int SupplierInvoiceID = Convert.ToInt32(provider.FormData.GetValues("InvoiceID").SingleOrDefault());
            string PONumber = provider.FormData.GetValues("PONumber").SingleOrDefault();

            if(DocumentTypeID == 0)
            {
                DocumentTypeID = docTypeService.GetDocTypeByName("Other");
            }


            string data = provider.FormData.GetValues("File").SingleOrDefault();
            var file = Convert.FromBase64String(data);

            //Post document to Alfresco
            var AlfrescoMetaData = alfService.Post(DocumentName, DocumentType, file, PONumber);

            //Get link ID and remove version
            string DocID = AlfrescoMetaData.Id;
            int index = DocID.LastIndexOf(";");
            if (index > 0)
                DocID = DocID.Substring(0, index);

            //Save document metadata to ITS_Document
            if (AlfrescoMetaData != null)
            {
                try
                {
                    ITS_Document objDocument = new ITS_Document();

                    objDocument.SupplierInvoiceID = SupplierInvoiceID;
                    objDocument.DocumentTypeID = DocumentTypeID;
                    objDocument.DocumentName = DocumentName;
                    objDocument.DocumentType = DocumentType;
                    objDocument.DocumentSize = DocumentSize;
                    objDocument.EDMSID = DocID;
                    objDocument.EDMSLocation = AlfrescoMetaData.Parents[0].Path;
                    objDocument.EDMSName = DocumentName;
                    //objDocument.EDMSLink = "http://dmssrv:8080/share/page/site/its/document-details?nodeRef=workspace://SpacesStore/" + DocID + "";
                    objDocument.EDMSLink = "http://dmssrv:8080/share/proxy/alfresco/slingshot/node/content/workspace/SpacesStore/" + DocID + "/" + DocumentName + "";
                    objDocument.DateCreated = System.DateTime.Now;
                    objDocument.UserCreated = "Bongani";
                    objDocument.UserUpdated = 1;
                    objDocument.DateUpdated = System.DateTime.Now;

                    Add(objDocument);
                    SaveChanges();
                }
                catch (Exception e)
                {
                    throw e as Exception;
                }

                return SupplierInvoiceID;
            }


            return SupplierInvoiceID;
        }

        public bool DeleteDocument(int id)
        {
            ITS_Document Document = Get(id);

            ITS_SupplierInvoice supInv = invService.Get(Document.SupplierInvoiceID);

            string FolderName = supInv.ITS_Submission.ITS_PurchaseOrder.PONumber;

            //Delete from Alfreasco first
            bool result = alfService.Delete(Document.DocumentName, FolderName);
            
            if (result)
            {
                try
                {
                    Delete(id);
                    SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex as Exception;
                }
            }

            return result;

        }

        public string DownloadDocument (string id)
        {
            //DotCMIS.Data.Impl.ContentStream
            var result = alfService.Download(id);

            dynamic file = new System.Dynamic.ExpandoObject();
            file.Name = result.FileName;
            file.Type = result.MimeType;

            string obj = Newtonsoft.Json.JsonConvert.SerializeObject(file);

            return obj;




            //byte[] buf = new byte[1024];
            //var response = new HttpResponseMessage(HttpStatusCode.OK);
            //response.Content = new ByteArrayContent(new Byte[result.Stream.Read(buf, 0, 1024)]); //byte[] myArray is byte[] of file downloaded from Azure blob
            ////response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("inline");
            //response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            //response.Content.Headers.ContentDisposition.FileName = result.FileName;
            //response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");


        }

        public List<DocumentsDTO> GetDocumentsByInvoice(int SupplierInvoiceID)
        {
            DocumentTypeService dtservice = new DocumentTypeService();
            List<ITS_Document> lstDocs = GetAll().Where(s => s.SupplierInvoiceID == SupplierInvoiceID).ToList();

            if (lstDocs == null)
                return null;

            
            List<DocumentsDTO> lstDocuments = new List<DocumentsDTO>();

            for (int i=0;i< lstDocs.Count();i++)
            {
                DocumentsDTO Documents = new DocumentsDTO();

                int typeid = -1;
                typeid = lstDocs[i].DocumentTypeID;

                Documents.ID = lstDocs[i].ID;
                Documents.DocName = lstDocs[i].DocumentName;
                Documents.DocTypeID = lstDocs[i].DocumentTypeID;
                Documents.DocTypeName = dtservice.Get(typeid).DocumentType;

                Documents.DocSize = lstDocs[i].DocumentSize;
                Documents.Link = lstDocs[i].EDMSLink;
                Documents.ObjectID = lstDocs[i].EDMSID;

                lstDocuments.Add(Documents);
            }

            return lstDocuments;

        }


        public Boolean DestroyFile(string name)
        {
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\downloadtemp\" + name))
            {
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\downloadtemp\" + name);
                    return true;
            }

            return false;

        }
    }
}