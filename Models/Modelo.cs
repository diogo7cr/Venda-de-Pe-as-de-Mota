using System.ComponentModel.DataAnnotations;

namespace MotoPartsShop.Models
{
    public class Modelo
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "O nome do modelo é obrigatório")]
        [StringLength(100)]
        [Display(Name = "Nome do Modelo")]
        public string Nome { get; set; } = string.Empty;
        
        [Display(Name = "Ano de Lançamento")]
        public int? AnoLancamento { get; set; }
        
        [Display(Name = "Cilindrada (cc)")]
        public int? Cilindrada { get; set; }
        
        // Chave estrangeira para Marca
        [Required]
        [Display(Name = "Marca")]
        public int MarcaId { get; set; }
        public Marca? Marca { get; set; }
        
        // Relação: Um modelo tem várias peças
        public ICollection<Peca>? Pecas { get; set; }
    }
}