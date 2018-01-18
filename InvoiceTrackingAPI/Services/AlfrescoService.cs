using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InvoiceTrackingAPI.DTO;
using System.Web.UI.WebControls;

using DotCMIS;
using DotCMIS.Client.Impl;
using DotCMIS.Client;
using DotCMIS.Data.Impl;
using DotCMIS.Data.Extensions;
using System.IO;

namespace InvoiceTrackingAPI.Models
{
    public class AlfrescoService
    {
        ISession session;
        IOperationContext operationContext;
        IFolder Master;
        IFolder WorkingDirectory;

        public AlfrescoService()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters[DotCMIS.SessionParameter.BindingType] = BindingType.AtomPub;
            parameters[DotCMIS.SessionParameter.AtomPubUrl] = "http://dmssrv:8080/alfresco/api/-default-/public/cmis/versions/1.0/atom";
            parameters[DotCMIS.SessionParameter.User] = "MduduziP";
            parameters[DotCMIS.SessionParameter.Password] = "P@sw0rd12";

            SessionFactory factory = SessionFactory.NewInstance();

            IList<IRepository> repos = factory.GetRepositories(parameters);
            session = repos.ElementAt(0).CreateSession();

            operationContext = session.CreateOperationContext();
            operationContext.IncludeAcls = false;
        }
       

        public IDocument Post(string DocumentName, string DocumentType, byte[] File, string FolderName)
        {

            //IFolder folder = session.GetRootFolder();
            Master = (IFolder)session.GetObject("8a4fec83-a7db-4636-8e84-9ed0babac107");

            var Exists = Master.GetChildren(operationContext).Any(child => child.Name.Equals(FolderName));

            if (Exists)
            {

                IItemEnumerable<ICmisObject> Children = Master.GetChildren();

                foreach (var child in Children)
                {
                    if (child.Name == FolderName)
                    {
                        WorkingDirectory = child as IFolder;
                    }
                }
            }
            else
            {
                 WorkingDirectory = CreateFolder(Master, FolderName);

            }
            

            // define dictionary
            IDictionary<string, object> properties = new Dictionary<string, object>();
            
            properties.Add(PropertyIds.Name, DocumentName);
            properties.Add(PropertyIds.ObjectTypeId, "cmis:document");
            ContentStream contentStream = new ContentStream
            {
                FileName = DocumentName,
                MimeType = DocumentType,
                Length = File.Length,
                Stream = new MemoryStream(File)
            };
            

            if (FileExists(WorkingDirectory, DocumentName))
            {
                IItemEnumerable<ICmisObject> Files = WorkingDirectory.GetChildren();
                DeleteFile(Files, DocumentName);
            }
           
            return WorkingDirectory.CreateDocument(properties, contentStream,null);

        }

        public bool Delete(string DocumentName, string FolderName)
        {

            Master = (IFolder)session.GetObject("8a4fec83-a7db-4636-8e84-9ed0babac107");

            var Exists = Master.GetChildren(operationContext).Any(child => child.Name.Equals(FolderName));

            if (Exists)
            {

                IItemEnumerable<ICmisObject> Children = Master.GetChildren();

                foreach (var child in Children)
                {
                    if (child.Name == FolderName)
                    {
                        WorkingDirectory = child as IFolder;
                    }
                }
            }
            else
            {
                WorkingDirectory = CreateFolder(Master, FolderName);

            }

            if (FileExists(WorkingDirectory, DocumentName))
            {
                IItemEnumerable<ICmisObject> Files = WorkingDirectory.GetChildren();

                try
                {
                    DeleteFile(Files, DocumentName);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                    throw ex as Exception;

                }

            }

            return true;

        }

        public bool FileExists (IFolder WorkingDirectory, string FileName)
        {
            return WorkingDirectory.GetChildren(operationContext).Any(child => child.Name.Equals(FileName));

        }

        public bool DeleteFile(IItemEnumerable<ICmisObject> Files, string FileName)
        {
            bool prop = false;

            foreach (var file in Files)
            {
                try
                {
                    if (file.Name == FileName)
                    {
                        file.Delete(true);
                    }

                    prop = true;
                }
                catch (Exception e)
                {
                   
                    throw e as Exception;
                }
                
            }

            return prop;
        }
        private static IFolder CreateFolder(IFolder target, String newFolderName)
        {
            IDictionary<string, object> props = new Dictionary<string, object>();
            props.Add(PropertyIds.ObjectTypeId, "cmis:folder");
            props.Add(PropertyIds.Name, newFolderName);

            IFolder newFolder = target.CreateFolder(props);
            return newFolder;
        }

        public void RenameAlfrescoDirectory(string OldName ,string NewName)
        {
            Master = (IFolder)session.GetObject("8a4fec83-a7db-4636-8e84-9ed0babac107");
            IItemEnumerable<ICmisObject> Children = Master.GetChildren();

            foreach (var child in Children)
            {
                if (child.Name == OldName)
                {
                    child.Rename(NewName);
                }
            }

        }

        public ContentStream Download (string DocumentID)
        {

            try
            {
                Document doc = session.GetObject(DocumentID) as Document;
                ContentStream contentStream = (DotCMIS.Data.Impl.ContentStream)doc.GetContentStream();
                Stream stream = contentStream.Stream;

                string path = AppDomain.CurrentDomain.BaseDirectory + @"\downloadtemp\";

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                using (var fileStream = File.Create(path + contentStream.FileName))
                {
                    contentStream.Stream.CopyTo(fileStream);
                }

                return contentStream;

            }
            catch (Exception ex)
            {
                throw ex as Exception;
            }

        }
    }
}