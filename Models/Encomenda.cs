using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotoPartsShop.Models
{
    public class Encomenda
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Cliente")]
        public int ClienteId { get; set; }

        [Display(Name = "Data da Encomenda")]
        [DataType(DataType.Date)]
        public DateTime DataEncomenda { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "O estado é obrigatório")]
        [StringLength(50)]
        [Display(Name = "Estado")]
        public string Estado { get; set; } = "Pendente"; // Pendente, Em Processamento, Enviada, Entregue, Cancelada

        [Display(Name = "Valor Total")]
        [Column(TypeName = "decimal(18,2)")]
        [DataType(DataType.Currency)]
        public decimal ValorTotal { get; set; }

        [Display(Name = "Observações")]
        [StringLength(500)]
        public string? Observacoes { get; set; }

        // Relações
        [Display(Name = "Cliente")]
        public Cliente? Cliente { get; set; }

        public ICollection<ItemEncomenda>? ItensEncomenda { get; set; }
    }
}