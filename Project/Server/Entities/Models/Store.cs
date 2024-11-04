using Server.Enums;

namespace Server.Entities.Models;

public class Store:BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; }= string.Empty;
    public string Logo { get; set; }= string.Empty;
    public string Banner { get; set; }= string.Empty;
    public string OpenningHour { get; set; }= string.Empty;
    public string ClosingHour { get; set; }= string.Empty;
    public StoreStatus StoreStatus { get; set; } = StoreStatus.Active;
    public DateTimeOffset CreateAt { get; set; } = DateTimeOffset.Now;
    public int? SellerId { get; set; }
    public Address? Address { get; set; }
}