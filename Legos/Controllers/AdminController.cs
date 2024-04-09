using Legos.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Legos.Controllers
{
    //[Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public AdminController(ILogger<AdminController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Cart()
        {
            return View();
        }

        public IActionResult ProductDetail()
        {
            return View();
        }
        public IActionResult Products()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }

        public IActionResult OrderConfirmation()
        {
            return View();
        }

        public IActionResult AdminIndex()
        {
            return View();
        }
        public IActionResult DeleteConfirmation()
        {
            return View();
        }

        public IActionResult AdminUsers()
        {
            return View();
        }
        
        public IActionResult AdminReviewOrders()
        {
            return View();
        }

        public IActionResult AddUser()
        {
            return View();
        }
        public IActionResult AdminProducts()
        {
            return View();
        }
        public IActionResult AddProduct()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
