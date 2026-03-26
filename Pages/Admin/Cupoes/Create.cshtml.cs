using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MotoPartsShop.Data;
using MotoPartsShop.Models;

namespace MotoPartsShop.Pages.Admin.Cupoes
{
    [Authorize(Roles = "Admin,Gestor")]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            // Valores padrão
            Cupao = new Cupao
            {
                DataInicio = DateTime.Now.Date,
                DataFim = DateTime.Now.Date.AddDays(30),
                ValorMinimo = 50,
                PercentagemDesconto = 10,
                Ativo = true
            };
            return Page();
        }

        [BindProperty]
        public Cupao Cupao { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Validações adicionais
            if (Cupao.DataFim < Cupao.DataInicio)
            {
                ModelState.AddModelError("Cupao.DataFim", "A data de fim deve ser posterior à data de início.");
                return Page();
            }

            // Verificar se o código já existe
            var codigoExiste = await _context.Cupoes
                .AnyAsync(c => c.Codigo.ToUpper() == Cupao.Codigo.ToUpper());

            if (codigoExiste)
            {
                ModelState.AddModelError("Cupao.Codigo", "Este código de cupão já existe.");
                return Page();
            }

            // Converter código para maiúsculas
            Cupao.Codigo = Cupao.Codigo.ToUpper();

            _context.Cupoes.Add(Cupao);
            await _context.SaveChangesAsync();

            TempData["Sucesso"] = $"Cupão '{Cupao.Codigo}' criado com sucesso!";
            return RedirectToPage("./Index");
        }
    }
}