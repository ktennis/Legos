using Legos.Models;
using Legos.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Drawing.Printing;
using Microsoft.EntityFrameworkCore;

namespace Legos.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private ILegosRepository _repo;
        public AdminController(ILegosRepository temp)
        {
            _repo = temp;
        }

        public IActionResult AdminPrivacy()
        {
            return View();
        }
        public IActionResult Cart()
        {
            return View();
        }
        
        public IActionResult AdminProductDetail(int productId)
        {
            var product = _repo.Products.FirstOrDefault(p => p.ProductId == productId);
    
            if (product == null)
            {
                // Handle case where product with given ID is not found
                return RedirectToAction("AdminIndex"); // Redirect to home or appropriate page
            }

            return View(product);
        }
        
        // public IActionResult OrderConfirmation()
        // {
        //     return View();
        // }

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
            int pageSize = 800;
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
        
        [HttpGet]
        public IActionResult EditUser(int id) // get the info for the edits and go back to form to edit them
        {
            var UserToEdit = _repo.Customers.FirstOrDefault(x => x.CustomerId == id);
            if (UserToEdit != null)
            {
                //ViewBag.Categories = _repo.Categories.OrderBy(x => x.CategoryName).ToList();
                return View("AddUser", UserToEdit);
            }
            return RedirectToAction("AdminUsers");
        }
        
        [HttpPost]
        public IActionResult EditUser(Customer updatedInfo) //save the updated form. return to table
        {
            _repo.EditUser(updatedInfo);

            return RedirectToAction("AdminUsers");
        }
        
        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult AddUser(Customer cust)
        {
            _repo.AddUse(cust);
            return View("UserConfirmation");
        }
        
        [HttpGet]
        public IActionResult DeleteUserConfirmation(int id)
        {
            var UserToDelete = _repo.Customers.FirstOrDefault(x => x.CustomerId == id);
            if (UserToDelete == null)
            {
                return NotFound();
            }

            return View(UserToDelete);
        }

        public IActionResult DeleteUse(int id)
        {
            var UserToDelete = _repo.Customers.FirstOrDefault(x => x.CustomerId == id);
            if (UserToDelete != null)
            {
                //ViewBag.Categories = _repo.Categories.OrderBy(x => x.CategoryName).ToList();
                _repo.DeleteUse(UserToDelete);
            }
            return RedirectToAction("AdminUsers");
        }

        public IActionResult AdminReviewOrders()
        {
            return View();
        }
        
        public IActionResult AdminProducts(int pageNum)
        {
            int pageSize = 10;
            var ProductData = new ProductListViewModel()
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
        
        [HttpGet]
        public IActionResult EditProduct(int id)
        {
            var taskToEdit = _repo.Products.FirstOrDefault(x => x.ProductId == id);
            if (taskToEdit != null)
            {
                //ViewBag.Categories = _repo.Categories.OrderBy(x => x.CategoryName).ToList();
                return View("AddProduct", taskToEdit);
            }
            return RedirectToAction("AdminProducts");
        }
        
        [HttpPost]
        public IActionResult EditProduct(Product updatedInfo) //save the updated form. return to table
        {
            _repo.EditProd(updatedInfo);

            return RedirectToAction("AdminProducts");
        }
        
        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            _repo.AddProd(product);
            return View("Confirmation");
        }
        
        [HttpGet]
        public IActionResult DeleteConfirmation(int id)
        {
            var productToDelete = _repo.Products.FirstOrDefault(x => x.ProductId == id);
            if (productToDelete == null)
            {
                return NotFound();
            }

            return View(productToDelete);
        }

        public IActionResult DeleteProduct(int id)
        {
            var prodToDelete = _repo.Products.FirstOrDefault(x => x.ProductId == id);
            if (prodToDelete != null)
            {
                //ViewBag.Categories = _repo.Categories.OrderBy(x => x.CategoryName).ToList();
                _repo.DeleteProd(prodToDelete);
            }
            return RedirectToAction("AdminProducts");
        }
        
        public IActionResult AdminProductsView(string productTypes, string productCat, int pageNum, int pageSize = 5)
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
        
        public IActionResult AdminAbout()
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
