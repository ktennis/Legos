using Legos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Legos.Views.Home
{
    public class CartModel : PageModel
    {
        private ILegosRepository _repo;

        public CartModel(ILegosRepository temp)
        {
            _repo = temp;
        }
        public Cart? Cart { get; set; }

        public void OnGet()
        {
        }
        public void OnPost(int productId) 
        { 
            Product p = _repo.Products
                .FirstOrDefault(x => x.ProductId == productId);
            
            Cart = new Cart();

            Cart.AddItem(p, 1);
        }
    }

}
