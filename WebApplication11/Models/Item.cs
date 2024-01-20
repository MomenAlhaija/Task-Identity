using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Identity.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = "";
        
        [Required]
        [DisplayName("The Price")]
        [Range(10, 10000, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Required]
        [DisplayName("Category")]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }


        public string? ImagePath { get;set; }

        [DisplayName("File")]
        [NotMapped]
        public IFormFile? ClientFile { get; set; }
        public Category? Category { get; set; }

       
    }
}
