using Legos.Models;
using Legos.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Drawing.Printing;

namespace Legos.Controllers
{
    //[Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private ILegosRepository _repo;
        public AdminController(ILegosRepository temp)
        {
            _repo = temp;
        }

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

        public IActionResult AdminUsers(int pageNum)
        {
            int pageSize = 5;
            var CustomerData = new CustomerListViewModel
            {
                Customers = _repo.Customers
                  .Skip((pageNum - 1) * pageSize)
                  .Take(pageSize),

                PaginationInfo = new PaginationInfo
                {
                    CurrentPage = pageNum,
                    ItemsPerPage = pageSize,
                    TotalItems = _repo.Customers.Count()
                }

            };
            return View(CustomerData);

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
