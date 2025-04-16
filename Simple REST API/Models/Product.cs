using System.ComponentModel.DataAnnotations;

namespace Simple_REST_API.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public decimal Price { get; set; }
        
        public string? Description { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public DateTime UpdatedAt { get; set; }

        public Product()
        {
        }

        public Product(string name, string description, decimal price)
        {
            Name = name;
            Description = description;
            Price = price;
            //the rest are handled in DbContext
        }

    }
}
