using System.Runtime.CompilerServices;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SomeShop.Application.DTO;
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
        private readonly IValidator<ProductDto> _productDtoValidator;

        public ProductsController(IProductService productService, IValidator<Product> productValidator, IValidator<ProductDto> productDtoValidator)
        {
            _productService = productService;
            _productValidator = productValidator;
            _productDtoValidator = productDtoValidator;
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
        public async Task<ActionResult> CreateProduct([FromBody] ProductDto productDtoValidator)
        {
            var validationResult = await _productDtoValidator.ValidateAsync(productDtoValidator);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var product = new Product   
            {
                Id = Guid.NewGuid(),
                Name = productDtoValidator.Name,
                Price = productDtoValidator.Price
            };

            await _productService.CreateProductAsync(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] ProductDto productDtoValidator)
        {
            var validationResult = await _productDtoValidator.ValidateAsync(productDtoValidator);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var product = new Product
            {
                Id = id,
                Name = productDtoValidator.Name,
                Price = productDtoValidator.Price
            };

            await _productService.UpdateProductAsync(product);
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
