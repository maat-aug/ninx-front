namespace NinxERP.Domain.DTOs;

public class EstoqueDTO
{
    public int      EstoqueID        { get; set; }
    public int      ProdutoID        { get; set; }
    public int      ComercioID       { get; set; }
    public int      Quantidade       { get; set; }
    public int      QuantidadeMinima { get; set; }
    public DateTime UltimaAtualizacao { get; set; }
    public DateTime AtualizadoEm    { get; set; }
}
