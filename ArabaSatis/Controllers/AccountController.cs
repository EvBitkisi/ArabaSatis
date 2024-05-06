using DataAccsessLayer.Data;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using ArabaPazarlama.Controllers;

namespace ArabaSatis.Controllers
{
    public class AccountController : Microsoft.AspNetCore.Mvc.Controller
    {
        public readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext Context)
        {
            _context = Context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Index", "Account");
            }

            return View("Index", user);
        }



        public async Task<IActionResult> Login(string Email, string Password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email.Equals(Email) && u.Password.Equals(Password));
            if (user != null)
            {
                HttpContext.Session.SetInt32("Id", user.UserId);
                HttpContext.Session.SetString("Name", user.Name);
                return Redirect("/Home/Index");
            }
            else
            {
                // Kullanıcı bilgileri eşleşmiyorsa veya yanlışsa, hata mesajıyla birlikte giriş sayfasına yönlendir
                TempData["LoginErrorMessage"] = "Email veya şifre yanlış. Lütfen tekrar deneyin.";
                return RedirectToAction("Index", "Account");
            }
        }



        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["LogoutMessage"] = "Oturum kapatıldı. Yönlendiriliyorsunuz...";
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
