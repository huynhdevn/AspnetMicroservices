using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(IProductRepository repository, ILogger<CatalogController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // [HttpGet]
        // public ActionResult<string> GetProducts()
        // {
        //     return Ok("This is CatalogAPI");
        // }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return Ok(await _repository.GetProducts());
        }

        [HttpGet("{id:length(24)}", Name = "getProduct")]
        public async Task<ActionResult<Product>> GetProductById(string id)
        {
            var product = await _repository.GetProduct(id);

            if (product == null)
            {
                _logger.LogError($"Product with id: {id}, not found.");
                return NotFound();
            }

            return Ok(product);
        }

        [HttpGet("[action]/{category}", Name = "getProductByCategory")]
        public async Task<ActionResult<Product>> GetProductByCategory(string category)
        {
            var products = await _repository.GetProductsByCategory(category);

            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            await _repository.CreateProduct(product);

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        [HttpPut]
        public async Task<ActionResult<bool>> UpdateProduct(Product product)
        {
            var productFound = await _repository.GetProduct(product.Id);

            if (productFound == null)
            {
                return NotFound();
            }

            return Ok(await _repository.UpdateProduct(productFound));
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteProductById(string id)
        {
            var productFound = await _repository.GetProduct(id);

            if (productFound == null)
            {
                return NotFound();
            }

            return Ok(await _repository.DeleteProduct(productFound.Id));
        }
    }
}
