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
            optionsBuilder.UseMySql("Server=sql10.freemysqlhosting.net,3306;Uid=sql10712328;Pwd=ksjJDh2IUp;Database=sql10712328;",
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