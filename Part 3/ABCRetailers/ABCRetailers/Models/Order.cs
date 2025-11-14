using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Azure;
using Azure.Data.Tables;

namespace ABCRetailers.Models
{
   
    public class Order : ITableEntity
    {
        // ITableEntity properties
        [NotMapped]
        public string PartitionKey { get; set; } = "Orders";

        [NotMapped]
        public string RowKey { get; set; } = Guid.NewGuid().ToString();

        [NotMapped]
        public DateTimeOffset? Timestamp { get; set; }

        [NotMapped]
        public ETag ETag { get; set; }

        // Calculated property
        [Display(Name = "Order ID")]
        public string OrderId => RowKey;

        [Required]
        [Display(Name = "Customer")]
        public string CustomerId { get; set; } = string.Empty;

        [Display(Name = "Username")]
        public string Username { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Product")]
        public string ProductId { get; set; } = string.Empty;

        [Display(Name = "Product Name")]
        public string ProductName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Order Date")]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; } = DateTime.Today;

        [Required]
        [Display(Name = "Quantity")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }

        [Display(Name = "Unit Price")]
        [DataType(DataType.Currency)]
        public decimal UnitPrice { get; set; }

        // calculated property
        [Display(Name = "Total Price")]
        [DataType(DataType.Currency)]
        public decimal TotalPrice => Quantity * UnitPrice; // auto-calculated

        [Required]
        [Display(Name = "Status")]
        public string Status { get; set; } = "Submitted";
    }
}
