using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MotoPartsShop.Data;
using MotoPartsShop.Models;
using System.Text.Json;

namespace MotoPartsShop.Pages
{
    public class CheckoutModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CheckoutModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<CarrinhoItem> Itens { get; set; } = new();
        public decimal Subtotal => Itens.Sum(i => i.Subtotal);
        public decimal ValorEnvio { get; set; } = 5.00m;
        public decimal Total => Subtotal - ValorDesconto + ValorEnvio;

        [BindProperty]
        public string NomeCliente { get; set; } = string.Empty;

        [BindProperty]
        public string Email { get; set; } = string.Empty;

        [BindProperty]
        public string Telefone { get; set; } = string.Empty;

        [BindProperty]
        public string Morada { get; set; } = string.Empty;

        [BindProperty]
        public string CodigoPostal { get; set; } = string.Empty;

        [BindProperty]
        public string Cidade { get; set; } = string.Empty;

        [BindProperty]
        public string MetodoPagamento { get; set; } = string.Empty;

        // ✅ NOVOS: Propriedades do cupão
        [BindProperty]
        public string? CodigoCupao { get; set; }

        public Cupao? CupaoAplicado { get; set; }
        public decimal ValorDesconto { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            CarregarCarrinho();
            
            if (!Itens.Any())
            {
                return RedirectToPage("/Carrinho");
            }

            // ✅ NOVO: Carregar cupão aplicado
            var codigoCupao = HttpContext.Session.GetString("CupaoAplicado");
            if (!string.IsNullOrEmpty(codigoCupao))
            {
                CupaoAplicado = await _context.Cupoes
                    .FirstOrDefaultAsync(c => c.Codigo == codigoCupao);

                if (CupaoAplicado != null)
                {
                    var subtotal = Itens.Sum(i => i.Subtotal);
                    if (CupaoAplicado.IsValido(subtotal, out _))
                    {
                        ValorDesconto = CupaoAplicado.CalcularDesconto(subtotal);
                    }
                    else
                    {
                        // Cupão já não é válido, remover
                        HttpContext.Session.Remove("CupaoAplicado");
                        CupaoAplicado = null;
                    }
                }
            }

            // Envio grátis para compras acima de €50
            if (Subtotal >= 50)
            {
                ValorEnvio = 0;
            }
            
            return Page();
        }

        // ✅ NOVO: Método para aplicar cupão
        public async Task<IActionResult> OnPostAplicarCupaoAsync()
        {
            if (string.IsNullOrWhiteSpace(CodigoCupao))
            {
                TempData["ErroCupao"] = "Por favor, insira um código de cupão.";
                return RedirectToPage();
            }

            // Buscar cupão
            var cupao = await _context.Cupoes
                .FirstOrDefaultAsync(c => c.Codigo.ToUpper() == CodigoCupao.ToUpper());

            if (cupao == null)
            {
                TempData["ErroCupao"] = "Cupão inválido ou inexistente.";
                return RedirectToPage();
            }

            // Calcular subtotal do carrinho
            CarregarCarrinho();
            var subtotal = Itens.Sum(i => i.Subtotal);

            // Validar cupão
            if (!cupao.IsValido(subtotal, out string mensagemErro))
            {
                TempData["ErroCupao"] = mensagemErro;
                return RedirectToPage();
            }

            // Guardar cupão na sessão
            HttpContext.Session.SetString("CupaoAplicado", cupao.Codigo);
            TempData["SucessoCupao"] = $"Cupão '{cupao.Codigo}' aplicado com sucesso! Desconto de {cupao.PercentagemDesconto}%";

            return RedirectToPage();
        }

        // ✅ NOVO: Método para remover cupão
        public IActionResult OnPostRemoverCupaoAsync()
        {
            HttpContext.Session.Remove("CupaoAplicado");
            TempData["InfoCupao"] = "Cupão removido.";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            CarregarCarrinho();

            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Obter utilizador atual
            var user = await _userManager.GetUserAsync(User);
            var userId = user?.Id ?? "guest";

            // Calcular valores
            decimal subtotal = Itens.Sum(i => i.Subtotal);
            decimal valorDesconto = 0;
            decimal valorEnvio = subtotal >= 50 ? 0 : 5.00m;

            // ✅ NOVO: Aplicar desconto do cupão
            var codigoCupao = HttpContext.Session.GetString("CupaoAplicado");
            if (!string.IsNullOrEmpty(codigoCupao))
            {
                var cupao = await _context.Cupoes
                    .FirstOrDefaultAsync(c => c.Codigo == codigoCupao);

                if (cupao != null && cupao.IsValido(subtotal, out _))
                {
                    valorDesconto = cupao.CalcularDesconto(subtotal);

                    // Incrementar utilizações
                    cupao.UtilizacoesAtuais++;
                }
            }

            decimal total = subtotal - valorDesconto + valorEnvio;

            // Criar pedido
            var pedido = new Pedido
            {
                UserId = userId,
                DataPedido = DateTime.Now,
                Total = total,
                Estado = "Pendente",
                NomeCliente = NomeCliente,
                Email = Email,
                Telefone = Telefone,
                Morada = Morada,
                CodigoPostal = CodigoPostal,
                Cidade = Cidade,
                MetodoPagamento = MetodoPagamento,
                Itens = new List<PedidoItem>()
            };

            // Adicionar itens do pedido
            foreach (var item in Itens)
            {
                pedido.Itens.Add(new PedidoItem
                {
                    PecaId = item.PecaId,
                    NomePeca = item.Nome,
                    Referencia = $"REF-{item.PecaId}",
                    PrecoUnitario = item.Preco,
                    Quantidade = item.Quantidade
                });

                // Atualizar stock
                var peca = await _context.Pecas.FindAsync(item.PecaId);
                if (peca != null)
                {
                    peca.Stock -= item.Quantidade;
                }
            }

            // Guardar na base de dados
            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            // Limpar carrinho e cupão
            HttpContext.Session.Remove("Carrinho");
            HttpContext.Session.Remove("CupaoAplicado");

            // Redirecionar para página de sucesso
            return RedirectToPage("/Sucesso", new { pedidoId = pedido.Id });
        }

        private void CarregarCarrinho()
        {
            var carrinhoJson = HttpContext.Session.GetString("Carrinho");
            if (!string.IsNullOrEmpty(carrinhoJson))
            {
                Itens = JsonSerializer.Deserialize<List<CarrinhoItem>>(carrinhoJson) ?? new();
            }
        }
    }
}