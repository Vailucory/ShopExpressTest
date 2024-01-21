using Microsoft.EntityFrameworkCore;
using ShopExpressTest.Models;

namespace ShopExpressTest.DataAccess;

public class ApplicationDbContext : DbContext
{
    public DbSet<ToDoItem> ToDoItems { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ToDoItem>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(e => e.IsCompleted)
                .IsRequired();    
        }) ;
    }
}
