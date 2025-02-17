using LimsPrestationService.Models;
using Microsoft.EntityFrameworkCore;


namespace LimsPrestationService.Data;

public class PrestationServiceContext : DbContext
{
    public PrestationServiceContext(DbContextOptions<PrestationServiceContext> options) : base(options)
    {}
    public DbSet<TypeEchantillon> TypeEchantillons { get; set; }
    public DbSet<Client> Clients { get; set; }
}