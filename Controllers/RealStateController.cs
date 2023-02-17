using Miar.Manager;
using Miar.Models;
using Miar.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Specialist_medical.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Taisser.Manager;

namespace Miar.Controllers
{
    public class RealStateController : Controller
    {
        public RealStateManager RealStateManager { get; private set; }
        public IWebHostEnvironment Environment { get; }



        public RealStateController(IHttpContextAccessor httpContextAccessor, IOptions<MyConfig> Config , IWebHostEnvironment environment)
        {
            if (httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                string Token = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(a => a.Type.Split('/').Last() == "primarysid").Value;
                RealStateManager = new RealStateManager(Config.Value.FirebaseClient, Token);

            }
            else
            {
                RealStateManager = new RealStateManager(Config.Value.FirebaseClient);

            }
            Environment = environment;



        }
      


        // GET: RealStateController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RealStateController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ViewModel.CreateRealState collection)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    string uniqueFileName, uploads, filePath;

                    if (collection.Images != null)
                    {
                        uniqueFileName = GetUniqueFileName(collection.Images.FileName);
                        uploads = Path.Combine(Environment.WebRootPath, "img/States");
                        filePath = Path.Combine(uploads, uniqueFileName);
                        collection.Images.CopyTo(new FileStream(filePath, FileMode.Create));

                    }
                    else
                    {
                        uniqueFileName = "DefuleImage.png";
                    }
                    await RealStateManager.AddState(new RealState {Discription= collection.Discription,Title=collection.Title,ImgName=uniqueFileName });

                }
                else
                {
                    return View();

                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }



        [Authorize]
        public async Task<ActionResult> Index(byte? Caller, int? pageNum = 1, int? pagesize = 5)
        {

            var Data = await RealStateManager.GetAllStates();
            (PaggingManger<RealState> PaggingData, int pagenumber, int Totalcount) = PaggingManger<RealState>.CreateWithPageNum(Data, (int)pagesize, (int)pageNum);
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
            var RealState = await RealStateManager.GetOneState(id);
            return View(RealState);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id, Messages data)
        {
            await RealStateManager.Delete(id);

            return RedirectToAction("Index");

        }




        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Details(string id)
        {
            var RealState = await RealStateManager.GetOneState(id);
            return View(RealState);

        }


        [Authorize(Roles = "Admin")]

        public async Task<ActionResult> Edit(string id)
        {
            var RealState = await RealStateManager.GetOneState(id);
            return View(new CreateRealState {Discription=RealState.Discription,Title=RealState.Title });

        }
        // POST: RealStateController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id,ViewModel.CreateRealState collection)
        {
            try
            {

                if (ModelState.IsValid )
                {
                    string uniqueFileName, uploads, filePath;
                    var RealState = await RealStateManager.GetOneState(id);
                    RealState.Title = collection.Title;
                    RealState.Discription = collection.Discription;

                    if (collection.Images != null)
                    {
                      
                        uniqueFileName = GetUniqueFileName(collection.Images.FileName);
                        uploads = Path.Combine(Environment.WebRootPath, "img/States");
                        filePath = Path.Combine(uploads, uniqueFileName);
                        collection.Images.CopyTo(new FileStream(filePath, FileMode.Create));

                    }
                    else
                    {
                        uniqueFileName = RealState.ImgName;
                    }
                    RealState.ImgName = uniqueFileName;
                   await RealStateManager.EditState(RealState,id);

                }
                else
                {

                    return View();

                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [NonAction]
        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }


    }
}
