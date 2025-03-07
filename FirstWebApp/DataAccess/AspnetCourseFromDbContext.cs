namespace FirstWebApp.DataAccess;

public partial class AspnetCourseFromDbContext : DbContext
{
    public AspnetCourseFromDbContext()
    {
    }

    public AspnetCourseFromDbContext(DbContextOptions<AspnetCourseFromDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ProductEntity> Products { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
