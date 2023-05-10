using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace product_api.Models
{
    public class ProductDto
    {
        public int Id { get; set; }
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

        public Product Product()
        {
            return new Product
            {
                Id = this.Id,
                State = "Draft",
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
