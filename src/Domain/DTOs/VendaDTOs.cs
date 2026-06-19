namespace NinxERP.Domain.DTOs;

public class ClienteDTO
{
    public int clienteID { get; set; }
    public string Nome { get; set; } = string.Empty;
}

public class VendaRequestDTO
{
    public int ComercioID { get; set; }
    public int UsuarioID { get; set; }
    public int ClienteID { get; set; }
    public string Observacoes { get; set; } = string.Empty;
    public int TipoVenda { get; set; }
    public List<ItemVendaDTO> ItensVenda { get; set; } = new();
    public List<PagamentoDTO> Pagamentos { get; set; } = new();
}

public class ItemVendaDTO
{
    public int ProdutoID { get; set; }
    public double Quantidade { get; set; }
    public string UnidadeMedida { get; set; } = string.Empty;
    public int ComercioId { get; set; }
    // Propriedades auxiliares para UI
    public string NomeProduto { get; set; } = string.Empty;
    public decimal PrecoUnitario { get; set; }
    public decimal Subtotal => (decimal)Quantidade * PrecoUnitario;
}

public class PagamentoDTO
{
    public int FormaPagamento { get; set; }
    public decimal Valor { get; set; }
}

public class VendaResponseDTO
{
    public int VendaID { get; set; }
    public int ComercioID { get; set; }
    public int UsuarioID { get; set; }
    public decimal Total { get; set; }
    public string TipoVenda { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime CriadoEm { get; set; }
    public Guid DocumentoGuid { get; set; }
}

public class AssinaturaResponseDTO
{
    public string Link { get; set; } = string.Empty;
}

public class AssinaturaDetalhesDTO
{
    public Guid DocumentoGuid { get; set; }
    public string NomeCliente { get; set; } = string.Empty;
    public string NomeComercio { get; set; } = string.Empty;
    public decimal ValorTotal { get; set; }
    public DateTime DataVenda { get; set; }
    public List<ItemAssinaturaDTO> Itens { get; set; } = new();
}

public class ItemAssinaturaDTO
{
    public string ProdutoNome { get; set; } = string.Empty;
    public double Quantidade { get; set; }
    public decimal Subtotal { get; set; }
}
