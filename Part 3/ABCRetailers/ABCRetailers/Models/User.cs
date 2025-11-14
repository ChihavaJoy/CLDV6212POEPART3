using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Azure;

namespace ABCRetailers.Models
{
    [Table("Users")]//Sql Table name
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(100)]
        [Display(Name = "Username")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage ="Password is required")]
        [StringLength(256)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string PasswordHash { get; set; } = string.Empty; // store hashed later

        [Required]
        [StringLength(20)]
        public string Role { get; set; } = "Customer"; // Admin or Customer

        [NotMapped] // EF Core will ignore this property
        public ETag ETag { get; set; }
    }
}
