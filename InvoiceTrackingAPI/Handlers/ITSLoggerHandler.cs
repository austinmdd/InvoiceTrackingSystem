using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ExceptionHandling;

namespace InvoiceTrackingAPI.Handlers
{
    public class ITSLoggerHandler : ExceptionLogger
    {

        public override void Log(ExceptionLoggerContext context)
        {           
            ILog log = LogManager.GetLogger(context.ExceptionContext.ControllerContext.Controller.GetType());
            log.ErrorFormat("Unhandled exception processing {0} for {1}: {2}",
                context.Request.Method,
                context.Request.RequestUri,
                context.Exception);
        }
    }
}