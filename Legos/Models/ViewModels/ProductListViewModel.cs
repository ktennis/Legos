namespace Legos.Models.ViewModels
{
    public class ProductListViewModel
    {
        public required IQueryable<Product> Products { get; set; }
        public PaginationInfo PaginationInfo { get; set; } = new PaginationInfo();
    }
}
