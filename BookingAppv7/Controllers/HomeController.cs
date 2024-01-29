using BookingAppv7.Models;
using BookingAppv7.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookingAppv7.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        

        // Get Action
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        //Post Action
        [HttpPost]
        public ActionResult Login(User u)
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                if (ModelState.IsValid)
                {
                    using (TestDbContext db = new TestDbContext())
                    {
                        var obj = db.Users.Where(a => a.UserName.Equals(u.UserName) && a.UserPassword.Equals(u.UserPassword)).FirstOrDefault();
                        if (obj != null)
                        {
                            HttpContext.Session.SetString("UserName", obj.UserName.ToString());
                            return RedirectToAction("Index");
                        }
                    }
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
            return View();
        }


        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserName");
            return RedirectToAction("Login");
        }


        [Authentication]
        public IActionResult Index()
        {
            return View();
        }

        [Authentication]
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
