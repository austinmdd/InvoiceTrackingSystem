using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ExceptionHandling;

namespace InvoiceTrackingAPI.Handlers
{
    public class ITSExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            context.Result = new ITSInternalServerErrorsForClient(context);
        }
    }
}