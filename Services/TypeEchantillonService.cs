using LimsPrestationService.Data;
using LimsPrestationService.Models;
using Microsoft.EntityFrameworkCore;


namespace LimsPrestationService.Services;
public class TypeEchantillonService : ITypeEchantillonService
{
    private readonly PrestationServiceContext _dbContext;
    public TypeEchantillonService(PrestationServiceContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<TypeEchantillon>> GetTypeEchantillons()
    {
        List<TypeEchantillon> results = await _dbContext.TypeEchantillons
        .ToListAsync();
        return results;
    }
    
    public async Task<List<TypeEchantillon>> GetTypeEchantillonFrom(int skiped, int size)
    {
       List<TypeEchantillon> results = await _dbContext.TypeEchantillons
            .OrderByDescending(e => e.IdTypeEchantillon).Skip(skiped).Take(size)
            .ToListAsync();

        return results;
    }

    public int CountTypeEchantillon()
    {
        int result =_dbContext.TypeEchantillons.Count();
        return result;
    }

    public async Task<TypeEchantillon> CreateTypeEchantillon(TypeEchantillon typeEchantillon)
    {
         _dbContext.TypeEchantillons.Add(typeEchantillon);
        await _dbContext.SaveChangesAsync();
       
        return await this.GetTypeEchantillon(typeEchantillon.IdTypeEchantillon);
    }

    public async Task<TypeEchantillon> GetTypeEchantillon(int id)
    {
        TypeEchantillon result = await _dbContext.TypeEchantillons
            .Where(p => p.IdTypeEchantillon == id)
            .FirstAsync();
        return result;
    }

    public async Task<TypeEchantillon> UpdateTypeEchantillon(int id, TypeEchantillon typeEchantillon)
    {
        _dbContext.TypeEchantillons.Update(typeEchantillon);
        await _dbContext.SaveChangesAsync();

        TypeEchantillon result = await this.GetTypeEchantillon(id);
        return result;

    }
}