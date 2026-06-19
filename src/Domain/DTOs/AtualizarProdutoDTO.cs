namespace NinxERP.Domain.DTOs;

public class AtualizarProdutoDTO
{
    public int?    CategoriaID   { get; set; }
    public string   Nome          { get; set; } = string.Empty;
    public string?   CodigoBarras  { get; set; } = string.Empty;
    public decimal  PrecoVenda    { get; set; }
    public decimal?  PrecoCusto    { get; set; }
    public string   UnidadeMedida { get; set; } = string.Empty;
    public DateTime? Validade      { get; set; }
    public bool     Ativo         { get; set; } = true;
}

