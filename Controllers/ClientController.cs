using LimsPrestationService.Models;
using LimsPrestationService.Services;
using LimsUtils.Api;
using Microsoft.AspNetCore.Mvc;

namespace LimsPrestationService.Controllers;

[ApiController]
[Route("/api/client")]
public class ClientController : Controller
{
    private readonly IClientService _clientService;
    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse>> GetClients()
    {
        List<Client> clients = await _clientService.GetClients();
        return Ok(
            new ApiResponse
            {
                Data = clients,
                ViewBag = null,
                IsSuccess = true,
                Message = "Clients retrieved successfully.",
                StatusCode = 200
            }
        );
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse>> GetClient(int id)
    {
        Client client = await _clientService.GetClient(id);
        return Ok(
            new ApiResponse
            {
                Data = client,
                ViewBag = null,
                IsSuccess = true,
                Message = "Client retrieved successfully.",
                StatusCode = 200
            }
        );
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse>> CreateClient([FromBody] Client client)
    {
        Client result = await _clientService.CreateClient(client);
        return Ok(
            new ApiResponse
            {
                Data = result,
                ViewBag = null,
                IsSuccess = true,
                Message = "Client created successfully.",
                StatusCode = 201
            }
        );
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse>> UpdateClient(int id, [FromBody] Client client)
    {
        Client result = await _clientService.UpdateClient(id, client);
        return Ok(
            new ApiResponse
            {
                Data = result,
                ViewBag = null,
                IsSuccess = true,
                Message = "Client updated successfully.",
                StatusCode = 200
            }
        );
    }
}