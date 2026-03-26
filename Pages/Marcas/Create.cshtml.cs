using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using MotoPartsShop.Data;
using MotoPartsShop.Models;

namespace MotoPartsShop.Pages_Marcas
{[Authorize(Roles = "Admin,Gestor")]
    public class CreateModel : PageModel
    {
        private readonly MotoPartsShop.Data.ApplicationDbContext _context;

        public CreateModel(MotoPartsShop.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Marca Marca { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Marcas.Add(Marca);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
