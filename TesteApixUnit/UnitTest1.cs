
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

using ApiTests.Controllers;
using ApiTests.Domains;
using ApiTests.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Moq;
using Xunit.Abstractions;

namespace TesteApixUnit
{
    // Indica que a classe contém métodos de teste de unidade
    public class UnitTest1
    {

        // ADICIONAL
        // O xUnit não exibe saídas do Console.WriteLine no console de saída durante a execução dos testes.
        // Para contornar isso, podemos usar a interface ITestOutputHelper fornecida pelo xUnit para capturar e
        // exibir saídas de log dentro do contexto dos testes unitários.

        private readonly ITestOutputHelper output; //  output é a variável declarada na classe e que será utilizada para registrar as saídas dos testes.

        //readonly indica que essa variável só pode ser atribuída no momento de sua declaração ou dentro do construtor da classe.
        //Isso garante que o valor da variável output não seja modificado em outros lugares do código após a construção do objeto.


        // Construtor que recebe uma instância de ITestOutputHelper.
        // Isso permite capturar e exibir saídas de log durante a execução dos testes.
        public UnitTest1(ITestOutputHelper output)
        {
            this.output = output;  // A variável de instância `output` é usada para capturar e exibir as mensagens de log.
        }                           // Ela é declarada na classe e atribuída no construtor.

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

        [Fact]
        public void Put()
        {
            // Arrange: Organizar (Cenário)
            var existingProductId = Guid.NewGuid();
            var existingProduct = new Product { IdProduct = existingProductId, Name = "Produto Existente", Price = 20 };
            var updatedProduct = new Product { IdProduct = existingProductId, Name = "Produto Atualizado", Price = 25 };

            // Cria um objeto de simulação do tipo IProductsRepository
            var mockRepository = new Mock<IProductsRepository>();

            // Configura o método GetById para retornar o produto existente
            mockRepository.Setup(x => x.GetById(existingProductId)).Returns(existingProduct);

            // Configura o método Put para atualizar o produto
            mockRepository.Setup(x => x.Put(existingProductId, updatedProduct)).Verifiable();

            // Act: Agir
            var controller = new ProductsController(mockRepository.Object);
            var result = controller.Put(existingProductId, updatedProduct);

            // Assert: Provar
            // Verifica se o método Put foi chamado uma vez com os parâmetros especificados
            mockRepository.Verify(x => x.Put(existingProductId, updatedProduct), Times.Once);

            // Verifica se o resultado da ação é NoContent
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void Delete()
        {
            // Arrange: Organizar (Cenário)
            var existingProductId = Guid.NewGuid();
            var existingProduct = new Product { IdProduct = existingProductId, Name = "Produto Existente", Price = 20 };

            // Cria um objeto de simulação do tipo IProductsRepository
            var mockRepository = new Mock<IProductsRepository>();

            // Configura o método GetById para retornar o produto existente
            mockRepository.Setup(x => x.GetById(existingProductId)).Returns(existingProduct);

            // Configura o método Delete para deletar o produto
            mockRepository.Setup(x => x.Delete(existingProductId)).Verifiable();

            // Act: Agir
            var controller = new ProductsController(mockRepository.Object);
            var result = controller.Delete(existingProductId);

            // Assert: Provar
            // Verifica se o método Delete foi chamado uma vez com o parâmetro especificado
            mockRepository.Verify(x => x.Delete(existingProductId), Times.Once);

            // Verifica se o resultado da ação é NoContent
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void Put_AtualizaProdutoExistente_RetornaNoContent()
        {
            // Arrange: Organizar (Cenário)
            var existingProductId = Guid.NewGuid(); // Cria um ID para um produto existente
            var existingProduct = new Product
            {
                IdProduct = existingProductId,
                Name = "Produto Existente",
                Price = 20
            };
            var updatedProduct = new Product
            {
                IdProduct = existingProductId,
                Name = "Produto Atualizado",
                Price = 25
            };

            // Cria um objeto de simulação (mock) do repositório de produtos
            var mockRepository = new Mock<IProductsRepository>();

            // Configura o método GetById para retornar o produto existente quando chamado com o ID do produto
            mockRepository.Setup(x => x.GetById(existingProductId)).Returns(existingProduct);

            // Configura o método Put para ser chamado corretamente com o ID e o produto atualizado
            mockRepository.Setup(x => x.Put(existingProductId, updatedProduct)).Verifiable();

            // Adiciona uma mensagem ao console para indicar que o Arrange foi concluído
            output.WriteLine("Arrange: Produto existente e produto atualizado configurados.");

            // Act: Agir
            var controller = new ProductsController(mockRepository.Object);
            var result = controller.Put(existingProductId, updatedProduct);

            // Adiciona uma mensagem ao console para indicar que o método Put foi chamado
            output.WriteLine($"Act: Método Put chamado para o produto com ID {existingProductId}.");

            // Assert: Provar
            mockRepository.Verify(x => x.Put(existingProductId, updatedProduct), Times.Once); // Verifica se o método Put foi chamado uma vez
            Assert.IsType<NoContentResult>(result); // Verifica se o retorno do método é do tipo NoContentResult

            // Adiciona uma mensagem ao console para indicar que o teste foi concluído com sucesso
            output.WriteLine("Assert: Verificações concluídas. Teste Put passou.");
        }



    }
}
