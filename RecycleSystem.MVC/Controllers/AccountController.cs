using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecycleSystem.Data.Data.LoginDTO;
using RecycleSystem.IService;

namespace RecycleSystem.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginInput loginInput)
        {
            if (ModelState.IsValid)
            {
                LoginOutput login = _accountService.Login(loginInput);
                HttpContext.Session.SetString("UserName", login.UserName);
                HttpContext.Session.SetString("UserId", login.UserId);
                return RedirectToAction("Index", "Main");
            }
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
