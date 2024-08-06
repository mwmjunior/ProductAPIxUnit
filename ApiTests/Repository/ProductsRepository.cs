using ApiTests.Context;
using ApiTests.Domains;
using ApiTests.Interface;

namespace ApiTests.Repository
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly ApiTestsContext _context;

        public ProductsRepository(ApiTestsContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> Get()
        {
            return _context.Products.ToList();
        }

        public Product GetById(Guid idProduct)
        {
            return _context.Products.Find(idProduct);
        }

        public void Post(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void Put(Guid idProduct, Product product)
        {
            var existingProduct = _context.Products.Find(idProduct);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                _context.SaveChanges();
            }
        }

        public void Delete(Guid idProduct)
        {
            var product = _context.Products.Find(idProduct);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }
    }
}