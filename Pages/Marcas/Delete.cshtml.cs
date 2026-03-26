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

namespace MotoPartsShop.Pages_Marcas
{[Authorize(Roles = "Admin,Gestor")]
    public class DeleteModel : PageModel
    {
        private readonly MotoPartsShop.Data.ApplicationDbContext _context;

        public DeleteModel(MotoPartsShop.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Marca Marca { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marca = await _context.Marcas.FirstOrDefaultAsync(m => m.Id == id);

            if (marca is not null)
            {
                Marca = marca;

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

            var marca = await _context.Marcas.FindAsync(id);
            if (marca != null)
            {
                Marca = marca;
                _context.Marcas.Remove(Marca);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
