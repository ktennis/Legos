using Legos.Models;
using Legos.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Drawing.Printing;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.EntityFrameworkCore.Design.Internal;
using Microsoft.ML.OnnxRuntime.Tensors;
using Microsoft.ML.OnnxRuntime;
using Microsoft.AspNetCore.Http;

namespace Legos.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private ILegosRepository _repo;
        private readonly InferenceSession _session;
        private readonly string _onnxModelPath;
        public AdminController(ILegosRepository temp, IHostEnvironment hostEnvironement)
        {
            _repo = temp;

            _onnxModelPath = System.IO.Path.Combine(hostEnvironement.ContentRootPath, "FraudModelDec.onnx");

            _session = new InferenceSession(_onnxModelPath);
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

        //public IActionResult AdminReviewOrders(int pageNum)
        //{
        //    int pageSize = 10;
        //    var OrderData = new OrdersViewModel()
        //    {
        //        Orders = _repo.Orders
        //            .Skip((pageNum - 1) * pageSize)
        //            .Take(pageSize),

        //        PaginationInfo = new PaginationInfo
        //        {
        //            CurrentPage = pageNum,
        //            ItemsPerPage = pageSize,
        //            TotalItems = _repo.Products.Count()
        //        }

        //    };
        //    return View(OrderData);
        //}

        public IActionResult AdminReviewOrders(int pageNum)
        {

            var records = _repo.Orders
                .OrderByDescending(o => o.Date)
                .Take(200)
                .ToList();
            var predictions = new List<OrdersViewModel>();
            var class_type_dict = new Dictionary<int, string>
                {
                    { 0, "Not Fraud"},
                    {1, "Fraud" }
                };

            foreach (var record in records)
            {
                var january1_2022 = new DateTime(2022, 1, 1);
                var dateString = record.Date;

                string[] dateComponents = dateString.Split('/');
                int day = int.Parse(dateComponents[1]);
                int month = int.Parse(dateComponents[0]);
                int year = int.Parse(dateComponents[2]);

                var recordDate = new DateTime(year, month, day);

                //DateTime date = DateTime.ParseExact(record.Date, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                var daysSinceJan12022 = Math.Abs((recordDate - january1_2022).Days);
                float amount = float.Parse(record.Amount);

                var input = new List<float>
                        {
                            //(float)record.CustomerId,
                            //(float)record.Time,
                            //amount,
                            daysSinceJan12022,

                            //dummy codes

                            record.DayOfWeek == "Mon" ? 1 : 0,
                            record.DayOfWeek == "Sat" ? 1 : 0,
                            record.DayOfWeek == "Sun" ? 1 : 0,
                            record.DayOfWeek == "Thu" ? 1 : 0,
                            record.DayOfWeek == "Tue" ? 1 : 0,
                            record.DayOfWeek == "Wed" ? 1 : 0,

                            record.EntryMode == "Pin" ? 1 : 0,
                            record.EntryMode == "Tap" ? 1 : 0,

                            record.TypeOfTransaction == "Online" ? 1 : 0,
                            record.TypeOfTransaction == "POS" ? 1 : 0,

                            record.CountryOfTransaction == "India" ? 1 : 0,
                            record.CountryOfTransaction == "Russia" ? 1 : 0,
                            record.CountryOfTransaction == "USA" ? 1 : 0,
                            record.CountryOfTransaction == "United Kingdom" ? 1 : 0,

                            record.ShippingAddress == "India" ? 1 : 0,
                            record.ShippingAddress == "Russia" ? 1 : 0,
                            record.ShippingAddress == "USA" ? 1 : 0,
                            record.ShippingAddress == "United Kingdom" ? 1 : 0,

                            record.Bank == "HSBC" ? 1 : 0,
                            record.Bank == "Halifax" ? 1 : 0,
                            record.Bank == "Lloyds" ? 1 : 0,
                            record.Bank == "Metro" ? 1 : 0,
                            record.Bank == "Monzo" ? 1 : 0,
                            record.Bank == "RBS" ? 1 : 0,

                            record.TypeOfCard == "Visa" ? 1 : 0
                        };
                var inputTensor = new DenseTensor<float>(input.ToArray(), new[] { 1, input.Count });

                var inputs = new List<NamedOnnxValue>
                    {
                        NamedOnnxValue.CreateFromTensor("float_input", inputTensor)
                    };

                string predictionResult;
                using (var results = _session.Run(inputs))
                {
                    var prediction = results.FirstOrDefault(item => item.Name == "output_label")?.AsTensor<long>().ToArray();
                    predictionResult = prediction != null && prediction.Length > 0 ? class_type_dict.GetValueOrDefault((int)prediction[0], "Unknown") : "Error in prediction";
                }

                predictions.Add(new OrdersViewModel { Orders = record, Prediction = predictionResult });
            };

            //int pageSize = 10;
            //var OrderData = new OrdersViewModel()
            //{
            //    Orders = (Order)_repo.Orders
            //        .Skip((pageNum - 1) * pageSize)
            //        .Take(pageSize),

            //    PaginationInfo = new PaginationInfo
            //    {
            //        CurrentPage = pageNum,
            //        ItemsPerPage = pageSize,
            //        TotalItems = _repo.Products.Count()
            //    }

            //};
            return View(predictions);

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
