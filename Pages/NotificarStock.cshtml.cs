using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MotoPartsShop.Data;
using MotoPartsShop.Models;

namespace MotoPartsShop.Pages
{
    [Authorize]
    public class NotificarStockModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public NotificarStockModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public int PecaId { get; set; }

        [BindProperty]
        public string Email { get; set; } = string.Empty;

        public string? PecaNome { get; set; }
        public string? Mensagem { get; set; }
        public bool Sucesso { get; set; }

        public async Task<IActionResult> OnGetAsync(int? pecaId)
        {
            if (pecaId == null)
            {
                return RedirectToPage("/Pecas/Index");
            }

            var peca = await _context.Pecas.FindAsync(pecaId);
            if (peca == null)
            {
                return NotFound();
            }

            PecaId = pecaId.Value;
            PecaNome = peca.Nome;

            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                Email = user.Email ?? "";
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            var peca = await _context.Pecas.FindAsync(PecaId);
            if (peca == null)
            {
                return NotFound();
            }

            PecaNome = peca.Nome;

            // Verificar se já existe uma notificação pendente
            var notificacaoExistente = await _context.NotificacoesStock
                .FirstOrDefaultAsync(n => n.PecaId == PecaId && n.UserId == user.Id && !n.Notificado);

            if (notificacaoExistente != null)
            {
                Mensagem = "Já tens uma notificação ativa para este produto.";
                Sucesso = false;
                return Page();
            }

            // Criar nova notificação
            var notificacao = new NotificacaoStock
            {
                PecaId = PecaId,
                UserId = user.Id,
                Email = Email,
                DataRegisto = DateTime.Now,
                Notificado = false
            };

            _context.NotificacoesStock.Add(notificacao);
            await _context.SaveChangesAsync();

            Mensagem = "Notificação registada com sucesso! Vamos avisar-te quando o produto voltar ao stock.";
            Sucesso = true;

            return Page();
        }
    }
}