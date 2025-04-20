using Microsoft.AspNetCore.Mvc;
using EventNest.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace EventNest.Controllers
{
    public class AccountController : Controller
    {
        private static List<User> _users = new()
        {
            new User { Username = "admin", Password = "admin123", FullName = "Admin User", Email = "admin@example.com" },
            new User { Username = "harsha", Password = "12345", FullName = "Harsha Jagu", Email = "harsha@eventnest.com" }
        };

        public IActionResult SignIn() => View();

        [HttpPost]
        public IActionResult SignIn(string username, string password)
        {
            var user = _users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user == null)
            {
                ViewBag.Error = "Invalid username or password.";
                return View();
            }

            HttpContext.Session.SetString("user", username);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult SignUp() => View();

        [HttpPost]
        public IActionResult SignUp(string fullname, string email, string username, string password, string confirmpass)
        {
            if (_users.Any(u => u.Username == username))
            {
                ViewBag.Error = "Username already exists.";
                return View();
            }

            if (password != confirmpass)
            {
                ViewBag.Error = "Passwords do not match.";
                return View();
            }

            _users.Add(new User
            {
                Username = username,
                Password = password,
                FullName = fullname,
                Email = email
            });

            HttpContext.Session.SetString("user", username);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("user");
            return RedirectToAction("SignIn");
        }
    }
}

