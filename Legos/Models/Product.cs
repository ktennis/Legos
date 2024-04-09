using System.ComponentModel.DataAnnotations;

namespace Legos.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public int ProductName { get; set; }
    }
}
