using Legos.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using Legos.Models.ViewModels;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML;

namespace Legos.Controllers
{
    public class HomeController : Controller
    {
        private ILegosRepository _repo;
        public HomeController(ILegosRepository temp)
        {
            _repo = temp;
        }
        //private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public async Task<IActionResult> Index()
        {

            //var user = await GetCurrentUserAsync();
            var userName = User.Identity.Name;
            var user = _repo.Customers.FirstOrDefault(c => c.Email == userName);
            if (User.IsInRole("Admin"))
            {
                return Redirect("~/Admin/AdminIndex");
            }
            else
            {
                if (User != null && User.IsInRole("User"))
                {
                    var customer = _repo.Customers.FirstOrDefault(c => c.Email == user.Email);

                    var mostRecentTransaction = _repo.Orders
                        .Where(o => o.CustomerId == customer.CustomerId)
                        .OrderByDescending(o => o.Date)
                        .FirstOrDefault();

                    if (mostRecentTransaction != null)
                    {
                        // Get the product ID from line items of the most recent transaction
                        var productId = _repo.LineItems
                            .Where(li => li.TransactionId == mostRecentTransaction.TransactionId)
                            .Select(li => li.ProductId)
                            .FirstOrDefault();

                        // Get the product information
                        var productInfo = _repo.Products
                            .Where(p => p.ProductId == productId)
                            .Select(p => new
                            {
                                p.rec_1,
                                p.rec_2,
                                p.rec_3,
                                p.rec_4,
                                p.rec_5
                            })
                            .FirstOrDefault();
                    }
                    else
                    {
                        // Handle case where there are no transactions for the customer
                    }
                }
                return View();
            }
            
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Cart()
        {
            return View();
        }

        //private readonly UserManager<IdentityUser> _userManager;

        //public HomeController(UserManager<IdentityUser> userManager, ILegosRepository repo)
        //{
        //    _userManager = userManager;
        //    _repo = repo;
        //}
        //public async Task<IActionResult> CheckoutFraud()
        //{
        //    // Get the current user
        //    var currentUser = await _userManager.GetUserAsync(User);

        //    if (currentUser != null)
        //    {
        //        // Get the user's email
        //        string userEmail = currentUser.Email;

        //        // Query the database to find the corresponding customer using email
        //        var customer = _repo.Customers.FirstOrDefault(c => c.Email == userEmail);

        //        if (customer != null)
        //        {
        //            // Populate the model with customer information
        //            var viewModel = new CheckoutFraud
        //            {
        //                // Populate other properties as needed
        //                first_name = customer.FirstName,
        //                last_name = customer.LastName,
        //                // Populate other properties as needed
        //            };

        //            // Pass the model to the view
        //            return View(viewModel);
        //        }
        //    }

        //    // Handle case where user is not found or has no corresponding customer
        //    // Redirect to an error page or handle it as appropriate for your application
        //    return RedirectToAction("Error");
        //}
    


    //public IActionResult CheckoutFraud() 
    //    {
    //        return View();
    //    }


       
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
        public IActionResult Products(string productTypes, string productCat, int pageNum, int pageSize = 5)
        {
            var ProductData = new ProductListViewModel
            {
                Products = _repo.Products
                    .Skip((pageNum - 1) * pageSize)
                    .Where(x => !(productTypes != null && !productTypes.Contains(x.PrimaryColor)) && (productCat == null || productCat.Contains(x.Category)))
                    .Take(pageSize),

                PaginationInfo = new PaginationInfo
                {
                    CurrentPage = pageNum,
                    ItemsPerPage = pageSize,
                    TotalItems = productTypes == null && productCat == null ? _repo.Products.Count() : _repo.Products.Where(x => x.PrimaryColor == productTypes && x.Category == productCat).Count()
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
