using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MotoPartsShop.Data;
using MotoPartsShop.Models;

namespace MotoPartsShop.Pages;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Cupao> CupoesAtivos { get; set; } = new();

    public async Task OnGetAsync()
    {
        var hoje = DateTime.Now.Date;
        CupoesAtivos = await _context.Cupoes
            .Where(c => c.Ativo && c.DataFim >= hoje && c.DataInicio <= hoje)
            .OrderBy(c => c.DataFim)
            .Take(3)
            .ToListAsync();
    }
}