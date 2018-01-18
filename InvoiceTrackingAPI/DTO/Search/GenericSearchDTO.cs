using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceTrackingAPI.DTO.Search
{
    public class GenericSearchDTO<T, D> where T : class where D : class
    {
        public int Count { get; set; }
        public List<D> Items = new List<D>();

        public GenericSearchDTO(List<T> types, int records = 0, bool AddExtra = true)
        {
            Count = records;
            foreach (T type in types)
            {                
                Items.Add((D)Activator.CreateInstance(typeof(D), type,AddExtra));
            }

        }
    }
}