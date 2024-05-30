using Microsoft.EntityFrameworkCore;
using mvc.crud.netcore6.webprojects.learning.Models.Domain;

namespace mvc.crud.netcore6.webprojects.learning.Data
{
    public class MvcDbContext : DbContext
    {
        public MvcDbContext(DbContextOptions<MvcDbContext> options) : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
    }
}
