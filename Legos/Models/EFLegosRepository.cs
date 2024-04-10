

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
    }
}
