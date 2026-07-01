using NinxERP.Components.Pages;
using NinxERP.Domain;
using NinxERP.Domain.DTOs;
using NinxERP.Domain.Interfaces;
using NinxERP.Infrastructure.Config;
using System;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace NinxERP.Infrastructure.Services;

public class VendaService : IVendaService
{
    private readonly HttpClient _httpClient;
    private readonly ApiConfiguration _config;

    public VendaService(HttpClient httpClient, ApiConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
    }

    public async Task<ProdutoDTO?> GetProdutoPorCodigoBarrasAsync(string codigoBarras)
    {
        try
        {
            var url = _config.GetEndpointUrl($"/api/Produto/codigo-barras/{Uri.EscapeDataString(codigoBarras)}");
            return await _httpClient.GetFromJsonAsync<ProdutoDTO>(url);
        }
        catch
        {
            return null;
        }
    }

    public async Task<List<ClienteDTO>> BuscarClientesAsync(string nome)
   {
        try
        {
            return await _httpClient.GetFromJsonAsync<List<ClienteDTO>>(_config.GetEndpointUrl($"/api/Cliente/nome/{Uri.EscapeDataString(nome)}")) ?? new();
        }
        catch
        {
            return new();
        }
    }

    public async Task<VendaResponseDTO?> RealizarVendaAsync(VendaRequestDTO request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(_config.GetEndpointUrl("/api/Venda"), request);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<VendaResponseDTO>();
            }
            return null;
        }
        catch
        {
            return null;
        }
    }

    public async Task<AssinaturaRetornoDTO?> GetDocumentoById(Guid documentoGuid)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<AssinaturaRetornoDTO>(_config.GetEndpointUrl($"/api/AssinaturaEletronica/{documentoGuid}"));
        }
        catch
        {
            return null;
        }
    }

    public async Task<bool> IsVendaValidAsync(int vendaId)
    {
        try
        {
            var response = await _httpClient.GetAsync(_config.GetEndpointUrl($"/api/venda/isvendavalid/{vendaId}"));
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DesistirVendaAsync(int vendaId)
    {
        try
        {
            var response = await _httpClient.PostAsync(_config.GetEndpointUrl($"/api/Venda/{vendaId}/estorno"), null);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<IEnumerable<VendaHistoricoDTO>> BuscarVendaPorCliente(int clienteId)
    {
        var response = await _httpClient.GetFromJsonAsync<List<VendaHistoricoDTO>>(_config.GetEndpointUrl($"/api/Venda/cliente/{clienteId}"));
        if (response == null ||  !(response.Count > 0))
        {
            return new List<VendaHistoricoDTO>();
        }

        return response;
    }

    public async Task<bool> VerificarDocumentoAssinadoAsync(Guid documentoGuid)
    {
        try
        {
            var response = await _httpClient.GetAsync(_config.GetEndpointUrl($"/api/AssinaturaEletronica/assinado/{documentoGuid}"));
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<Guid> ReceberPagamentoGeralFiadoAsync(int clienteId, decimal valorTotalPago, FormaPagamentoEnum formaPagamento)
    {
        var requestDto = new ReceberPagamentoGeralFiadoRequest
        {
            ValorPago = valorTotalPago,
            FormaPagamento = formaPagamento
        };

        var response = await _httpClient.PostAsJsonAsync(_config.GetEndpointUrl($"api/Venda/cliente/{clienteId}/pagamento-geral-fiado"), requestDto);

        if (response.IsSuccessStatusCode)
        {
            var guidResult = await response.Content.ReadFromJsonAsync<Guid>();
            return guidResult;
        }

        var erroTexto = await response.Content.ReadAsStringAsync();
        throw new Exception(!string.IsNullOrEmpty(erroTexto) ? erroTexto : "Erro desconhecido ao processar o pagamento global.");
    }

    public async Task<Guid> ReceberPagamentoFiadoAsync(int vendaId, decimal valorPago, FormaPagamentoEnum formaPagamento)
    {
        var requestDto = new ReceberPagamentoGeralFiadoRequest
        {
            ValorPago = valorPago,
            FormaPagamento = formaPagamento
        };

        var response = await _httpClient.PostAsJsonAsync(_config.GetEndpointUrl($"/api/Venda/{vendaId}/pagamento-fiado"), requestDto);

        if (response.IsSuccessStatusCode)
        {
            var guidResult = await response.Content.ReadFromJsonAsync<Guid>();
            return guidResult;
        }

        var erroTexto = await response.Content.ReadAsStringAsync();
        throw new Exception(!string.IsNullOrEmpty(erroTexto) ? erroTexto : "Erro desconhecido ao processar o pagamento individual.");
    }
}
