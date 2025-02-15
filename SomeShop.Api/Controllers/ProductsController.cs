using System.Runtime.CompilerServices;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SomeShop.Domain.Entities;
using SomeShop.Domain.Interfaces;

namespace SomeShop.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/product")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IValidator<Product> _productValidator;

        public ProductsController(IProductService productService, IValidator<Product> productValidator)
        {
            _productService = productService;
            _productValidator = productValidator;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetProduct(Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            return product == null ? NotFound() : Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> CreateProduct([FromBody] Product product)
        {
            var validationResult = await _productValidator.ValidateAsync(product);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            await _productService.CreateProductAsync(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] Product updatedProduct)
        {
            var validationResult = await _productValidator.ValidateAsync(updatedProduct);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            await _productService.UpdateProductAsync(updatedProduct);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _productService.DeleteProductAsync(id);
            return NoContent();
        }
    }
}
