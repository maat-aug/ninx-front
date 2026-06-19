using NinxERP.Domain.DTOs;

namespace NinxERP.Domain.Interfaces;

public interface IProdutoService
{
    Task<List<ProdutoDTO>> GetAllAsync();
    Task<ProdutoDTO?> GetByIdAsync(int produtoId);
    Task<ProdutoDTO?> GetByCodigoBarrasAsync(string codigoBarras);
    Task<ProdutoDTO?> GetByNomeAsync(string nome);
    Task<bool> CreateAsync(CriarProdutoDTO dto);
    Task<bool> UpdateAsync(int produtoId, AtualizarProdutoDTO dto);
    Task<bool> DeleteAsync(int produtoId);
}
