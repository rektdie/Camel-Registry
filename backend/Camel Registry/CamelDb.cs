using Microsoft.EntityFrameworkCore;

public class CamelDb : DbContext
{
    public CamelDb(DbContextOptions<CamelDb> options)
        : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=Camels.db");
    }

    public DbSet<Camel> Camels => Set<Camel>();
}