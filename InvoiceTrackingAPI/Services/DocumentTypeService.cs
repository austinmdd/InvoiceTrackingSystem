using InvoiceTrackingAPI.DTO;
using InvoiceTrackingAPI.Models;
using InvoiceTrackingAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace InvoiceTrackingAPI.Services
{
    public class DocumentTypeService: DocumentTypeRepository
    {
        DocumentTypeInvoiceCategoriesLinksServices docTypInvCatLinksServices = new DocumentTypeInvoiceCategoriesLinksServices();
        InvoiceCategories invCatService = new InvoiceCategories();
        
        public InvoiceCategoryLinksDTO GetInvoiceCatLinksTypes(int invCatId, ref int count)
        {
            var linked = GetInvoiceCatLinkedDocTypes(invCatId);
            var unlinked = GetAll().Except(linked).ToList();
            count = unlinked.Count();
            return new InvoiceCategoryLinksDTO(unlinked, linked);
        }

        public void AddLinks(List<DoctypesLinksDTO> linked)
        {
            foreach (DoctypesLinksDTO link in linked)
            {
                var newLink = MapService.MapOne<DoctypesLinksDTO,ITS_DocumentTypeInvoiceCategoriesLinks>(link);                                
                docTypInvCatLinksServices.Add(newLink);                
            }
        }

        public DoctypesLinksDTO UpdateLink(DoctypesLinksDTO linked) {    
            var link = docTypInvCatLinksServices.Update(MapService.MapOne<DoctypesLinksDTO, ITS_DocumentTypeInvoiceCategoriesLinks>(linked));
            return MapService.MapOne<ITS_DocumentTypeInvoiceCategoriesLinks, DoctypesLinksDTO>(link);            
        }

        public DoctypesLinksDTO DeleteLink(int id)
        {
            var deletedLink = docTypInvCatLinksServices.Delete(id);
            return MapService.MapOne<ITS_DocumentTypeInvoiceCategoriesLinks, DoctypesLinksDTO>(deletedLink);
        }

        public DoctypesLinksDTO GetLink(int id)
        {           
            return MapService.MapOne<ITS_DocumentTypeInvoiceCategoriesLinks, DoctypesLinksDTO>(docTypInvCatLinksServices.Get(id));
        }

        public int GetDocTypeByName(string DocumentName)
        {
            return GetAll().Where(x => x.DocumentType == DocumentName).Select(y => y.ID).FirstOrDefault();
                         
        }
    }
}