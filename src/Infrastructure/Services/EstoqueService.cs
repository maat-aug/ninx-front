using NinxERP.Domain.DTOs;
using NinxERP.Domain.Interfaces;
using NinxERP.Infrastructure.Config;
using System.Net.Http.Json;

namespace NinxERP.Infrastructure.Services;

public class EstoqueService : IEstoqueService
{
    private readonly HttpClient _httpClient;
    private readonly ApiConfiguration _config;

    public EstoqueService(HttpClient httpClient, ApiConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
    }

    public async Task<List<EstoqueDTO>> GetAllAsync(PaginacaoDTO paginacao)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(_config.GetEndpointUrl("/api/Estoque/All"), paginacao);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<List<EstoqueDTO>>() ?? new();
            return new();
        }
        catch
        {
            return new();
        }
    }

    public async Task<EstoqueDTO?> GetByIdAsync(int estoqueId)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<EstoqueDTO>(_config.GetEndpointUrl($"/api/Estoque/{estoqueId}"));
        }
        catch
        {
            return null;
        }
    }

    public async Task<bool> CreateAsync(AtualizarEstoqueDTO dto)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(_config.GetEndpointUrl("/api/Estoque"), dto);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> UpdateAsync(int estoqueId, AtualizarEstoqueDTO dto)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync(_config.GetEndpointUrl($"/api/Estoque/{estoqueId}"), dto);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteAsync(int estoqueId)
    {
        try
        {
            var response = await _httpClient.DeleteAsync(_config.GetEndpointUrl($"/api/Estoque/{estoqueId}"));
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
}
