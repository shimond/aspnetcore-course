namespace FirstWebApp.DataAccess;

public partial class AspnetCourseFromDbContext(DbContextOptions<AspnetCourseFromDbContext> options) : DbContext(options)
{
    public virtual DbSet<ProductEntity> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
