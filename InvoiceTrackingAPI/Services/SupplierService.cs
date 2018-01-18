using InvoiceTrackingAPI.Models;
using InvoiceTrackingAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InvoiceTrackingAPI.DTO.Search;

namespace InvoiceTrackingAPI.Services
{
    public class SupplierService: SupplierRepository
    {       
        public SupplierDTO GetByPO(string PO) {
            var SUP = FindByPO(PO);
            return (SUP != null) ? new SupplierDTO(SUP) : null;
        }

        public  GenericSearchDTO<ITS_Supplier, SupplierDTO> GetByNamePaging(int PageNum, int PageSize, string Name)
        {
            int Count = 0;
            var supps = FindByNamePaging(PageNum, PageSize, Name, ref Count);
            if (supps.Count != 0)
            {
                return new GenericSearchDTO<ITS_Supplier, SupplierDTO>(supps, Count);
            }
            return null;
        }
              
    }
}