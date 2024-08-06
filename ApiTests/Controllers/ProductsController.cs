using Microsoft.AspNetCore.Mvc;
using ApiTests.Domains;
using ApiTests.Interface;
using System;
using System.Collections.Generic;

namespace ApiTests.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsRepository _productsRepository;

        public ProductsController(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        // GET: api/products
        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            var products = _productsRepository.Get();
            return Ok(products);
        }

        // GET: api/products/{id}
        [HttpGet("{id}")]
        public ActionResult<Product> Get(Guid id)
        {
            var product = _productsRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        // POST: api/products
        [HttpPost]
        public ActionResult<Product> Post(Product product)
        {
            _productsRepository.Post(product);
            return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
        }

        // PUT: api/products/{id}
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, Product product)
        {
            var existingProduct = _productsRepository.GetById(id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            _productsRepository.Put(id, product);
            return NoContent();
        }

        // DELETE: api/products/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var product = _productsRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            _productsRepository.Delete(id);
            return NoContent();
        }
    }
}
