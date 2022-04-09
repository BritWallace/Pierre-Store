using Microsoft.EntityFrameworkCore;

namespace Pierre.Models
{
  public class PierreContext : DbContext
  {
    public DbSet<Treat> Treats{ get; set; }
    public DbSet<Flavor> Flavors { get; set; }
        public DbSet<TreatFlavor> TreatFlavors { get; set; }

    public PierreContext(DbContextOptions options) : base(options) { }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}