using Microsoft.AspNetCore.Mvc;
using Legos.Models;
namespace Legos.Components
{
    public class productCategoryViewComponent : ViewComponent
    {
        private ILegosRepository _legoRepo;
        public productCategoryViewComponent(ILegosRepository temp)
        {
            _legoRepo = temp;
        }
        public IViewComponentResult Invoke()
        {
            var productCat = _legoRepo.Products
                 .Select(p => p.Category)
                .Distinct();
            return View(productCat);

        }
    }
}
