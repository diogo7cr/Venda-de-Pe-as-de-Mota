using System.ComponentModel.DataAnnotations;

namespace MotoPartsShop.Models
{
    public class Avaliacao
    {
        public int Id { get; set; }
        
        [Required]
        public int PecaId { get; set; }
        public Peca? Peca { get; set; }
        
        [Required]
        public string UserId { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string NomeUtilizador { get; set; } = string.Empty;
        
        [Required]
        [Range(1, 5)]
        public int Estrelas { get; set; }
        
        [StringLength(1000)]
        public string? Comentario { get; set; }
        
        public DateTime DataAvaliacao { get; set; } = DateTime.Now;
        
        public bool Verificado { get; set; } = false; // Comprou a peça?
    }
}