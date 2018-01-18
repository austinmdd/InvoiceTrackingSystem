using AutoMapper;
using InvoiceTrackingAPI.DTO.Maintain;
using InvoiceTrackingAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using InvoiceTrackingAPI.Repositories;

namespace InvoiceTrackingAPI.Services
{
    public class UtilsService<T>: ApiController where T : class
    {      

        public IHttpActionResult Error(string exceptionMSG,string url)
        {
            return ResponseMessage(ResponseMessageService.ErrorMessage(url, exceptionMSG));
        }

        public IHttpActionResult Response(T type, HttpStatusCode code, string msg, string Uri)
        {            

                if (type != null)
                {
                    return new ResponseService<T>(type, Request);
                }
                return ResponseMessage(new ResponseMessageService(code, msg, Uri));
           
        }

        public static List<D> MapTo<S, D>(List<S> Items) where S : class  where D : class
        {
            List<D> records = new List<D>();
            Mapper.Initialize(cfg => cfg.CreateMap<S, D>());
            foreach (S doct in Items)
            {
                records.Add(Mapper.Map<D>(doct));
            }
            return records;
        }

        internal IHttpActionResult Response(List<RuleRoleLinkRepository> list, HttpStatusCode internalServerError, string v, string absoluteUri)
        {
            throw new NotImplementedException();
        }

        public static implicit operator UtilsService<T>(UtilsService<ITS_Notes> v)
        {
            throw new NotImplementedException();
        }
    }
}