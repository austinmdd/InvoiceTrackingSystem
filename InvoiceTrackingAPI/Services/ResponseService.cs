using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace InvoiceTrackingAPI.Services
{
    public class ResponseService<T> : IHttpActionResult where T:class
    {
        T entity;
        HttpRequestMessage request;
       
        public ResponseService(T entity, HttpRequestMessage request)
        {
            this.entity = entity;
            this.request = request;
        }
        Task<HttpResponseMessage> IHttpActionResult.ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage()
            {
                Content = new ObjectContent<T>(entity,new JsonMediaTypeFormatter()),
                RequestMessage = request
                
            };
            
            return Task.FromResult(response);
        }
    }
}