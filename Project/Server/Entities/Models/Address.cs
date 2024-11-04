namespace Server.Entities.Models;

public class Address:BaseEntity
{
    public string FullAddress { get; set; }= string.Empty;
    public string Region { get; set; }= string.Empty;
    public string City { get; set; }= string.Empty;
    public string Zone { get; set; }= string.Empty;
    public bool DefaultBilling { get; set; } = false;
    public bool DefaultShipping { get; set; } = false;
    public int? UserId { get; set; }
    public int? StoreId { get; set; }
}