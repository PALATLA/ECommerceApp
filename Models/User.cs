using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
//Here we have done Validation part  for both the felids or columns
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(3, ErrorMessage = "Password must be at least 3 characters long")]
        public string Password { get; set; }
    }
}
