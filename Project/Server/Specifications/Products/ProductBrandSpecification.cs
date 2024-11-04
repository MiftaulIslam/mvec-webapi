using Server.Entities.Models;
using Server.Specifications.Base;

namespace Server.Specifications.Products;

public class ProductBrandSpecification : Specification<Product, string>
{
    public ProductBrandSpecification()
        : base(p => true)
    {
        AddSelect(p => p.Brand);
    }
}
