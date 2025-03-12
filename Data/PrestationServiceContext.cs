using LimsPrestationService.Models;
using LimsPrestationService.Sequences;
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
        
        modelBuilder.Entity<VPrestationEtatDecompte>().ToView("v_prestation_etat_decompte");
        modelBuilder.Entity<VPrestationDetails>().ToView("v_prestation_details");

        modelBuilder.Entity<Prestation>()
            .HasMany(p => p.PrestationDetails)
            .WithOne(pd => pd.Prestation);
    }
    public DbSet<Client> Clients { get; set; }
    public DbSet<EtatPrestation> EtatPrestations { get; set; }
    public DbSet<Prestation> Prestations { get; set; }
    public DbSet<VPrestationEtatDecompte> VPrestationEtatDecomptes { get; set; }
    public DbSet<VPrestationDetails> VPrestationDetails { get; set; }
    public DbSet<Echantillon> Echantillons { get; set; }
    public DbSet<FicheTravailSequence> FicheTravailSequences { get; set; }
    public DbSet<Travail> Travails { get; set; }
    public DbSet<EtatDecompte> EtatDecomptes {get; set;}

}