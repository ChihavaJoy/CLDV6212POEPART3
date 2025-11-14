using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABCRetailers.Models
{
    [Table("Orders")] // SQL table
    public class OrderEntity
    {
        [Key] // EF Core needs a primary key
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string CustomerUsername { get; set; } = string.Empty;

        [Required]
        public string ProductId { get; set; } = string.Empty;

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        [NotMapped] // calculated, not stored in SQL
        public decimal TotalPrice => Quantity * UnitPrice;
    }
}
