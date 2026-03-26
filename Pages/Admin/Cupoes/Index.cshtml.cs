using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MotoPartsShop.Data;
using MotoPartsShop.Models;

namespace MotoPartsShop.Pages.Admin.Cupoes
{
    [Authorize(Roles = "Admin,Gestor")]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Cupao> Cupoes { get; set; } = default!;
        public int CupoesAtivos { get; set; }
        public int CupoesExpirados { get; set; }
        public int TotalUtilizacoes { get; set; }

        public async Task OnGetAsync()
        {
            Cupoes = await _context.Cupoes
                .OrderByDescending(c => c.DataInicio)
                .ToListAsync();

            var hoje = DateTime.Now.Date;
            CupoesAtivos = Cupoes.Count(c => c.Ativo && c.DataFim >= hoje);
            CupoesExpirados = Cupoes.Count(c => c.DataFim < hoje);
            TotalUtilizacoes = Cupoes.Sum(c => c.UtilizacoesAtuais);
        }
    }
}