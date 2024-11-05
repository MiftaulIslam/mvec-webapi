using System.ComponentModel.DataAnnotations;
using Server.Enums;

namespace Server.Entities.Models;

public class Seller:BaseEntity
{
    
    public string Name { get; set; } = string.Empty;
    [EmailAddress]
    public string Email { get; set; }= string.Empty;
    
    [RegularExpression(@"/^(?:\+?88)?01[13-9]\d{8}$/gm", ErrorMessage = "Invalid phone number")]
    public string Phone { get; set; }= string.Empty;
    public Gender Gender { get; set; }
    public string Role { get; set; } = "Seller";
    public string Image { get; set; }= string.Empty;
    public DateTimeOffset CreateAt { get; set; } = DateTimeOffset.Now;
    public int? StoreId { get; set; }

}