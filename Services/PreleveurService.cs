using LimsPrestationService.Data;
using LimsPrestationService.Models;
using Microsoft.EntityFrameworkCore;

namespace LimsPrestationService.Services;

public class PreleveurService : IPreleveurService
{
    private readonly PrestationServiceContext _context;

    public PreleveurService(PrestationServiceContext context)
    {
        _context = context;
    }

    public async Task<List<Preleveur>> GetPreleveurs()
    {
        return await _context.Preleveurs.ToListAsync();
    }

    public async Task<Preleveur> GetPreleveur(int id)
    {
        return await _context.Preleveurs.FindAsync(id) ?? throw new KeyNotFoundException("Preleveur not found");
    }
}