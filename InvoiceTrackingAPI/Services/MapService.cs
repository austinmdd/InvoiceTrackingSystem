using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceTrackingAPI.Services
{
    public class MapService
    {
        public static D MapOne<S, D>(S Item) where S : class where D : class
        {           
            Mapper.Initialize(cfg => cfg.CreateMap<S, D>());
            return Mapper.Map<D>(Item);
            //return null;
        }
        public static List<D> MapMany<S, D>(List<S> Items) where S : class where D : class
        {
            List<D> records = new List<D>();
            Mapper.Initialize(cfg => cfg.CreateMap<S, D>());
            foreach (S doct in Items)
            {
               records.Add(Mapper.Map<D>(doct));
            }
            return records;
        }
    }
}                                                                                    