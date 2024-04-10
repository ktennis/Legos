using Legos.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Legos.Models.ViewModels;

namespace Legos.Controllers
{
    public class HomeController : Controller
    {
        private ILegosRepository _repo;

        public HomeController(ILegosRepository temp)
        {
            _repo = temp;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Cart()
        {
            return View();
        }
        
        public IActionResult ProductDetail(int productId)
        {
            var product = _repo.Products.FirstOrDefault(p => p.ProductId == productId);
    
            if (product == null)
            {
                // Handle case where product with given ID is not found
                return RedirectToAction("Index"); // Redirect to home or appropriate page
            }

            return View(product);
        }
        public IActionResult ChangePageSize(int pageSize)
        {
            // Update the page size in session or any other storage mechanism
            // For now, I'll assume you're updating it in session
            HttpContext.Session.SetInt32("PageSize", pageSize);

            // Redirect back to the Products action to reload the page with the new page size
            return RedirectToAction("Products", new { pageNum = 1 });
        }


        //public IActionResult Products(int pageNum)
        //{
        //    int pageSize;
        //    var ProductData = new ProductListViewModel
        //    {
        //        Products = _repo.Products
        //          .Skip((pageNum - 1) * pageSize)
        //          .Take(pageSize),

        //        PaginationInfo = new PaginationInfo
        //        {
        //            CurrentPage = pageNum,
        //            ItemsPerPage = pageSize,
        //            TotalItems = _repo.Products.Count()
        //        }

        //    };
        //    return View(ProductData);

        //}
        public IActionResult Products(int pageNum, int pageSize = 5)
        {
            var ProductData = new ProductListViewModel
            {
                Products = _repo.Products
                    .Skip((pageNum - 1) * pageSize)
                    .Take(pageSize),

                PaginationInfo = new PaginationInfo
                {
                    CurrentPage = pageNum,
                    ItemsPerPage = pageSize,
                    TotalItems = _repo.Products.Count()
                }
            };

            //// Store the selected page size in session
            //HttpContext.Session.SetInt32("PageSize", pageSize);

            return View(ProductData);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult OrderConfirmation()
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
