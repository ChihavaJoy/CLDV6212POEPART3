using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;

namespace ABCRetailers.Functions.Models
{
    public record CustomerDto(string Id, string Name, string Surname, string Username, string Email, string ShippingAddress);
    public record ProductDto(string Id, string ProductName, string Description, decimal Price, int StockAvailable, string ImageUrl);
    public record OrderDto(
        string Id, 
        string CustomerId, 
        string ProductId, 
        string ProductName,
        int Quantity, 
        decimal UnitPrice, 
        decimal TotalAmount, 
        DateTimeOffset OrderDateUtc, 
        string Status,
        string Username);
}