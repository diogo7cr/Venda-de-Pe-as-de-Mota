using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MotoPartsShop.Data;
using MotoPartsShop.Models;
using X.PagedList;
using X.PagedList.Extensions; // ADICIONA ESTA LINHA

namespace MotoPartsShop.Pages_Pecas
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IPagedList<Peca> Peca { get; set; } = default!;
        
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public int? CategoriaId { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public int? MarcaId { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public int? ModeloId { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public bool? ApenasEmStock { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public decimal? PrecoMin { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public decimal? PrecoMax { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public int? PageNumber { get; set; } // MUDEI DE Page PARA PageNumber

        public SelectList? Categorias { get; set; }
        public SelectList? Marcas { get; set; }
        public SelectList? Modelos { get; set; }
        
        public HashSet<int> FavoritosIds { get; set; } = new();
        
        public int PageSize { get; set; } = 12; // Peças por página

        public async Task OnGetAsync()
        {
            // Carregar dados para os dropdowns
            Categorias = new SelectList(await _context.Categorias.ToListAsync(), "Id", "Nome");
            Marcas = new SelectList(await _context.Marcas.ToListAsync(), "Id", "Nome");
            Modelos = new SelectList(await _context.Modelos.Include(m => m!.Marca).ToListAsync(), "Id", "Nome");

            var pecasQuery = _context.Pecas
                .Include(p => p.Categoria)
                .Include(p => p.Modelo)
                    .ThenInclude(m => m!.Marca)
                .AsQueryable();

            // Aplicar filtros
            if (!string.IsNullOrEmpty(SearchString))
            {
                pecasQuery = pecasQuery.Where(p => 
                    p.Nome.Contains(SearchString) || 
                    p.Referencia.Contains(SearchString) ||
                    (p.Descricao != null && p.Descricao.Contains(SearchString)));
            }

            if (CategoriaId.HasValue)
            {
                pecasQuery = pecasQuery.Where(p => p.CategoriaId == CategoriaId);
            }

            if (MarcaId.HasValue)
            {
                pecasQuery = pecasQuery.Where(p => p.Modelo!.MarcaId == MarcaId);
            }

            if (ModeloId.HasValue)
            {
                pecasQuery = pecasQuery.Where(p => p.ModeloId == ModeloId);
            }

            if (ApenasEmStock == true)
            {
                pecasQuery = pecasQuery.Where(p => p.Stock > 0);
            }

            if (PrecoMin.HasValue)
            {
                pecasQuery = pecasQuery.Where(p => p.Preco >= PrecoMin);
            }

            if (PrecoMax.HasValue)
            {
                pecasQuery = pecasQuery.Where(p => p.Preco <= PrecoMax);
            }

            // Aplicar paginação
            int pageNumber = PageNumber ?? 1; // MUDEI AQUI
            var pecasList = await pecasQuery.ToListAsync();
            Peca = pecasList.ToPagedList(pageNumber, PageSize);
            
            // Carregar favoritos do utilizador atual
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                FavoritosIds = await _context.Favoritos
                    .Where(f => f.UserId == user.Id)
                    .Select(f => f.PecaId)
                    .ToHashSetAsync();
            }
        }
        
        public async Task<IActionResult> OnPostToggleFavoritoAsync(int pecaId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            var favoritoExistente = await _context.Favoritos
                .FirstOrDefaultAsync(f => f.UserId == user.Id && f.PecaId == pecaId);

            if (favoritoExistente != null)
            {
                _context.Favoritos.Remove(favoritoExistente);
            }
            else
            {
                var favorito = new Favorito
                {
                    UserId = user.Id,
                    PecaId = pecaId,
                    DataAdicao = DateTime.Now
                };
                _context.Favoritos.Add(favorito);
            }

            await _context.SaveChangesAsync();
            
            return RedirectToPage(new 
            { 
                SearchString, 
                CategoriaId, 
                MarcaId, 
                ModeloId, 
                ApenasEmStock, 
                PrecoMin, 
                PrecoMax,
                PageNumber // MUDEI AQUI TAMBÉM
            });
        }
    }
}