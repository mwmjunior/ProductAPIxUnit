
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

using ApiTests.Controllers;
using ApiTests.Domains;
using ApiTests.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Moq;
using Xunit.Abstractions;

namespace TesteApixUnit
{
    // Indica que a classe cont�m m�todos de teste de unidade
    public class UnitTest1
    {

        // ADICIONAL
        // O xUnit n�o exibe sa�das do Console.WriteLine no console de sa�da durante a execu��o dos testes.
        // Para contornar isso, podemos usar a interface ITestOutputHelper fornecida pelo xUnit para capturar e
        // exibir sa�das de log dentro do contexto dos testes unit�rios.

        private readonly ITestOutputHelper output; //  output � a vari�vel declarada na classe e que ser� utilizada para registrar as sa�das dos testes.

        //readonly indica que essa vari�vel s� pode ser atribu�da no momento de sua declara��o ou dentro do construtor da classe.
        //Isso garante que o valor da vari�vel output n�o seja modificado em outros lugares do c�digo ap�s a constru��o do objeto.


        // Construtor que recebe uma inst�ncia de ITestOutputHelper.
        // Isso permite capturar e exibir sa�das de log durante a execu��o dos testes.
        public UnitTest1(ITestOutputHelper output)
        {
            this.output = output;  // A vari�vel de inst�ncia `output` � usada para capturar e exibir as mensagens de log.
        }                           // Ela � declarada na classe e atribu�da no construtor.

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

        [Fact]
        public void Put()
        {
            // Arrange: Organizar (Cen�rio)
            var existingProductId = Guid.NewGuid();
            var existingProduct = new Product { IdProduct = existingProductId, Name = "Produto Existente", Price = 20 };
            var updatedProduct = new Product { IdProduct = existingProductId, Name = "Produto Atualizado", Price = 25 };

            // Cria um objeto de simula��o do tipo IProductsRepository
            var mockRepository = new Mock<IProductsRepository>();

            // Configura o m�todo GetById para retornar o produto existente
            mockRepository.Setup(x => x.GetById(existingProductId)).Returns(existingProduct);

            // Configura o m�todo Put para atualizar o produto
            mockRepository.Setup(x => x.Put(existingProductId, updatedProduct)).Verifiable();

            // Act: Agir
            var controller = new ProductsController(mockRepository.Object);
            var result = controller.Put(existingProductId, updatedProduct);

            // Assert: Provar
            // Verifica se o m�todo Put foi chamado uma vez com os par�metros especificados
            mockRepository.Verify(x => x.Put(existingProductId, updatedProduct), Times.Once);

            // Verifica se o resultado da a��o � NoContent
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void Delete()
        {
            // Arrange: Organizar (Cen�rio)
            var existingProductId = Guid.NewGuid();
            var existingProduct = new Product { IdProduct = existingProductId, Name = "Produto Existente", Price = 20 };

            // Cria um objeto de simula��o do tipo IProductsRepository
            var mockRepository = new Mock<IProductsRepository>();

            // Configura o m�todo GetById para retornar o produto existente
            mockRepository.Setup(x => x.GetById(existingProductId)).Returns(existingProduct);

            // Configura o m�todo Delete para deletar o produto
            mockRepository.Setup(x => x.Delete(existingProductId)).Verifiable();

            // Act: Agir
            var controller = new ProductsController(mockRepository.Object);
            var result = controller.Delete(existingProductId);

            // Assert: Provar
            // Verifica se o m�todo Delete foi chamado uma vez com o par�metro especificado
            mockRepository.Verify(x => x.Delete(existingProductId), Times.Once);

            // Verifica se o resultado da a��o � NoContent
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void Put_AtualizaProdutoExistente_RetornaNoContent()
        {
            // Arrange: Organizar (Cen�rio)
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

            // Cria um objeto de simula��o (mock) do reposit�rio de produtos
            var mockRepository = new Mock<IProductsRepository>();

            // Configura o m�todo GetById para retornar o produto existente quando chamado com o ID do produto
            mockRepository.Setup(x => x.GetById(existingProductId)).Returns(existingProduct);

            // Configura o m�todo Put para ser chamado corretamente com o ID e o produto atualizado
            mockRepository.Setup(x => x.Put(existingProductId, updatedProduct)).Verifiable();

            // Adiciona uma mensagem ao console para indicar que o Arrange foi conclu�do
            output.WriteLine("Arrange: Produto existente e produto atualizado configurados.");

            // Act: Agir
            var controller = new ProductsController(mockRepository.Object);
            var result = controller.Put(existingProductId, updatedProduct);

            // Adiciona uma mensagem ao console para indicar que o m�todo Put foi chamado
            output.WriteLine($"Act: M�todo Put chamado para o produto com ID {existingProductId}.");

            // Assert: Provar
            mockRepository.Verify(x => x.Put(existingProductId, updatedProduct), Times.Once); // Verifica se o m�todo Put foi chamado uma vez
            Assert.IsType<NoContentResult>(result); // Verifica se o retorno do m�todo � do tipo NoContentResult

            // Adiciona uma mensagem ao console para indicar que o teste foi conclu�do com sucesso
            output.WriteLine("Assert: Verifica��es conclu�das. Teste Put passou.");
        }



    }
}
