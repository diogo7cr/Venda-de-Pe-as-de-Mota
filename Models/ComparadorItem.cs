namespace MotoPartsShop.Models
{
    public class ComparadorItem
    {
        public int PecaId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Referencia { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public int Stock { get; set; }
        public string? ImagemUrl { get; set; }
        public string? CategoriaNome { get; set; }
        public string? MarcaNome { get; set; }
        public string? ModeloNome { get; set; }
        public bool PecaOriginal { get; set; }
        public decimal? Peso { get; set; }
        public string? Descricao { get; set; }
    }
}