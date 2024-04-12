using Microsoft.AspNetCore.Mvc;
using Legos.Models;
namespace Legos.Components
{
    public class ProductTypesViewComponent : ViewComponent
    {
        private ILegosRepository _legoRepo;
        public ProductTypesViewComponent(ILegosRepository temp) 
        {
            _legoRepo = temp;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedProductType = RouteData?.Values["productType"];
            var productTypes = _legoRepo.Products
                 .Select(p => p.Primarycolor)
                .Distinct();
            return View(productTypes);

        }
    }
}
