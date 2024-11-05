using System.ComponentModel.DataAnnotations;

namespace Server.Entities.Models.Auth;

public class Register
{
    [Required]
    public string Name { get; set; }= string.Empty;
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }= string.Empty;
    [Required]
    [RegularExpression(@"^(?=.*\d)(?=.*[a-z]).{8,}$", 
        ErrorMessage = "Your password must contain at least 8 characters, including at least one digit and one lowercase letter. Please try again.")]
    public string Password { get; set; }= string.Empty;
    [Required]
    [RegularExpression(@"^(?:\+?88)?01[3-9]\d{8}$", ErrorMessage = "Invalid phone number")]
    public string Phone { get; set; }= string.Empty;

}