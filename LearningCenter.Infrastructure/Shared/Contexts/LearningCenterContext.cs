using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Contexts;

public class LearningCenterContext : DbContext
{
    public LearningCenterContext()
    {
    }

    public LearningCenterContext(DbContextOptions<LearningCenterContext> options) : base(options)
    {
    }

    public DbSet<Tutorial> Tutorials { get; set; }
    public DbSet<Section> Sections { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
            optionsBuilder.UseMySql("Server=127.0.0.1,3306;Uid=root;Pwd=Upc123!;Database=learning_center_ws52;",
                serverVersion);
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Tutorial>().ToTable("Tutorial");
        //builder.Entity<Tutorial>().HasKey(p => p.Id);
        //builder.Entity<Tutorial>().Property(p => p.Name).IsRequired().HasMaxLength(25);

        builder.Entity<Section>().ToTable("Section");
    }
}