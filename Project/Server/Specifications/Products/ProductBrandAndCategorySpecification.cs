using Server.Entities.Models;
using Server.Specifications.Base;

namespace Server.Specifications.Products;

public class ProductBrandAndCategorySpecification : Specification<Product, object>
{
    public ProductBrandAndCategorySpecification()
        : base(p => true)
    {
        AddSelect(p => new { p.Brand, p.Category });
    }
}
