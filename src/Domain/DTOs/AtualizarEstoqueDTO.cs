namespace NinxERP.Domain.DTOs;

public class AtualizarEstoqueDTO
{
    public int ProdutoID        { get; set; }
    public int Quantidade       { get; set; }
    public int QuantidadeMinima { get; set; }
}
