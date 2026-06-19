using NinxERP.Domain.DTOs;
using NinxERP.Domain.Interfaces;
using NinxERP.Infrastructure.Config;
using System.Net.Http.Json;

namespace NinxERP.Infrastructure.Services;

public class ProdutoService : IProdutoService
{
    private readonly HttpClient _httpClient;
    private readonly ApiConfiguration _config;

    public ProdutoService(HttpClient httpClient, ApiConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
    }

    public async Task<List<ProdutoDTO>> GetAllAsync()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<List<ProdutoDTO>>(_config.GetEndpointUrl("/api/Produto/GetAll")) ?? new();
        }
        catch
        {
            return new();
        }
    }

    public async Task<ProdutoDTO?> GetByIdAsync(int produtoId)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<ProdutoDTO>(_config.GetEndpointUrl($"/api/Produto/{produtoId}"));
        }
        catch
        {
            return null;
        }
    }

    public async Task<ProdutoDTO?> GetByCodigoBarrasAsync(string codigoBarras)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<ProdutoDTO>(_config.GetEndpointUrl($"/api/Produto/codigo-barras/{Uri.EscapeDataString(codigoBarras)}"));
        }
        catch
        {
            return null;
        }
    }

    public async Task<ProdutoDTO?> GetByNomeAsync(string nome)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<ProdutoDTO>(_config.GetEndpointUrl($"/api/Produto/produto/{Uri.EscapeDataString(nome)}"));
        }
        catch
        {
            return null;
        }
    }

    public async Task<bool> CreateAsync(CriarProdutoDTO dto)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(_config.GetEndpointUrl("/api/Produto"), dto);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> UpdateAsync(int produtoId, AtualizarProdutoDTO dto)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync(_config.GetEndpointUrl($"/api/Produto/{produtoId}"), dto);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteAsync(int produtoId)
    {
        try
        {
            var response = await _httpClient.DeleteAsync(_config.GetEndpointUrl($"/api/Produto/{produtoId}"));
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
}
