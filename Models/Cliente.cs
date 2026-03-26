using System.ComponentModel.DataAnnotations;

namespace MotoPartsShop.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres")]
        [Display(Name = "Nome")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "O telefone é obrigatório")]
        [Phone(ErrorMessage = "Telefone inválido")]
        [StringLength(20)]
        [Display(Name = "Telefone")]
        public string Telefone { get; set; } = string.Empty;

        [Required(ErrorMessage = "A morada é obrigatória")]
        [StringLength(200)]
        [Display(Name = "Morada")]
        public string Morada { get; set; } = string.Empty;

        [Required(ErrorMessage = "O NIF é obrigatório")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "O NIF deve ter 9 dígitos")]
        [Display(Name = "NIF")]
        public string NIF { get; set; } = string.Empty;

        [Display(Name = "Data de Registo")]
        [DataType(DataType.Date)]
        public DateTime DataRegisto { get; set; } = DateTime.Now;

        // Relação com Encomendas
        public ICollection<Encomenda>? Encomendas { get; set; }
    }
}