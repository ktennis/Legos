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

        public IActionResult ProductDetail()
        {
            return View();
        }
        //public IActionResult Products()
        //{
        //    var productData = _repo.Products;
        //    return View(productData);
        //}

        //with Pagination
        public IActionResult Products(int pageNum)
        {
            int pageSize = 5;
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
