using LimsPrestationService.Models;
using Microsoft.EntityFrameworkCore;


namespace LimsPrestationService.Data;

public class PrestationServiceContext : DbContext
{
    public PrestationServiceContext(DbContextOptions<PrestationServiceContext> options) : base(options)
    {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>()
            .HasIndex(c => new { c.Cin, c.Contact, c.Email, c.Passeport })
            .IsUnique(true);
    }
    public DbSet<TypeEchantillon> TypeEchantillons { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<EtatPrestation> EtatPrestations { get; set; }
    public DbSet<Prestation> Prestations { get; set; }
}