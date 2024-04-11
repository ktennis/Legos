namespace Legos.Models.ViewModels
{
    public class OrdersViewModel
    {
        public IQueryable<Order> Orders { get; set; }
        public PaginationInfo PaginationInfo { get; set; } = new PaginationInfo();
    }
}
