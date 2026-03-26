using System.ComponentModel.DataAnnotations;

namespace MotoPartsShop.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "O nome da categoria é obrigatório")]
        [StringLength(100)]
        [Display(Name = "Nome da Categoria")]
        public string Nome { get; set; } = string.Empty;
        
        [StringLength(500)]
        [Display(Name = "Descrição")]
        public string? Descricao { get; set; }
        
        // Relação: Uma categoria tem várias peças
        public ICollection<Peca>? Pecas { get; set; }
    }
}