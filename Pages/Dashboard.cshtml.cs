using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MotoPartsShop.Data;
using MotoPartsShop.Models;

namespace MotoPartsShop.Pages
{
    [Authorize(Roles = "Admin,Gestor")]
    public class DashboardModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DashboardModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // Estatísticas Gerais
        public int TotalPecas { get; set; }
        public int TotalCategorias { get; set; }
        public int TotalMarcas { get; set; }
        public int TotalModelos { get; set; }
        public decimal ValorTotalStock { get; set; }
        public int PecasEmStock { get; set; }
        public int PecasSemStock { get; set; }

        // Estatísticas de Vendas
        public int TotalPedidos { get; set; }
        public int PedidosPendentes { get; set; }
        public int PedidosEntregues { get; set; }
        public decimal ReceitaTotal { get; set; }
        public decimal ReceitaMesAtual { get; set; }
        public decimal ReceitaMesAnterior { get; set; }

        // Listas
        public List<PecaStockBaixo> PecasStockBaixo { get; set; } = new();
        public List<CategoriaEstatistica> TopCategorias { get; set; } = new();
        public List<PedidoRecente> PedidosRecentes { get; set; } = new();
        public List<PecaMaisVendida> PecasMaisVendidas { get; set; } = new();

        // Dados para gráficos (JSON)
        public string VendasPorMesJson { get; set; } = "[]";
        public string VendasPorCategoriaJson { get; set; } = "[]";
        public string PedidosPorEstadoJson { get; set; } = "[]";

        public async Task OnGetAsync()
        {
            // Estatísticas de Produtos
            TotalPecas = await _context.Pecas.CountAsync();
            TotalCategorias = await _context.Categorias.CountAsync();
            TotalMarcas = await _context.Marcas.CountAsync();
            TotalModelos = await _context.Modelos.CountAsync();
            ValorTotalStock = await _context.Pecas.SumAsync(p => p.Preco * p.Stock);
            PecasEmStock = await _context.Pecas.CountAsync(p => p.Stock > 0);
            PecasSemStock = await _context.Pecas.CountAsync(p => p.Stock == 0);

            // Estatísticas de Pedidos
            TotalPedidos = await _context.Pedidos.CountAsync();
            PedidosPendentes = await _context.Pedidos.CountAsync(p => p.Estado == "Pendente" || p.Estado == "Processando");
            PedidosEntregues = await _context.Pedidos.CountAsync(p => p.Estado == "Entregue");
            ReceitaTotal = await _context.Pedidos
                .Where(p => p.Estado != "Cancelado")
                .SumAsync(p => p.Total);

            var mesAtual = DateTime.Now.Month;
            var anoAtual = DateTime.Now.Year;
            ReceitaMesAtual = await _context.Pedidos
                .Where(p => p.DataPedido.Month == mesAtual && p.DataPedido.Year == anoAtual && p.Estado != "Cancelado")
                .SumAsync(p => p.Total);

            var mesAnterior = mesAtual == 1 ? 12 : mesAtual - 1;
            var anoAnterior = mesAtual == 1 ? anoAtual - 1 : anoAtual;
            ReceitaMesAnterior = await _context.Pedidos
                .Where(p => p.DataPedido.Month == mesAnterior && p.DataPedido.Year == anoAnterior && p.Estado != "Cancelado")
                .SumAsync(p => p.Total);

            // Peças com stock baixo
            PecasStockBaixo = await _context.Pecas
                .Where(p => p.Stock > 0 && p.Stock < 10)
                .OrderBy(p => p.Stock)
                .Select(p => new PecaStockBaixo
                {
                    Nome = p.Nome,
                    Stock = p.Stock,
                    Referencia = p.Referencia,
                    Id = p.Id
                })
                .Take(5)
                .ToListAsync();

            // Top 5 categorias
            TopCategorias = await _context.Categorias
                .Select(c => new CategoriaEstatistica
                {
                    Nome = c.Nome,
                    TotalPecas = c.Pecas!.Count()
                })
                .OrderByDescending(c => c.TotalPecas)
                .Take(5)
                .ToListAsync();

            // Pedidos recentes
            PedidosRecentes = await _context.Pedidos
                .OrderByDescending(p => p.DataPedido)
                .Select(p => new PedidoRecente
                {
                    Id = p.Id,
                    DataPedido = p.DataPedido,
                    Total = p.Total,
                    Estado = p.Estado,
                    NomeCliente = p.NomeCliente
                })
                .Take(10)
                .ToListAsync();

            // Peças mais vendidas
            PecasMaisVendidas = await _context.PedidoItens
                .GroupBy(pi => new { pi.PecaId, pi.NomePeca })
                .Select(g => new PecaMaisVendida
                {
                    PecaId = g.Key.PecaId,
                    Nome = g.Key.NomePeca,
                    QuantidadeVendida = g.Sum(pi => pi.Quantidade),
                    ReceitaTotal = g.Sum(pi => pi.PrecoUnitario * pi.Quantidade)
                })
                .OrderByDescending(p => p.QuantidadeVendida)
                .Take(5)
                .ToListAsync();

            // Preparar dados para gráficos
            await PrepararDadosGraficos();
        }

        private async Task PrepararDadosGraficos()
        {
            // Vendas dos últimos 6 meses
            var vendasMeses = new List<object>();
            for (int i = 5; i >= 0; i--)
            {
                var data = DateTime.Now.AddMonths(-i);
                var receita = await _context.Pedidos
                    .Where(p => p.DataPedido.Month == data.Month && p.DataPedido.Year == data.Year && p.Estado != "Cancelado")
                    .SumAsync(p => p.Total);
                
                vendasMeses.Add(new
                {
                    mes = data.ToString("MMM yyyy"),
                    receita = receita
                });
            }
            VendasPorMesJson = System.Text.Json.JsonSerializer.Serialize(vendasMeses);

            // Vendas por categoria
            var vendasCategorias = await _context.PedidoItens
                .Include(pi => pi.Peca)
                .ThenInclude(p => p!.Categoria)
                .Where(pi => pi.Peca != null && pi.Peca.Categoria != null)
                .GroupBy(pi => pi.Peca!.Categoria!.Nome)
                .Select(g => new
                {
                    categoria = g.Key,
                    total = g.Sum(pi => pi.PrecoUnitario * pi.Quantidade)
                })
                .ToListAsync();
            VendasPorCategoriaJson = System.Text.Json.JsonSerializer.Serialize(vendasCategorias);

            // Pedidos por estado
            var pedidosEstado = await _context.Pedidos
                .GroupBy(p => p.Estado)
                .Select(g => new
                {
                    estado = g.Key,
                    quantidade = g.Count()
                })
                .ToListAsync();
            PedidosPorEstadoJson = System.Text.Json.JsonSerializer.Serialize(pedidosEstado);
        }

        public class PecaStockBaixo
        {
            public int Id { get; set; }
            public string Nome { get; set; } = string.Empty;
            public int Stock { get; set; }
            public string Referencia { get; set; } = string.Empty;
        }

        public class CategoriaEstatistica
        {
            public string Nome { get; set; } = string.Empty;
            public int TotalPecas { get; set; }
        }

        public class PedidoRecente
        {
            public int Id { get; set; }
            public DateTime DataPedido { get; set; }
            public decimal Total { get; set; }
            public string Estado { get; set; } = string.Empty;
            public string NomeCliente { get; set; } = string.Empty;
        }

        public class PecaMaisVendida
        {
            public int PecaId { get; set; }
            public string Nome { get; set; } = string.Empty;
            public int QuantidadeVendida { get; set; }
            public decimal ReceitaTotal { get; set; }
        }
    }
}