using Microsoft.EntityFrameworkCore;

namespace FirstWebApp.Infra.ApplicationDbContext
{
    public class CourseDbContext : DbContext
    {
        public DbSet<Product> Products  { get; set; }

        public CourseDbContext(DbContextOptions<CourseDbContext> contextOptions) : base(contextOptions)
        {
                
        }
    }
}
