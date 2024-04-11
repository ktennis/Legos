using Microsoft.Build.Evaluation;

namespace Legos.Models;

public class Cart
{
    public List<CartLine> Lines { get; set; } = new List<CartLine>();
    public virtual void AddItem(Product p, int quantity)
    {
        CartLine? Line = Lines
            .Where(x => x.Product.ProductId == p.ProductId)
            .FirstOrDefault();

        if (Line == null)
        {
            Lines.Add(new CartLine
            {
                Product = p,
                Quantity = quantity
            });
        }
        else
        {
            Line.Quantity += quantity;
        }
    }

    public virtual void RemoveLine(Product p) => Lines.RemoveAll(x => x.Product.ProductId == p.ProductId);

    public virtual void Clear() => Lines.Clear();
    public decimal CalculateTotal() => (decimal)Lines.Sum(x => x.Product.Price * x.Quantity);
    public class CartLine
    {
        public int CartLineId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
