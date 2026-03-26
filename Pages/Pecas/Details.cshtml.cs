using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MotoPartsShop.Data;
using MotoPartsShop.Models;

namespace MotoPartsShop.Pages_Pecas
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DetailsModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Peca Peca { get; set; } = default!;
        public List<Avaliacao> Avaliacoes { get; set; } = new();
        public double MediaAvaliacoes { get; set; }
        public int TotalAvaliacoes { get; set; }
        public bool JaAvaliou { get; set; }

        [BindProperty]
        public int Estrelas { get; set; }

        [BindProperty]
        public string? Comentario { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var peca = await _context.Pecas
                .Include(p => p.Categoria)
                .Include(p => p.Modelo)
                .ThenInclude(m => m!.Marca)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (peca == null)
            {
                return NotFound();
            }

            Peca = peca;

            // Carregar avaliações
            Avaliacoes = await _context.Avaliacoes
                .Where(a => a.PecaId == id)
                .OrderByDescending(a => a.DataAvaliacao)
                .ToListAsync();

            TotalAvaliacoes = Avaliacoes.Count;
            MediaAvaliacoes = TotalAvaliacoes > 0 ? Avaliacoes.Average(a => a.Estrelas) : 0;

            // Verificar se o utilizador já avaliou
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                JaAvaliou = await _context.Avaliacoes
                    .AnyAsync(a => a.PecaId == id && a.UserId == user.Id);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAvaliarAsync(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            // Verificar se já avaliou
            var avaliacaoExistente = await _context.Avaliacoes
                .FirstOrDefaultAsync(a => a.PecaId == id && a.UserId == user.Id);

            if (avaliacaoExistente != null)
            {
                return RedirectToPage(new { id });
            }

            var avaliacao = new Avaliacao
            {
                PecaId = id,
                UserId = user.Id,
                NomeUtilizador = user.Email?.Split('@')[0] ?? "Utilizador",
                Estrelas = Estrelas,
                Comentario = Comentario,
                DataAvaliacao = DateTime.Now,
                Verificado = false
            };

            _context.Avaliacoes.Add(avaliacao);
            await _context.SaveChangesAsync();

            return RedirectToPage(new { id });
        }
    }
}