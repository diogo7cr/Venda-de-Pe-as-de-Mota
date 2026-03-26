using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using MotoPartsShop.Data;
using MotoPartsShop.Models;

namespace MotoPartsShop.Pages_Categorias
{[Authorize(Roles = "Admin,Gestor")]
    public class DeleteModel : PageModel
    {
        private readonly MotoPartsShop.Data.ApplicationDbContext _context;

        public DeleteModel(MotoPartsShop.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Categoria Categoria { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categorias.FirstOrDefaultAsync(m => m.Id == id);

            if (categoria is not null)
            {
                Categoria = categoria;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria != null)
            {
                Categoria = categoria;
                _context.Categorias.Remove(Categoria);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
