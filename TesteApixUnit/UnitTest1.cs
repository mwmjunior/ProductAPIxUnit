
/* 
using ApiTests.Domains;
using ApiTests.Interface;
using Moq;

namespace TesteApixUnit
{
    public class UnitTest1
    {
        // Método auxiliar para criar um mock de IProductsRepository
        private Mock<IProductsRepository> CreateMockRepository(List<Product> products)
        {
            var mockRepository = new Mock<IProductsRepository>();
            mockRepository.Setup(x => x.Get()).Returns(products); // Configura o método Get para retornar a lista de produtos mock
            return mockRepository;
        }

        // Método auxiliar para criar uma lista de produtos de amostra
        private List<Product> GetSampleProducts()
        {
            return new List<Product>
            {
                new Product {IdProduct = Guid.NewGuid(), Name = "Produto 1", Price = 10},
                new Product {IdProduct = Guid.NewGuid(), Name = "Produto 2", Price = 20},
                new Product {IdProduct = Guid.NewGuid(), Name = "Produto 3", Price = 30},
                new Product {IdProduct = Guid.NewGuid(), Name = null, Price = 40} // Produto com erro (nome nulo)
            };
        }

        [Fact]
        public void Get_RetornaNumeroCorretoDeProdutos()
        {
            // Arrange: Organizar (Cenário)
            var products = GetSampleProducts();                  // Obtém a lista de produtos de amostra
            var mockRepository = CreateMockRepository(products); // Cria um mock do repositório com a lista de produtos

            // Act: Agir
            var result = mockRepository.Object.Get(); // Obtém a lista de produtos do mock

            // Assert: Provar
            Assert.Equal(4, result.Count()); // Verifica se o número de produtos está correto
        }

        [Fact]
        public void Get_RetornaNomesInvalidosParaOsProdutos()
        {
            // Arrange: Organizar (Cenário)
            var products = GetSampleProducts(); // Obtém a lista de produtos de amostra
            var mockRepository = CreateMockRepository(products); // Cria um mock do repositório com a lista de produtos

            // Act: Agir
            var result = mockRepository.Object.Get(); // Obtém a lista de produtos do mock

            // Assert: Provar
            // Verifica se todos os produtos têm nomes válidos
            foreach (var product in result)
            {
                Assert.False(string.IsNullOrEmpty(product.Name), $"O produto com Id {product.IdProduct} tem nome nulo ou vazio.");
            }
        }
    }
}
*/

using ApiTests.Domains;
using ApiTests.Interface;
using Moq;

namespace TesteApixUnit
{
    // Indica que a classe contém métodos de teste de unidade
    public class UnitTest1
    {
        [Fact]
        public void Get()
        {
            // Arrange: Organizar (Cenário)
            var products = new List<Product>
            {
                new Product {IdProduct = Guid.NewGuid(), Name = "Produto 1", Price = 10},
                new Product {IdProduct = Guid.NewGuid(), Name = "Produto 2", Price = 20},
                new Product {IdProduct = Guid.NewGuid(), Name = "Produto 3", Price = 30}
            };

            // Cria um objeto de simulação do tipo IProductsRepository
            var mockRepository = new Mock<IProductsRepository>();

            // Configura o método Get para retornar a lista de produtos "mock"
            mockRepository.Setup(x => x.Get()).Returns(products);

            // Act: Agir
            var result = mockRepository.Object.Get();

            // Assert: Provar
            // Prova se o resultado esperado é igual ao resultado obtido através da busca
            Assert.Equal(3, result.Count()); // Usa Count() como extensão de IEnumerable

            // ATENÇÃO: REFERENCIAR O PROJETO PARA FUNCIONAR
        }

        [Fact]
        public void Post()
        {
            // Arrange: Organizar (Cenário)
            var newProduct = new Product { IdProduct = Guid.NewGuid(), Name = "Novo Produto", Price = 50 };

            // Cria um objeto de simulação do tipo IProductsRepository
            var mockRepository = new Mock<IProductsRepository>();

            // Configura o método Post para adicionar o produto
            mockRepository.Setup(x => x.Post(It.IsAny<Product>())).Verifiable();

            // Act: Agir
            mockRepository.Object.Post(newProduct);

            // Assert: Provar
            // Verifica se o método Post foi chamado uma vez com o produto especificado
            mockRepository.Verify(x => x.Post(It.IsAny<Product>()), Times.Once);

            // ATENÇÃO: REFERENCIAR O PROJETO PARA FUNCIONAR
        }
    }
}
