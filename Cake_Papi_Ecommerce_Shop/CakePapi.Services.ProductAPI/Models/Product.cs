using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace CakePapi.Services.ProductAPI.Models
{
    public class Product
    {
        [Key]
        public int ProductId  { get; set; }
        [Required]
        public string? Name { get; set; }

        [Range(1, int.MaxValue)]
        public double Price { get; set; }
        public string? Description { get; set; }
        public string? CategoryName { get; set; }
        public byte[]? Image { get; set; }

    }
}
