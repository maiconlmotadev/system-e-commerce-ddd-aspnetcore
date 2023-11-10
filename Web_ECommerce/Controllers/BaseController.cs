using ApplicationApp.Interfaces;
using Entities.Entities;
using Entities.Entities.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace Web_ECommerce.Controllers
{
    public class BaseController : Controller
    {
        public readonly ILogger<BaseController> Logger;
        public readonly UserManager<ApplicationUser> _userManager;
        public readonly ILogSystemApp _ILogSystemApp;

        public BaseController(ILogger<BaseController> logger, UserManager<ApplicationUser> userManager, ILogSystemApp iLogSystemApp)
        {
            Logger = logger;
            _userManager = userManager;
            _ILogSystemApp = iLogSystemApp;
        }

        public async Task<string> ReturnLoggedUserId()
        {
            var userId = await _userManager.GetUserAsync(User);
            return userId.Id;

        }

        public async Task LogEcommerce(EnumLogType logType, Object obj)
        {
            string actionName = ControllerContext.RouteData.Values["action"].ToString();
            string controllerName = ControllerContext.RouteData.Values["controller"].ToString();

            await _ILogSystemApp.Add(new LogSystem
            {
                LogType = logType,
                JsonInformation = JsonConvert.SerializeObject(obj),
                UserId = await ReturnLoggedUserId(),
                ActionName = actionName,
                ControllerName = controllerName

            });
            
        }

    }

}
 