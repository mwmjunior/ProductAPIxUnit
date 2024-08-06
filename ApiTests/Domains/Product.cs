using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiTests.Domains
{
    [Table("Product")]
    public class Product
    {

        [Key] // Isso define 'Id' como a chave primária da entidade
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid IdProduct { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        // Construtor vazio para o Entity Framework
        public Product() { }

        // Construtor para inicializar a classe com valores
        public Product(Guid idProduct, string name, decimal price)
        {
            IdProduct = idProduct;
            Name = name;
            Price = price;
        }
    }
}
