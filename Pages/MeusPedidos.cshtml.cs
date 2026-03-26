using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MotoPartsShop.Data;
using MotoPartsShop.Models;

namespace MotoPartsShop.Pages
{
    [Authorize]
    public class MeusPedidosModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public MeusPedidosModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<Pedido> Pedidos { get; set; } = new();

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                Pedidos = await _context.Pedidos
                    .Include(p => p.Itens!)  // <- ADICIONA ! aqui
                        .ThenInclude(i => i.Peca)
                    .Where(p => p.UserId == user.Id)
                    .OrderByDescending(p => p.DataPedido)
                    .ToListAsync();
            }
        }
    }
}