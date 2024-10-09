using Microsoft.AspNetCore.Mvc;
using ECommerceApp.Data;
using ECommerceApp.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);
            if (existingUser != null)
            {
                TempData["SuccessMessage"] = "Login successful!";
                return RedirectToAction("Index", "Dashboard");
            }

            ModelState.AddModelError(string.Empty, "Invalid credentials");
            return View();
        }

        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signup(User user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = _context.Users.FirstOrDefault(u => u.Email == user.Email);
                if (existingUser == null)
                {
                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Signup successful! Please log in.";
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Email already exists");
                }
            }

            return View(user);
        }

        [HttpPost]
        public IActionResult Logout()
        {
            // Clear user session or authentication cookie here if needed
            TempData["SuccessMessage"] = "You have been logged out.";
            return RedirectToAction("Login");
        }
    }
}










//using Microsoft.AspNetCore.Mvc;
//using ECommerceApp.Data;
//using ECommerceApp.Models;
//using System.Linq;
//using System.Threading.Tasks;

//namespace ECommerceApp.Controllers
//{
//    public class AccountController : Controller
//    {
//        private readonly ApplicationDbContext _context;

//        public AccountController(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        [HttpGet]
//        public IActionResult Login()
//        {
//            return View();
//        }

//        [HttpPost]
//        public async Task<IActionResult> Login(User user)
//        {
//            var existingUser = _context.Users.FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);
//            if (existingUser != null)
//            {
//                // Redirect to Dashboard
//                return RedirectToAction("Index", "Dashboard");
//            }

//            ViewBag.Message = "Invalid credentials";
//            return View();
//        }

//        [HttpGet]
//        public IActionResult Signup()
//        {
//            return View();
//        }

//        [HttpPost]
//        public async Task<IActionResult> Signup(User user)
//        {
//            _context.Users.Add(user);
//            await _context.SaveChangesAsync();
//            return RedirectToAction("Login");
//        }
//    }
//}
