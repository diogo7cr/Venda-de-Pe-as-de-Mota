using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using MotoPartsShop.Data;
using MotoPartsShop.Models;

namespace MotoPartsShop.Pages_Pecas
{
    [Authorize(Roles = "Admin,Gestor")]
    public class EditModel : PageModel
    {
        private readonly MotoPartsShop.Data.ApplicationDbContext _context;

        public EditModel(MotoPartsShop.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Peca Peca { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var peca =  await _context.Pecas.FirstOrDefaultAsync(m => m.Id == id);
            if (peca == null)
            {
                return NotFound();
            }
            Peca = peca;
           ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nome");
           ViewData["ModeloId"] = new SelectList(_context.Modelos, "Id", "Nome");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Peca).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PecaExists(Peca.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PecaExists(int id)
        {
            return _context.Pecas.Any(e => e.Id == id);
        }
    }
}
