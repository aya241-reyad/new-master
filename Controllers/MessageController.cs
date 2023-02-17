using Miar.Manager;
using Miar.Models;
using Miar.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Specialist_medical.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taisser.Manager;

namespace Miar.Controllers
{
    public class MessageController : Controller
    {

        private readonly ILogger<MessageController> _logger;

        public MessageManger MessageManger { get; private set; }

        public IWebHostEnvironment Environment { get; }

        public MessageController(ILogger<MessageController> logger, IOptions<MyConfig> Config, IWebHostEnvironment environment, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            if (httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                string Token = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(a => a.Type.Split('/').Last() == "primarysid").Value;
                MessageManger = new MessageManger(Config.Value.FirebaseClient, Token);
            }
            else
            {
                MessageManger = new MessageManger(Config.Value.FirebaseClient);

            }
            Environment = environment;

        }


     

        // GET: MessageController/Create
        public ActionResult Create()
        {
            return View();
        }


        // POST: MessageController/Create
        [HttpPost]
        public async  Task<ActionResult> Create(Messages collection)
        {
            try
            {
                collection.EnterDate = DateTime.Now;
                await MessageManger.AddMessage(collection);
                return Json("Done");

            }
            catch
            {
                return Json("Error");
            }
        }


        [Authorize]
        public async Task<ActionResult> Index(DateTime ResultDate_From, DateTime ResultDate_To, byte? Caller, int? pageNum = 1, int? pagesize = 5)
        {

            var Data = await MessageManger.GetAllMessages();
            var LastData = Data.Where(a => (ResultDate_From == DateTime.MinValue || a.EnterDate.Date >= ResultDate_From.Date) && (ResultDate_To == DateTime.MinValue || a.EnterDate.Date <= ResultDate_To.Date)).Select(a => new MessageList
            {
                Name = a.Name,
                EnterDate = a.EnterDate.ToShortDateString(),
                Key = a.Key,
                Message = a.Message,
                Tele = a.Tele,

            }); ;
            (PaggingManger<MessageList> PaggingData, int pagenumber, int Totalcount) = PaggingManger<MessageList>.CreateWithPageNum(LastData, (int)pagesize, (int)pageNum);
            ViewBag.PagesNum = pagenumber;
            ViewBag.TotalNum = Totalcount;

            if (!Caller.HasValue)
            {
                return View(PaggingData);
            }
            else
                return Json(new { Data = PaggingData, pageNum = pagenumber, Totalcount = Totalcount });
        }

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(string id)
        {
            var Message = await MessageManger.GetOneMessages(id);
            return View(Message);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id, Messages data)
        {
            await MessageManger.Delete(id);

            return RedirectToAction("Index");

        }

    }
}
