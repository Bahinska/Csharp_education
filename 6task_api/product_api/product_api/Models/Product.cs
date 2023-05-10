using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace product_api.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string State { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Image_url { get; set; }
        [Required]
        public float Price { get; set; }
        [Required]
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public string Description { get; set; }

        public ProductDto ProductDto()
        {
            return new ProductDto
            {
                Id = this.Id,
                Title = this.Title,
                Image_url = this.Image_url,
                Price = this.Price,
                Created_at = this.Created_at,
                Updated_at = this.Updated_at,
                Description = this.Description
            };
        }
    }
}
