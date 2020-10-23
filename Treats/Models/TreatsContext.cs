using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Treats.Models
{
  public class TreatsContext : IdentityDbContext<ApplicationUser>
  {
    public DbSet<Treat> Treats { get; set; }

    public DbSet<Flavor> Flavors { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderTreat> OrderTreats { get; set; }
    
    public DbSet<TreatFlavor> TreatFlavors { get; set; }

    
    public TreatsContext(DbContextOptions options) : base(options) { }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}