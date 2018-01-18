using InvoiceTrackingAPI.Models;
using InvoiceTrackingAPI.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceTrackingAPI.Services
{
    public class POTypeDocTypeLinkService : POTypeDocTypeLinkRepository
    {
        public List<ITS_POTypeDocTypeLink> LinkedDocuments(int POTypeID)
        {

            return GetAll().Where(p => p.POTypeID == POTypeID).OrderBy(x => x.MandatoryYN ? 0 : 1).ToList();
        }
    }
}