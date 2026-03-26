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
    public class MeusFavoritosModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public MeusFavoritosModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<Favorito> Favoritos { get; set; } = new();

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                Favoritos = await _context.Favoritos
                    .Include(f => f.Peca)
                        .ThenInclude(p => p!.Categoria)
                    .Include(f => f.Peca)
                        .ThenInclude(p => p!.Modelo)
                            .ThenInclude(m => m!.Marca)
                    .Where(f => f.UserId == user.Id)
                    .OrderByDescending(f => f.DataAdicao)
                    .ToListAsync();
            }
        }

        public async Task<IActionResult> OnPostAdicionarAsync(int pecaId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            // Verificar se já existe
            var favoritoExistente = await _context.Favoritos
                .FirstOrDefaultAsync(f => f.UserId == user.Id && f.PecaId == pecaId);

            if (favoritoExistente == null)
            {
                var favorito = new Favorito
                {
                    UserId = user.Id,
                    PecaId = pecaId,
                    DataAdicao = DateTime.Now
                };

                _context.Favoritos.Add(favorito);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostRemoverAsync(int favoritoId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            var favorito = await _context.Favoritos
                .FirstOrDefaultAsync(f => f.Id == favoritoId && f.UserId == user.Id);

            if (favorito != null)
            {
                _context.Favoritos.Remove(favorito);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}