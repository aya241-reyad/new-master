using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Miar.Manager;
using Miar.Models;
using Specialist_medical.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Miar.Controllers
{
    public class AccountController : Controller
    {
        public AccountManager AccountManager { get; private set; }

        public AccountController(IOptions<MyConfig> Config)
        {
            AccountManager = new AccountManager(Config.Value.FirebaseClient);

        }
        // GET: AccountController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AccountController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AccountController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AccountController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AccountController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AccountController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AccountController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }



        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(Account user, string ReturnUrl)
        {

            if (ModelState.IsValid)
            {
                string Auth = await AccountManager.Login(user);

                if (Auth != "NotFound")
                {
                    string Role = user.Email.Split("@")[0];
                    List<Claim> claims = new List<Claim> { 
                        new Claim(ClaimTypes.Name,user.Email),
                        new Claim(ClaimTypes.Role, Role), 
                        new Claim(ClaimTypes.PrimarySid, Auth)
                    };


                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    


                    if (!string.IsNullOrEmpty(ReturnUrl)) 
                    {
                        var action = ReturnUrl.Split('/');
                        return RedirectToAction(action[2],action[1]);
                    }

                    return RedirectToAction("index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "الاميل او كلمه المرور غير صحيحه");
                    return View();


                }

            }
            return View();
        }




        public async Task<ActionResult> Logout()
        {

            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
