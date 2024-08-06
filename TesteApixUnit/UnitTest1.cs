
/* 
using ApiTests.Domains;
using ApiTests.Interface;
using Moq;

namespace TesteApixUnit
{
    public class UnitTest1
    {
        // M�todo auxiliar para criar um mock de IProductsRepository
        private Mock<IProductsRepository> CreateMockRepository(List<Product> products)
        {
            var mockRepository = new Mock<IProductsRepository>();
            mockRepository.Setup(x => x.Get()).Returns(products); // Configura o m�todo Get para retornar a lista de produtos mock
            return mockRepository;
        }

        // M�todo auxiliar para criar uma lista de produtos de amostra
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
            // Arrange: Organizar (Cen�rio)
            var products = GetSampleProducts();                  // Obt�m a lista de produtos de amostra
            var mockRepository = CreateMockRepository(products); // Cria um mock do reposit�rio com a lista de produtos

            // Act: Agir
            var result = mockRepository.Object.Get(); // Obt�m a lista de produtos do mock

            // Assert: Provar
            Assert.Equal(4, result.Count()); // Verifica se o n�mero de produtos est� correto
        }

        [Fact]
        public void Get_RetornaNomesInvalidosParaOsProdutos()
        {
            // Arrange: Organizar (Cen�rio)
            var products = GetSampleProducts(); // Obt�m a lista de produtos de amostra
            var mockRepository = CreateMockRepository(products); // Cria um mock do reposit�rio com a lista de produtos

            // Act: Agir
            var result = mockRepository.Object.Get(); // Obt�m a lista de produtos do mock

            // Assert: Provar
            // Verifica se todos os produtos t�m nomes v�lidos
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
    // Indica que a classe cont�m m�todos de teste de unidade
    public class UnitTest1
    {
        [Fact]
        public void Get()
        {
            // Arrange: Organizar (Cen�rio)
            var products = new List<Product>
            {
                new Product {IdProduct = Guid.NewGuid(), Name = "Produto 1", Price = 10},
                new Product {IdProduct = Guid.NewGuid(), Name = "Produto 2", Price = 20},
                new Product {IdProduct = Guid.NewGuid(), Name = "Produto 3", Price = 30}
            };

            // Cria um objeto de simula��o do tipo IProductsRepository
            var mockRepository = new Mock<IProductsRepository>();

            // Configura o m�todo Get para retornar a lista de produtos "mock"
            mockRepository.Setup(x => x.Get()).Returns(products);

            // Act: Agir
            var result = mockRepository.Object.Get();

            // Assert: Provar
            // Prova se o resultado esperado � igual ao resultado obtido atrav�s da busca
            Assert.Equal(3, result.Count()); // Usa Count() como extens�o de IEnumerable

            // ATEN��O: REFERENCIAR O PROJETO PARA FUNCIONAR
        }

        [Fact]
        public void Post()
        {
            // Arrange: Organizar (Cen�rio)
            var newProduct = new Product { IdProduct = Guid.NewGuid(), Name = "Novo Produto", Price = 50 };

            // Cria um objeto de simula��o do tipo IProductsRepository
            var mockRepository = new Mock<IProductsRepository>();

            // Configura o m�todo Post para adicionar o produto
            mockRepository.Setup(x => x.Post(It.IsAny<Product>())).Verifiable();

            // Act: Agir
            mockRepository.Object.Post(newProduct);

            // Assert: Provar
            // Verifica se o m�todo Post foi chamado uma vez com o produto especificado
            mockRepository.Verify(x => x.Post(It.IsAny<Product>()), Times.Once);

            // ATEN��O: REFERENCIAR O PROJETO PARA FUNCIONAR
        }
    }
}
