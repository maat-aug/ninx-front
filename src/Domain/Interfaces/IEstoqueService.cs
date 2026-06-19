using NinxERP.Domain.DTOs;

namespace NinxERP.Domain.Interfaces;

public interface IEstoqueService
{
    Task<List<EstoqueDTO>> GetAllAsync(PaginacaoDTO paginacao);
    Task<EstoqueDTO?> GetByIdAsync(int estoqueId);
    Task<bool> CreateAsync(AtualizarEstoqueDTO dto);
    Task<bool> UpdateAsync(int estoqueId, AtualizarEstoqueDTO dto);
    Task<bool> DeleteAsync(int estoqueId);
}
