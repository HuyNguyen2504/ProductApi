using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;
using ProductApi.Services;

namespace ProductApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _productService.GetAllProductsAsync();
        if (products == null){
            return NotFound($"404 No products!");
        }
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        
        if (product == null)
        {
            return NotFound($"Product with ID {id} not found");
        }
        
        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Product product)
    {
        if (string.IsNullOrEmpty(product.Name))
            return BadRequest("Name is required");
            
        var createdProduct = await _productService.CreateProductAsync(product);
        return CreatedAtAction(nameof(GetById), new { id = createdProduct.ProductId }, createdProduct);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Product product)
    {
        if (id != product.ProductId)
        {
            return BadRequest("ID in URL does not match ID in product data");
        }
        
        var updatedProduct = await _productService.UpdateProductAsync(id, product);
        
        if (updatedProduct == null)
        {
            return NotFound($"Product with ID {id} not found");
        }
        
        return Ok(updatedProduct);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _productService.DeleteProductAsync(id);
        
        if (!result)
        {
            return NotFound($"Product with ID {id} not found");
        }
        
        return NoContent();
    }
}