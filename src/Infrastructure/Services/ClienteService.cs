using System.Net.Http.Json;
using NinxERP.Domain.DTOs;
using NinxERP.Domain.Interfaces;
using NinxERP.Infrastructure.Config;

namespace NinxERP.Infrastructure.Services
{
    public class ClienteService : IClienteService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiConfiguration _config;
        private readonly string BaseUri;

        public ClienteService(HttpClient httpClient, ApiConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
            BaseUri = _config.GetEndpointUrl("/api/Cliente");
        }


        public async Task<PaginatedResponse<ClienteDTO>> GetPagedAsync(int page, int pageSize, string searchTerm, string status)
        {
            var url = ($"{BaseUri}/All?PageNumber={page}&PageSize={pageSize}");

            var response = await _httpClient.GetFromJsonAsync<PaginatedResponse<ClienteDTO>>(url);
            return response ?? new PaginatedResponse<ClienteDTO>();
        }

        public async Task<ClienteDTO?> GetByIdAsync(int id) =>
            await _httpClient.GetFromJsonAsync<ClienteDTO>($"{BaseUri}/{id}");

        public async Task<ClienteDTO> CreateAsync(ClienteDTO cliente)
        {
            var response = await _httpClient.PostAsJsonAsync(BaseUri, cliente);
            response.EnsureSuccessStatusCode();
            return (await response.Content.ReadFromJsonAsync<ClienteDTO>()) ?? cliente;
        }

        public async Task UpdateAsync(ClienteDTO cliente)
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseUri}/{cliente.ClienteID}", cliente);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUri}/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}