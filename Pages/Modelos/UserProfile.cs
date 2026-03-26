using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace MotoPartsShop.Models
{[Authorize(Roles = "Admin,Gestor")]
    public class UserProfile
    {
        public int Id { get; set; }
        
        [Required]
        public string UserId { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "O primeiro nome é obrigatório")]
        [StringLength(50)]
        [Display(Name = "Primeiro Nome")]
        public string PrimeiroNome { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "O último nome é obrigatório")]
        [StringLength(50)]
        [Display(Name = "Último Nome")]
        public string UltimoNome { get; set; } = string.Empty;
        
        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        public DateTime? DataNascimento { get; set; }
        
        [StringLength(20)]
        [Display(Name = "Telefone")]
        public string? Telefone { get; set; }
        
        [StringLength(200)]
        [Display(Name = "Morada")]
        public string? Morada { get; set; }
        
        [StringLength(10)]
        [Display(Name = "Código Postal")]
        public string? CodigoPostal { get; set; }
        
        [StringLength(100)]
        [Display(Name = "Cidade")]
        public string? Cidade { get; set; }
        
        [StringLength(500)]
        [Display(Name = "URL da Foto de Perfil")]
        public string? FotoPerfilUrl { get; set; }
        
        // Propriedade calculada
        public string NomeCompleto => $"{PrimeiroNome} {UltimoNome}";
    }
}