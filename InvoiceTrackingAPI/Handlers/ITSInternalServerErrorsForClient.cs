using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace InvoiceTrackingAPI.Handlers
{
    public class ITSInternalServerErrorsForClient : IHttpActionResult
    {
        private Exception _exception;
        private HttpRequestMessage _request;
        public ITSInternalServerErrorsForClient(ExceptionHandlerContext context)
        {
            _exception = context.Exception;
            _request = context.Request;
        }
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute());
        }

        private HttpResponseMessage Execute()
        {           
            var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new ObjectContent<ExceptionMSG>(new ExceptionMSG(((int)HttpStatusCode.InternalServerError).ToString(), _exception.Message, _request.RequestUri.ToString()), new JsonMediaTypeFormatter()),
                RequestMessage = _request,
                ReasonPhrase = "ITS"               
            };                        
            return response;
        }
    }
}