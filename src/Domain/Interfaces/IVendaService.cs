using NinxERP.Domain.DTOs;

namespace NinxERP.Domain.Interfaces;

public interface IVendaService
{
    Task<ProdutoDTO?> GetProdutoPorCodigoBarrasAsync(string codigoBarras);
    Task<List<ClienteDTO>> BuscarClientesAsync(string nome);
    Task<VendaResponseDTO?> RealizarVendaAsync(VendaRequestDTO request);
    Task<AssinaturaResponseDTO?> GetLinkAssinaturaAsync(Guid documentoGuid);
    Task<bool> IsVendaValidAsync(int vendaId);
    Task<bool> DesistirVendaAsync(int vendaId);
    Task<bool> VerificarDocumentoAssinadoAsync(Guid documentoGuid);
}
