namespace MotoPartsShop.Models
{
    public class CarrinhoItem
    {
        public int PecaId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
        public string? ImagemUrl { get; set; }
        
        public decimal Subtotal => Preco * Quantidade;
    }
}