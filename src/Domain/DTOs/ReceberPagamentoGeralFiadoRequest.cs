namespace NinxERP.Domain.DTOs
{
    public class ReceberPagamentoGeralFiadoRequest
    {
        public decimal ValorPago { get; set; }
        public FormaPagamentoEnum FormaPagamento { get; set; }
    }
}
