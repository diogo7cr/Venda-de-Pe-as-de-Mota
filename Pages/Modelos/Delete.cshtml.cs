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

namespace MotoPartsShop.Pages_Modelos
{
[Authorize(Roles = "Admin,Gestor")]    public class DeleteModel : PageModel
    {
        private readonly MotoPartsShop.Data.ApplicationDbContext _context;

        public DeleteModel(MotoPartsShop.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Modelo Modelo { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modelo = await _context.Modelos.FirstOrDefaultAsync(m => m.Id == id);

            if (modelo is not null)
            {
                Modelo = modelo;

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

            var modelo = await _context.Modelos.FindAsync(id);
            if (modelo != null)
            {
                Modelo = modelo;
                _context.Modelos.Remove(Modelo);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
