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
            var productTypes = _legoRepo.Products
                .Select(p => p.Name)
                .Distinct();
            return View(productTypes);
        }
    }
}
