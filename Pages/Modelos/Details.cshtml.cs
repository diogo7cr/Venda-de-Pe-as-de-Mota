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
{[Authorize(Roles = "Admin,Gestor")]
    public class DetailsModel : PageModel
    {
        private readonly MotoPartsShop.Data.ApplicationDbContext _context;

        public DetailsModel(MotoPartsShop.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
