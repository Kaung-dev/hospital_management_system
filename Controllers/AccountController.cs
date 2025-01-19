using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly HospitalDbContext _context;
        private readonly PasswordHasher<string> _passwordHasher = new();

        public AccountController(HospitalDbContext context)
        {
            _context = context;
        }

        // GET: Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(User model)
        {
            if (ModelState.IsValid)
            {
                // Check if the email is already registered
                if (_context.Users.Any(u => u.Email == model.Email))
                {
                    ModelState.AddModelError("Email", "Email is already in use.");
                    return View(model);
                }

                // Hash the password and save the user
                model.Password = _passwordHasher.HashPassword(null, model.Password);
                _context.Users.Add(model);
                _context.SaveChanges();

                TempData["success"] = "Registration successful. Please log in.";
                return RedirectToAction("Login");
            }
            return View(model);
        }

        // Existing Login methods (for context)
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user != null && _passwordHasher.VerifyHashedPassword(null, user.Password, password) == PasswordVerificationResult.Success)
            {
                // Store session data
                HttpContext.Session.SetString("UserId", user.Id.ToString());
                HttpContext.Session.SetString("UserName", user.Name);

                // Redirect to HomeController's Index action
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View();
        }


        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("UserId") == null)
                return RedirectToAction("Login");

            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
