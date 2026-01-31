using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace DelegatingHandlerSample_2
{
    public class LoggingAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext context)
        {

        }
    }
}