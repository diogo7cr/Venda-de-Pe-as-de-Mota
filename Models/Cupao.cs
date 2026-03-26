using System.ComponentModel.DataAnnotations;

namespace MotoPartsShop.Models
{
    public class Cupao
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O código é obrigatório")]
        [StringLength(20, ErrorMessage = "O código deve ter no máximo 20 caracteres")]
        public string Codigo { get; set; } = string.Empty;

        [Required(ErrorMessage = "A descrição é obrigatória")]
        [StringLength(200)]
        public string Descricao { get; set; } = string.Empty;

        [Required]
        [Range(0, 100, ErrorMessage = "A percentagem deve estar entre 0 e 100")]
        public decimal PercentagemDesconto { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DataInicio { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DataFim { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "O valor mínimo deve ser positivo")]
        public decimal ValorMinimo { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "O limite de utilizações deve ser pelo menos 1")]
        public int? LimiteUtilizacoes { get; set; }

        public int UtilizacoesAtuais { get; set; } = 0;

        public bool Ativo { get; set; } = true;

        // Método para verificar se o cupão é válido
        public bool IsValido(decimal valorCompra, out string mensagemErro)
        {
            mensagemErro = string.Empty;

            if (!Ativo)
            {
                mensagemErro = "Este cupão não está ativo.";
                return false;
            }

            var hoje = DateTime.Now.Date;
            if (hoje < DataInicio.Date || hoje > DataFim.Date)
            {
                mensagemErro = $"Este cupão é válido apenas de {DataInicio:dd/MM/yyyy} a {DataFim:dd/MM/yyyy}.";
                return false;
            }

            if (valorCompra < ValorMinimo)
            {
                mensagemErro = $"Valor mínimo de compra: €{ValorMinimo:F2}";
                return false;
            }

            if (LimiteUtilizacoes.HasValue && UtilizacoesAtuais >= LimiteUtilizacoes.Value)
            {
                mensagemErro = "Este cupão atingiu o limite de utilizações.";
                return false;
            }

            return true;
        }

        // Calcular desconto
        public decimal CalcularDesconto(decimal valorCompra)
        {
            return valorCompra * (PercentagemDesconto / 100);
        }
    }
}