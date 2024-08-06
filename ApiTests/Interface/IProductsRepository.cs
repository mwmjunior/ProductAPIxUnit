using ApiTests.Domains;

namespace ApiTests.Interface
{
    public interface IProductsRepository
    {

        IEnumerable<Product> Get(); // Retorna todos os produtos
        Product GetById(Guid idProduct); // Retorna um produto pelo Id
        void Post(Product product); // Adiciona um novo produto
        void Put(Guid idProduct, Product product); // Atualiza um produto existente
        void Delete(Guid idProduct); // Deleta um produto pelo Id
    }
}
