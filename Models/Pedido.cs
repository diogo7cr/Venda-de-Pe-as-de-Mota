using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotoPartsShop.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        
        [Required]
        public string UserId { get; set; } = string.Empty;
        
        [Required]
        public DateTime DataPedido { get; set; } = DateTime.Now;
        
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Estado { get; set; } = "Pendente"; // Pendente, Processando, Enviado, Entregue, Cancelado
        
        // Dados de Envio
        [Required]
        [StringLength(200)]
        public string NomeCliente { get; set; } = string.Empty;
        
        [Required]
        [StringLength(200)]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        [StringLength(20)]
        public string Telefone { get; set; } = string.Empty;
        
        [Required]
        [StringLength(300)]
        public string Morada { get; set; } = string.Empty;
        
        [Required]
        [StringLength(10)]
        public string CodigoPostal { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string Cidade { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50)]
        public string MetodoPagamento { get; set; } = string.Empty;
        
        // Relação com itens do pedido
        public ICollection<PedidoItem>? Itens { get; set; }
    }
    
    public class PedidoItem
    {
        public int Id { get; set; }
        
        [Required]
        public int PedidoId { get; set; }
        public Pedido? Pedido { get; set; }
        
        [Required]
        public int PecaId { get; set; }
        public Peca? Peca { get; set; }
        
        [Required]
        [StringLength(200)]
        public string NomePeca { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50)]
        public string Referencia { get; set; } = string.Empty;
        
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecoUnitario { get; set; }
        
        [Required]
        public int Quantidade { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Subtotal => PrecoUnitario * Quantidade;
    }
}