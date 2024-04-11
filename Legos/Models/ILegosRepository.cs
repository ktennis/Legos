using Microsoft.Build.Evaluation;

namespace Legos.Models
{
    public interface ILegosRepository
    {
        public IQueryable<Product> Products { get; }
        public IQueryable<Customer> Customers { get; }
        public IQueryable<Order> Orders { get; }

        public void EditProd(Product product);
        public void EditUser(Customer customer);
        public void AddProd(Product product);
        public void AddUse(Customer customer);
        public void DeleteProd(Product product);
        public void DeleteUse(Customer customer);
    }
}
