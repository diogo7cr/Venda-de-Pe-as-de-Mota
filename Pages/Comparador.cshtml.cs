using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MotoPartsShop.Data;
using MotoPartsShop.Models;
using System.Text.Json;

namespace MotoPartsShop.Pages
{
    public class ComparadorModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ComparadorModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<ComparadorItem> Itens { get; set; } = new();

        public void OnGet()
        {
            CarregarComparador();
        }

        public async Task<IActionResult> OnPostAdicionarAsync(int pecaId)
        {
            CarregarComparador();

            // Máximo 4 peças
            if (Itens.Count >= 4)
            {
                TempData["Erro"] = "Só podes comparar até 4 peças!";
                return RedirectToPage();
            }

            // Verificar se já existe
            if (Itens.Any(i => i.PecaId == pecaId))
            {
                TempData["Aviso"] = "Esta peça já está no comparador!";
                return RedirectToPage();
            }

            // Buscar a peça
            var peca = await _context.Pecas
                .Include(p => p.Categoria)
                .Include(p => p.Modelo)
                    .ThenInclude(m => m!.Marca)
                .FirstOrDefaultAsync(p => p.Id == pecaId);

            if (peca == null)
            {
                return NotFound();
            }

            // Adicionar ao comparador
            Itens.Add(new ComparadorItem
            {
                PecaId = peca.Id,
                Nome = peca.Nome,
                Referencia = peca.Referencia,
                Preco = peca.Preco,
                Stock = peca.Stock,
                ImagemUrl = peca.ImagemUrl,
                CategoriaNome = peca.Categoria?.Nome,
                MarcaNome = peca.Modelo?.Marca?.Nome,
                ModeloNome = peca.Modelo?.Nome,
                PecaOriginal = peca.PecaOriginal,
                Peso = peca.Peso,
                Descricao = peca.Descricao
            });

            SalvarComparador();
            TempData["Sucesso"] = "Peça adicionada ao comparador!";
            return RedirectToPage();
        }

        public IActionResult OnPostRemover(int pecaId)
        {
            CarregarComparador();
            Itens.RemoveAll(i => i.PecaId == pecaId);
            SalvarComparador();
            return RedirectToPage();
        }

        public IActionResult OnPostLimpar()
        {
            HttpContext.Session.Remove("Comparador");
            return RedirectToPage();
        }

        private void CarregarComparador()
        {
            var comparadorJson = HttpContext.Session.GetString("Comparador");
            if (!string.IsNullOrEmpty(comparadorJson))
            {
                Itens = JsonSerializer.Deserialize<List<ComparadorItem>>(comparadorJson) ?? new();
            }
        }

        private void SalvarComparador()
        {
            var comparadorJson = JsonSerializer.Serialize(Itens);
            HttpContext.Session.SetString("Comparador", comparadorJson);
        }
    }
}