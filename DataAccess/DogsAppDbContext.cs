using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class DogsAppDbContext : DbContext
{
    public DogsAppDbContext(DbContextOptions<DogsAppDbContext> options) : base(options)
    {
    }

    public DbSet<Dog> Dogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Dog>()
            .ToTable(b =>
            {
                b.HasCheckConstraint("CK_tail_length", "[tail_length] >= 0");
                b.HasCheckConstraint("CK_weight", "[weight] > 0");
            });


    }
}
