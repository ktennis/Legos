using Legos.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Legos.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private ILegosRepository _repo;
        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
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
        //public IActionResult Products(int pageNum)
        //{
        //    int pageSize = userPageDisplayData;
        //    var Blah = new ProductsListViewModel
        //    {
        //        Products = _repo.Products
        //          .Skip((pageNum - 1) * pageSize)
        //          .Take(pageSize),

        //        PageinationInfo = new PaginationInfo
        //        {
        //            CurrentPage = pageNum,
        //            ItemsPerPage = pageSize,
        //            TotalItems = _repo.Projects.Count()
        //        }

        //    };
        //    return View(Blah);

        //}
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
