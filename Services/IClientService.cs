using LimsPrestationService.Models;

namespace LimsPrestationService.Services;

public interface IClientService
{
    Task<List<Client>> GetClients();
    Task<Client> GetClient(int id);
    Task<Client> UpdateClient(int id, Client client);
    Task<Client> CreateClient(Client client);
    Task<List<Client>> SearchClient(string searchTerm);
}