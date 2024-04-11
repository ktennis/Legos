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
        
        public void DeleteProd(Product prod)
        {
            _context.Remove(prod);
            _context.SaveChanges();
        }
        
        public void AddUse(Customer cust)
        {
            _context.Add(cust);
            _context.SaveChanges();
        }
        
        public void EditUser(Customer updatedproduct)
        {
            _context.Attach(updatedproduct);
            _context.Entry(updatedproduct).CurrentValues.SetValues(updatedproduct);
            _context.Entry(updatedproduct).State = EntityState.Modified;

            _context.SaveChanges();
        }
        
        public void DeleteUse(Customer cust)
        {
            _context.Remove(cust);
            _context.SaveChanges();
        }
    }
}
