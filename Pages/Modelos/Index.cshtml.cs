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
    public class IndexModel : PageModel
    {
        private readonly MotoPartsShop.Data.ApplicationDbContext _context;

        public IndexModel(MotoPartsShop.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Modelo> Modelo { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Modelo = await _context.Modelos
                .Include(m => m.Marca).ToListAsync();
        }
    }
}
