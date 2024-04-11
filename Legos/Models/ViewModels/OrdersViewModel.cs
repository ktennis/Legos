namespace Legos.Models.ViewModels
{
    public class OrdersViewModel
    {
        public Order Orders { get; set; }
        public string Prediction { get; set; }
        public PaginationInfo PaginationInfo { get; set; } = new PaginationInfo();
    }
}
