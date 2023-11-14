using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace Web_ECommerce.Controllers
{
    public class LogActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Log("OnActionExecuting", filterContext.RouteData, filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Log("OnActionExecuted", filterContext.RouteData);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            Log("OnResultExecuting", filterContext.RouteData);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            Log("OnResultExecuted", filterContext.RouteData);
        }


        private void Log(string methodName, RouteData routeData, ActionExecutingContext filterContext = null)
        {
            var controllerName = routeData.Values["controller"];
            var actionName = routeData.Values["action"];
            var message = String.Format("{0} controller:{1} action:{2}", methodName, controllerName, actionName);


            if (filterContext != null && filterContext.ActionArguments.Any())
            {
                var test = JsonConvert.SerializeObject(filterContext.ActionArguments);
            }


            //  Debug.WriteLine(message, "Action Filter Log");
        }
    }
}
