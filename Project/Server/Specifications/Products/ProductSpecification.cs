using Server.Entities.Models;
using Server.Specifications.Base;

namespace Server.Specifications.Products;

public class ProductSpecification : Specification<Product, Product>
{
    public ProductSpecification(string? brand, string? category, string? sort)
        : base(p =>
            (string.IsNullOrEmpty(brand) || p.Brand == brand) &&
            (string.IsNullOrEmpty(category) || p.Category == category))
    {
        ApplySorting(sort);
    }

    private void ApplySorting(string? sort)
    {
        switch (sort?.ToLower())
        {
            case "price":
                ApplyOrderBy(p => p.Price);
                break;
            case "price_desc":
                ApplyOrderByDescending(p => p.Price);
                break;
            case "name":
                ApplyOrderBy(p => p.Name);
                break;
            case "name_desc":
                ApplyOrderByDescending(p => p.Name);
                break;
            default:
                ApplyOrderBy(p => p.Id);
                break;
        }
    }
}