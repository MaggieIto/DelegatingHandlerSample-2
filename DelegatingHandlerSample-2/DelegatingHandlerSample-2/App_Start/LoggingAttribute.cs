using System;
using System.Diagnostics;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace DelegatingHandlerSample_2
{
    public class LoggingAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext context)
        {
            Debug.WriteLine($"OnActionExecuting: {DateTime.Now.ToString("HH:mm:ss.fff")}");
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
            Debug.WriteLine($"OnActionExecuted: {DateTime.Now.ToString("HH:mm:ss.fff")}");
        }
    }
}