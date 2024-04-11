using Microsoft.EntityFrameworkCore;


namespace Legos.Models
{
    public class EFLegosRepository : ILegosRepository
    {
        private AurorasBricksContext _context;
        public EFLegosRepository(AurorasBricksContext temp) 
        {
            _context = temp;
        }
        public IQueryable<Product> Products => _context.Products;
        public IQueryable<Customer> Customers => _context.Customers;

        public void AddProd(Product product)
        {
            _context.Add(product);
            _context.SaveChanges();
        }

        public void EditProd(Product updatedproduct)
        {
            _context.Attach(updatedproduct);
            _context.Entry(updatedproduct).CurrentValues.SetValues(updatedproduct);
            _context.Entry(updatedproduct).State = EntityState.Modified;

            _context.SaveChanges();
        }
        
        // public void EditProd(Product product)
        // {
        //     var existingProduct = _context.Products.FirstOrDefault(x => x.ProductId == product.ProductId);
        //     if (existingProduct != null)
        //     {
        //         existingProduct.Name = product.Name;
        //         //saving the rest of the fields
        //         existingProduct.Year = product.Year;
        //         existingProduct.NumParts = product.NumParts;
        //         existingProduct.Price = product.Price;
        //         existingProduct.ImgLink = product.ImgLink;
        //         existingProduct.PrimaryColor = product.PrimaryColor;
        //         existingProduct.SecondaryColor = product.SecondaryColor;
        //         existingProduct.Description = product.Description;
        //         existingProduct.Category = product.Category;
        //
        //         //end rest of fields
        //         _context.SaveChanges();
        //     }
        // }
    }
}
