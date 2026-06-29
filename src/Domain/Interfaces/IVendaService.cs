using NinxERP.Domain.DTOs;

namespace NinxERP.Domain.Interfaces;

public interface IVendaService
{
    Task<ProdutoDTO?> GetProdutoPorCodigoBarrasAsync(string codigoBarras);
    Task<List<ClienteDTO>> BuscarClientesAsync(string nome);
    Task<VendaResponseDTO?> RealizarVendaAsync(VendaRequestDTO request);
    Task<AssinaturaRetornoDTO?> GetDocumentoById(Guid documentoGuid);
    Task<bool> IsVendaValidAsync(int vendaId);
    Task<bool> DesistirVendaAsync(int vendaId);
    Task<bool> VerificarDocumentoAssinadoAsync(Guid documentoGuid);
    Task<IEnumerable<VendaHistoricoDTO>> BuscarVendaPorCliente(int clienteId);
    Task<Guid> ReceberPagamentoGeralFiadoAsync(int clienteId, decimal valorTotalPago, int formaPagamento);
    Task<Guid> ReceberPagamentoFiadoAsync(int vendaId, decimal valorPago, int formaPagamento);
}
