using Microsoft.Build.Evaluation;

namespace Legos.Models
{
    public interface ILegosRepository
    {
        public IQueryable<Product> Products { get; }
        public IQueryable<Customer> Customers { get; }

        public void EditProd(Product product);
        public void AddProd(Product product);
    }
}
