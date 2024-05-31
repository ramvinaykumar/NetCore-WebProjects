using System.ComponentModel.DataAnnotations;

namespace MVC.NetCore8.WebSite.Models.Dto
{
    /// <summary>
    /// Dto Medel class for Data traverse between model to view
    /// </summary>
    public class AddEditProduct
    {
        [Required, MaxLength(100)]
        public required string Name { get; set; }

        [Required, MaxLength(100)]
        public required string Brand { get; set; }

        [Required, MaxLength(100)]
        public required string Category { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required, MaxLength(1000)]
        public string Description { get; set; } = string.Empty;

        public IFormFile? ImageFile { get; set; }
    }
}
