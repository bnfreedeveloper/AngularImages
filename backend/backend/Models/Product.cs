using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }    
        public string? Image { get; set; }  
        [NotMapped]
        public IFormFile ImageFile { get; set; } 
    }
}
