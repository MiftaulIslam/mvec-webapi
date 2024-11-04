using System.ComponentModel.DataAnnotations;

namespace Server.Entities.Models.Auth;

public class Login
{
    [Required]
    public string Email { get; set; }= string.Empty;

    [Required]
    [RegularExpression(@"^(?=.*\d)(?=.*[a-z]).{8,}$", 
        ErrorMessage = "Your password must contain at least 8 characters, including at least one digit and one lowercase letter. Please try again.")]
    public string Password { get; set; } = string.Empty;
}