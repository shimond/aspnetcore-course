using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FirstWebApp.EntitiesFromDb;

public partial class AspnetCourseFromDbContext : DbContext
{
    public AspnetCourseFromDbContext()
    {
    }

    public AspnetCourseFromDbContext(DbContextOptions<AspnetCourseFromDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:courseDbFirst");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
