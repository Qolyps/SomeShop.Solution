using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using SomeShop.Domain.Entities;
using SomeShop.Domain.Interfaces;

namespace SomeShop.Api.Controllers
{
    [ApiController]
    [Route("api/conroller")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
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
            var productId = await _productService.CreateProduct(product);
            return Ok(productId);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Product updatedProduct)
        {
            var updatedId = await _productService.UpdateProduct(id, updatedProduct.Name, updatedProduct.Description, updatedProduct.Price);
            return Ok(updatedId);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var deletedId = await _productService.DeleteTask(id);
            return Ok(deletedId);
        }
    }
}
