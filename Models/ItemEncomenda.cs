using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotoPartsShop.Models
{
    public class ItemEncomenda
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Encomenda")]
        public int EncomendaId { get; set; }

        [Required]
        [Display(Name = "Peça")]
        public int PecaId { get; set; }

        [Required(ErrorMessage = "A quantidade é obrigatória")]
        [Range(1, 1000, ErrorMessage = "A quantidade deve estar entre 1 e 1000")]
        [Display(Name = "Quantidade")]
        public int Quantidade { get; set; }

        [Required]
        [Display(Name = "Preço Unitário")]
        [Column(TypeName = "decimal(18,2)")]
        [DataType(DataType.Currency)]
        public decimal PrecoUnitario { get; set; }

        [Display(Name = "Desconto (%)")]
        [Range(0, 100)]
        [Column(TypeName = "decimal(5,2)")]
        public decimal Desconto { get; set; } = 0;

        // Propriedade calculada
        [Display(Name = "Subtotal")]
        [NotMapped]
        public decimal Subtotal => Quantidade * PrecoUnitario * (1 - Desconto / 100);

        // Relações
        [Display(Name = "Encomenda")]
        public Encomenda? Encomenda { get; set; }

        [Display(Name = "Peça")]
        public Peca? Peca { get; set; }
    }
}