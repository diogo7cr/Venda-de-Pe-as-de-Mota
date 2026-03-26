using System.ComponentModel.DataAnnotations;

namespace MotoPartsShop.Models
{
    public class Marca
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "O nome da marca é obrigatório")]
        [StringLength(100)]
        [Display(Name = "Nome da Marca")]
        public string Nome { get; set; } = string.Empty;
        
        [StringLength(100)]
        [Display(Name = "País de Origem")]
        public string? PaisOrigem { get; set; }
        
        // Relação: Uma marca tem vários modelos
        public ICollection<Modelo>? Modelos { get; set; }
    }
}