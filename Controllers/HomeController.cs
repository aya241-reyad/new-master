using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Miar.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Miar.Manager;
using Microsoft.Extensions.Options;
using Specialist_medical.Models;

namespace Miar.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public RealStateManager RealStateManager { get; private set; }

        public HomeController(ILogger<HomeController> logger, IOptions<MyConfig> Config)
        {
            _logger = logger;
            RealStateManager = new RealStateManager(Config.Value.FirebaseClient);

        }

        public async Task<IActionResult> Index()
        {
           var data = await RealStateManager.GetAllStates();
            return View(data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
