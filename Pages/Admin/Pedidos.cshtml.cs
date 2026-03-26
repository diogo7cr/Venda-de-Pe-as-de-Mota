using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MotoPartsShop.Data;
using MotoPartsShop.Models;

namespace MotoPartsShop.Pages.Admin
{
    [Authorize(Roles = "Admin,Gestor")]
    public class PedidosModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public PedidosModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Pedido> Pedidos { get; set; } = new();
        
        [BindProperty(SupportsGet = true)]
        public string? FiltroEstado { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string? Pesquisa { get; set; }

        public int TotalPedidos { get; set; }
        public decimal ReceitaTotal { get; set; }

        public async Task OnGetAsync()
        {
            var query = _context.Pedidos.AsQueryable();

            // Filtro por estado
            if (!string.IsNullOrEmpty(FiltroEstado) && FiltroEstado != "Todos")
            {
                query = query.Where(p => p.Estado == FiltroEstado);
            }

            // Pesquisa por ID ou nome do cliente
            if (!string.IsNullOrEmpty(Pesquisa))
            {
                query = query.Where(p => 
                    p.NomeCliente.Contains(Pesquisa) || 
                    p.Email.Contains(Pesquisa) ||
                    p.Id.ToString().Contains(Pesquisa));
            }

            Pedidos = await query
                .OrderByDescending(p => p.DataPedido)
                .ToListAsync();

            TotalPedidos = Pedidos.Count;
            ReceitaTotal = Pedidos.Where(p => p.Estado != "Cancelado").Sum(p => p.Total);
        }

        public async Task<IActionResult> OnPostAlterarEstadoAsync(int id, string novoEstado)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            
            if (pedido == null)
            {
                return NotFound();
            }

            pedido.Estado = novoEstado;
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEliminarAsync(int id)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.Itens)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null)
            {
                return NotFound();
            }

            if (pedido.Itens != null && pedido.Itens.Any())
            {
                _context.PedidoItens.RemoveRange(pedido.Itens);
            }
            
            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}