using Microsoft.EntityFrameworkCore;
using Projekt.Models;


namespace Projekt.DBAccess;


public class BeerAppContext : DbContext{
    public DbSet<Beer> Beers{ get; set; }
    public DbSet<Brewery> Breweries{ get; set; }
    
    public DbSet<BeerType> BeerTypes{ get; set; }
    
    public DbSet<BeerUser> BeerUsers{ get; set; }
    
    public DbSet<BeerReview> BeerReviews{ get; set; }
    
    public DbSet<BeerRole> BeerRoles{ get; set; } 

    protected override void OnModelCreating(ModelBuilder modelBuilder){
        modelBuilder.Entity<Beer>()
            .HasOne(b => b.Brewery)
            .WithMany(br => br.Beers)
            .HasForeignKey(b => b.BreweryId);
        
        modelBuilder.Entity<Beer>()
            .HasOne(t => t.BeerType)
            .WithMany(bt => bt.Beers)
            .HasForeignKey(t => t.BeerTypeId);
        
        //users
        modelBuilder.Entity<BeerUser>()
            .HasMany(u => u.BeerReviews)
            .WithOne(r => r.BeerUser)
            .HasForeignKey(r => r.BeerUserId);
        
        modelBuilder.Entity<BeerUser>()
            .HasOne(u => u.BeerRole)
            .WithMany(r => r.BeerUsers)
            .HasForeignKey(u => u.BeerRoleId);

        
        
    }

    public BeerAppContext(DbContextOptions<BeerAppContext> options) : base(options) { }
    
}