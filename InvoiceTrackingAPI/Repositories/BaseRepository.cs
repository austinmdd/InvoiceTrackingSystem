using InvoiceTrackingAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace InvoiceTrackingAPI.Repositories
{
    public class BaseRepository<T> where T: class
    {
        private InvoiceTrackingContext context;

        protected DbSet<T> DbSet;

        public BaseRepository()
        {
            context = new InvoiceTrackingContext();
            DbSet = context.Set<T>();
        }

        public List<T> GetAll()
        {
            return DbSet.ToList();
        }

        public T Get(int id) {
            return DbSet.Find(id);
        }

        public T Add(T entity) 
        {            
                DbSet.Add(entity);
                SaveChanges();
                return entity;            
        }

        public T Update(T entity)
        {            
            context.Set<T>().Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            SaveChanges();
            return entity;
        }

        public T Delete(int id)
        {            
            T entity = DbSet.Find(id);
            entity = (entity!=null)? DbSet.Remove(entity):null;
            SaveChanges();
            return entity;           
        }
        public void SaveChanges() {
            context.SaveChanges();
        }



    }
}