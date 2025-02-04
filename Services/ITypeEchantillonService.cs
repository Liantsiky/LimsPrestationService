using LimsPrestationService.Models;

namespace LimsPrestationService.Services;
public interface ITypeEchantillonService
{
    Task<List<TypeEchantillon>> GetTypeEchantillonFrom(int skiped, int size);
    Task<List<TypeEchantillon>> GetTypeEchantillons();
    int CountTypeEchantillon();
    Task<TypeEchantillon> CreateTypeEchantillon(TypeEchantillon typeEchantillon);
    Task<TypeEchantillon> GetTypeEchantillon(int id);
    Task<TypeEchantillon> UpdateTypeEchantillon(int id, TypeEchantillon typeEchantillon);
}