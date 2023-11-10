using ApplicationApp.Interfaces;
using ApplicationApp.OpenApp;
using Domain.Interfaces.InterfaceLogSystem;
using Microsoft.AspNetCore.Mvc;

namespace Web_ECommerce.Controllers
{
    public class LogSystemsController : Controller
    {
        private readonly ILogSystemApp _ILogSystemApp;

        public LogSystemsController(ILogSystemApp ILogSystemApp)
        {
            _ILogSystemApp = ILogSystemApp;
        }

        // GET: LogSystems
        public async Task<IActionResult> Index()
        {

            return View(await _ILogSystemApp.List());
        }

        // GET: LogSystems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var LogSystem = await _ILogSystemApp.GetEntityById((int)id);
            if (LogSystem == null)
            {
                return NotFound();
            }

            return View(LogSystem);
        }
    }
}
