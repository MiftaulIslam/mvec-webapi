using Server.Entities.Models;
using Server.Specifications.Base;

namespace Server.Specifications.Products;

public class ProductCategorySpecification : Specification<Product, string>
{
    public ProductCategorySpecification()
        : base(p => true)
    {
        AddSelect(p => p.Category);
    }
}
