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
    public class IndexModel : PageModel
    {
        private readonly MotoPartsShop.Data.ApplicationDbContext _context;

        public IndexModel(MotoPartsShop.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Marca> Marca { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Marca = await _context.Marcas.ToListAsync();
        }
    }
}
