using NinxERP.Domain.DTOs;

namespace NinxERP.Domain.Interfaces
{
    public interface IClienteService
    {
        Task<PaginatedResponse<ClienteDTO>> GetPagedAsync(int page, int pageSize, string searchTerm, string status);
        Task<ClienteDTO?> GetByIdAsync(int id);
        Task<ClienteDTO> CreateAsync(ClienteDTO cliente);
        Task UpdateAsync(ClienteDTO cliente);
        Task DeleteAsync(int id);
    }
}