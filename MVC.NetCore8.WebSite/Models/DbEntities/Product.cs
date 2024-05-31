using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MVC.NetCore8.WebSite.Models.DbEntities
{
    /// <summary>
    /// Model / Entities class for generating table into database
    /// </summary>
    public class Product
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public required string Name { get; set; } 

        [MaxLength(100)]
        public required string Brand { get; set; }

        [MaxLength(100)]
        public required string Category { get; set; } 

        [Precision(16,2)]
        public decimal Price { get; set; }

        public string Description { get; set; } = string.Empty;

        [MaxLength(100)]
        public required string ImageFileName { get; set;} 

        public DateTime CreatedOn { get; set; }
    }
}
