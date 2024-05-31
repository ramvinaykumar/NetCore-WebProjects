using Microsoft.EntityFrameworkCore;
using MVC.NetCore8.WebSite.Models.DbEntities;

namespace MVC.NetCore8.WebSite.Data.DbContextService
{
    /// <summary>
    /// Db Context service class to talk with Database
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}
