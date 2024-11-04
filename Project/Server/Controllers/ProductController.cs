using Microsoft.AspNetCore.Mvc;
using Server.Entities.Models;
using Server.Repositories.Interfaces;
using Server.Specifications.Products;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController(IGenericRepository<Product> genericRepository) : ControllerBase
{
    private readonly IGenericRepository<Product> _genericRepository = genericRepository;

    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GetProducts()
    {
        return Ok(await _genericRepository.ListEntity());
    }

    [HttpGet]
    [Route("filter")]
    public async Task<IActionResult> GetProducts(string? brand, string? category)
    {
        return Ok(await _genericRepository.ListEntity(new ProductSpecification(brand, category, null)));
    }

    [HttpGet]
    [Route("sort")]
    public async Task<IActionResult> GetProducts(string? sort)
    {
        return Ok(await _genericRepository.ListEntity(new ProductSpecification(null, null, sort)));
    }

    [HttpGet]
    [Route("brands")]
    public async Task<IActionResult> GetBrands()
    {
        IReadOnlyList<string> brands = await _genericRepository.ListEntity<string>(new ProductBrandSpecification());
        return Ok(brands.Distinct());
    }

    [HttpGet]
    [Route("categories")]
    public async Task<IActionResult> GetCategories()
    {
        IReadOnlyList<string> categories = await _genericRepository.ListEntity<string>(new ProductCategorySpecification());
        return Ok(categories.Distinct());
    }

    [HttpGet]
    [Route("details")]
    public async Task<IActionResult> GetProduct(int productId)
    {
        Product? product = await _genericRepository.GetEntity(productId);
        return product == null ? NotFound() : Ok(product);
    }

    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> PostProduct(Product product)
    {
        if (product == null)
        {
            return BadRequest("Product cannot be null.");
        }

        await _genericRepository.AddEntity(product);
        _ = await _genericRepository.Save();
        return CreatedAtAction(nameof(GetProduct), new { productId = product.Id }, product);
    }

    [HttpPut]
    [Route("update")]
    public async Task<IActionResult> PutProduct(int productId, Product product)
    {
        if (productId != product.Id)
        {
            return BadRequest("Product ID mismatch.");
        }

        _genericRepository.UpdateEntity(product);
        bool isSaved = await _genericRepository.Save();
        return !isSaved ? BadRequest("Error updating product.") : NoContent();
    }

    [HttpDelete]
    [Route("delete")]
    public async Task<IActionResult> DeleteProduct(int productId)
    {
        Product? product = await _genericRepository.GetEntity(productId);

        if (product == null)
        {
            return NotFound();
        }

        _genericRepository.RemoveEntity(product);
        _ = await _genericRepository.Save();
        return NoContent();
    }
}
