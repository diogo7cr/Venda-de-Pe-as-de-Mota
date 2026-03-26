using System.ComponentModel.DataAnnotations;

namespace MotoPartsShop.Models
{
    public class Favorito
    {
        public int Id { get; set; }
        
        [Required]
        public string UserId { get; set; } = string.Empty;
        
        [Required]
        public int PecaId { get; set; }
        public Peca? Peca { get; set; }
        
        public DateTime DataAdicao { get; set; } = DateTime.Now;
    }
}