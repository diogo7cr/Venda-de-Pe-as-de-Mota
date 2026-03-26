using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotoPartsShop.Models
{
    public class Peca
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "O nome da peça é obrigatório")]
        [StringLength(200)]
        [Display(Name = "Nome da Peça")]
        public string Nome { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "A referência é obrigatória")]
        [StringLength(50)]
        [Display(Name = "Referência")]
        public string Referencia { get; set; } = string.Empty;
        
        [StringLength(1000)]
        [Display(Name = "Descrição")]
        public string? Descricao { get; set; }
        
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Preço (€)")]
        [Range(0.01, 999999.99, ErrorMessage = "O preço deve ser maior que 0")]
        public decimal Preco { get; set; }
        
        [Required]
        [Display(Name = "Stock Disponível")]
        [Range(0, int.MaxValue, ErrorMessage = "O stock não pode ser negativo")]
        public int Stock { get; set; }
        
        [StringLength(500)]
        [Display(Name = "URL da Imagem")]
        public string? ImagemUrl { get; set; }
        
        [Display(Name = "Peso (kg)")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal? Peso { get; set; }
        
        [Display(Name = "Peça Original")]
        public bool PecaOriginal { get; set; }
        
        // Chaves estrangeiras
        [Required]
        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }
        
        [Required]
        [Display(Name = "Modelo")]
        public int ModeloId { get; set; }
        public Modelo? Modelo { get; set; }
    }
}