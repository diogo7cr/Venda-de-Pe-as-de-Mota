using System.ComponentModel.DataAnnotations;

namespace MotoPartsShop.Models
{
    public class NotificacaoStock
    {
        public int Id { get; set; }
        
        [Required]
        public int PecaId { get; set; }
        public Peca? Peca { get; set; }
        
        [Required]
        public string UserId { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        [StringLength(200)]
        public string Email { get; set; } = string.Empty;
        
        public DateTime DataRegisto { get; set; } = DateTime.Now;
        
        public bool Notificado { get; set; } = false;
        
        public DateTime? DataNotificacao { get; set; }
    }
}