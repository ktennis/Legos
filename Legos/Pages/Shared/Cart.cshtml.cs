using Legos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Legos.Infastructure;

namespace Legos.Pages.Shared;

public class CartModel : PageModel
{
    private ILegosRepository _repo;

    public CartModel(ILegosRepository temp)
    //public CartModel(ILegosRepository temp, Cart cartService)

    {
        _repo = temp;
    }
    public Cart? Cart { get; set; }
    public string ReturnUrl { get; set; } = "/";

    public void OnGet(string returnUrl)
    {
        ReturnUrl = returnUrl ?? "/";
        //Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
    }
    public IActionResult OnPost(int productId, string returnUrl)
    {
        Product p = _repo.Products
            .FirstOrDefault(x => x.ProductId == productId);

        if (p != null)
        {
            //Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
            Cart.AddItem(p, 1);
            //HttpContext.Session.SetJson("cart", Cart);
        }

        return RedirectToPage(new { returnUrl });
    }
}
