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

namespace MotoPartsShop.Pages_Pecas
{
    
    [Authorize(Roles = "Admin,Gestor")]
    public class DeleteModel : PageModel
    {
        private readonly MotoPartsShop.Data.ApplicationDbContext _context;

        public DeleteModel(MotoPartsShop.Data.ApplicationDbContext context)
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

            var peca = await _context.Pecas.FirstOrDefaultAsync(m => m.Id == id);

            if (peca is not null)
            {
                Peca = peca;

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

            var peca = await _context.Pecas.FindAsync(id);
            if (peca != null)
            {
                Peca = peca;
                _context.Pecas.Remove(Peca);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
