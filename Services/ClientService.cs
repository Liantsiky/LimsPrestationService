using LimsPrestationService.Data;
using LimsPrestationService.Models;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace LimsPrestationService.Services;

public class ClientService : IClientService
{

    private readonly PrestationServiceContext _dbContext;
    public ClientService(PrestationServiceContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Client> CreateClient(Client client)
    {
        try
        {
            _dbContext.Clients.Add(client);
            await _dbContext.SaveChangesAsync();
            return await GetClient(client.IdClient);
        }catch(DbUpdateException ex)
        {
            if(ex.InnerException is MySqlException mysqlEx && mysqlEx.Number == 1062)
            {
                throw new ArgumentException(
                    $"Un client avec les mêmes identifiants (email : '{client.Email}', cin : '{client.Cin}', passeport : '{client.Passeport}', contact : '{client.Contact})' existe déjà."
                );
            }
            throw;
        }
    }

    public async Task<Client> GetClient(int id)
    {
        Client? result = await _dbContext.Clients.FindAsync(id);
        if(result == null)
        {
            throw new ArgumentException("Ce client n'existe pas");
        }
        return result;
    }

    public async Task<List<Client>> GetClients()
    {
        List<Client> clients = await _dbContext.Clients.ToListAsync();
        if(clients == null)
        {
            throw new ArgumentException("Aucun client trouvé");
        }
        return clients;
    }

    public Task<Client> UpdateClient(int id, Client client)
    {
        if(id!=client.IdClient)
        {
            throw new ArgumentException("Id client invalide");
        }
        _dbContext.Clients.Update(client);
        _dbContext.SaveChanges();
        return Task.FromResult(client);
    }
}