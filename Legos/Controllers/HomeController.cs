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
using Microsoft.AspNetCore.Http;
using Legos.Infastructure;
using Microsoft.AspNetCore.Authorization;

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
            
            if (User.IsInRole("Admin"))
            {
                return Redirect("~/Admin/AdminIndex");
            }
            else
            {
                if (User != null && User.IsInRole("User"))
                {
                    var userName = User.Identity.Name;
                    var customerId = _repo.Customers
                        .Where(c => c.Email == userName)
                        .Select(c => c.CustomerId)
                        .FirstOrDefault();

                    int limit = 1;
                    var mostRecentTransaction = _repo.Orders
                        .Where(o => o.CustomerId == customerId)
                        .OrderByDescending(o => o.TransactionId)
                        .Take(limit)
                        .Select(o => o.TransactionId)
                        .FirstOrDefault();

                    if (mostRecentTransaction == null || mostRecentTransaction == 0)
                    {
                        return View();
                        
                    }
                    else
                    {
                        // Get the product ID from line items of the most recent transaction
                        var productId = _repo.LineItems
                            .Where(li => li.TransactionId == mostRecentTransaction)
                            .Take(limit)
                            .Select(li => li.ProductId)
                            .FirstOrDefault();

                        // Get the product information
                        var product = _repo.Products.FirstOrDefault(p => p.ProductId == productId);

                        if (product == null)
                        {
                            return View();
                        }

                        var recommendedProducts = new List<Product>();

                        foreach (var recId in new[] { product.Rec1, product.Rec2, product.Rec3, product.Rec4, product.Rec5 })
                        {
                            var recommendedProduct = _repo.Products.FirstOrDefault(p => p.ProductId == recId);
                            if (recommendedProduct != null)
                            {
                                recommendedProducts.Add(recommendedProduct);
                            }
                        }

                        var viewModel = new ProductDetailsViewModel
                        {
                            RecommendedProduct1 = recommendedProducts.ElementAtOrDefault(0),
                            RecommendedProduct2 = recommendedProducts.ElementAtOrDefault(1),
                            RecommendedProduct3 = recommendedProducts.ElementAtOrDefault(2),
                            RecommendedProduct4 = recommendedProducts.ElementAtOrDefault(3),
                            RecommendedProduct5 = recommendedProducts.ElementAtOrDefault(4)
                        };

                        return View(viewModel);
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
        //                first_name = customer.Firstname,
        //                last_name = customer.Lastname,
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

            var recommendedProducts = new List<Product>();

            foreach (var recId in new[] { product.Rec1, product.Rec2, product.Rec3, product.Rec4, product.Rec5 })
            {
                var recommendedProduct = _repo.Products.FirstOrDefault(p => p.ProductId == recId);
                if (recommendedProduct != null)
                {
                    recommendedProducts.Add(recommendedProduct);
                }
            }

            var viewModel = new ProductDetailsViewModel
            {
                Product = product,
                RecommendedProduct1 = recommendedProducts.ElementAtOrDefault(0),
                RecommendedProduct2 = recommendedProducts.ElementAtOrDefault(1),
                RecommendedProduct3 = recommendedProducts.ElementAtOrDefault(2),
                RecommendedProduct4 = recommendedProducts.ElementAtOrDefault(3),
                RecommendedProduct5 = recommendedProducts.ElementAtOrDefault(4)
            };

            return View(viewModel);
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
            if (pageNum < 1)
            {
                pageNum = 1;
            }
            var ProductData = new ProductListViewModel
            {
                Products = _repo.Products
                        .Skip((pageNum - 1) * pageSize)
                        .Where(x => !(productTypes != null && !productTypes.Contains(x.Primarycolor)) && (productCat == null || productCat.Contains(x.Category)))
                        .Take(pageSize),

                PaginationInfo = new PaginationInfo
                {
                    CurrentPage = pageNum,
                    ItemsPerPage = pageSize,
                    TotalItems = productTypes == null && productCat == null ? _repo.Products.Count() : _repo.Products.Where(x => x.Primarycolor == productTypes && x.Category == productCat).Count()
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
        [Authorize(Roles = "User,Admin")]
        public IActionResult CheckoutFraud()
        {
            var cart = GetCart(HttpContext.RequestServices); // Retrieve the cart somehow

            // Calculate the total amount from the cart
            var cartTotalAmount = cart.CalculateTotal();

            // Create the CheckoutFraud model instance and populate its properties
            var checkoutFraud = new CheckoutFraud
            {
                // Populate other properties as needed
                Cart = cart,
                CartTotalAmount = cartTotalAmount
            };

            return View(checkoutFraud);
        }

        public SessionCart GetCart(IServiceProvider services)
        {
            // Retrieve the session from the provided services
            ISession session = services.GetRequiredService<IHttpContextAccessor>()
                                       .HttpContext?.Session;

            // Get the cart from session or create a new one if it doesn't exist
            SessionCart cart = session?.GetJson<SessionCart>("Cart") ?? new SessionCart();

            // Assign the session to the cart for future updates
            cart.Session = session;

            return cart;
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}