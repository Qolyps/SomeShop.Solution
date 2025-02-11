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
        public async Task<ActionResult> GetProducts()
        {
            var products = await _productService.GetAllProduct();

            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult> CreateProduct([FromBody] Product product)
        {
            var validationResult = await _productValidator.ValidateAsync(product);
    
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var productId = await _productService.CreateProduct(product);
            return CreatedAtAction(nameof(GetProducts), new { id = productId }, product);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Product updatedProduct)
        {
            var validationResult = await _productValidator.ValidateAsync(updatedProduct);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var updatedId = await _productService.UpdateProduct(id, updatedProduct.Name, updatedProduct.Description, updatedProduct.Price);
            return Ok(updatedId);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var deletedId = await _productService.DeleteProduct(id);
            return Ok(deletedId);
        }
    }
}
