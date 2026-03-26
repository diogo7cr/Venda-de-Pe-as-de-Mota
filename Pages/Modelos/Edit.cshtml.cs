using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MotoPartsShop.Data;
using MotoPartsShop.Models;

namespace MotoPartsShop.Pages_Modelos
{[Authorize(Roles = "Admin,Gestor")]
    public class EditModel : PageModel
    {
        private readonly MotoPartsShop.Data.ApplicationDbContext _context;

        public EditModel(MotoPartsShop.Data.ApplicationDbContext context)
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

            var modelo =  await _context.Modelos.FirstOrDefaultAsync(m => m.Id == id);
            if (modelo == null)
            {
                return NotFound();
            }
            Modelo = modelo;
           ViewData["MarcaId"] = new SelectList(_context.Marcas, "Id", "Nome");
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

            _context.Attach(Modelo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModeloExists(Modelo.Id))
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

        private bool ModeloExists(int id)
        {
            return _context.Modelos.Any(e => e.Id == id);
        }
    }
}
