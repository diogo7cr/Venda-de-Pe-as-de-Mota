using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using MotoPartsShop.Data;
using MotoPartsShop.Models;

namespace MotoPartsShop.Pages_Pecas
{
    [Authorize(Roles = "Admin,Gestor")]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public CreateModel(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [BindProperty]
        public Peca Peca { get; set; } = default!;
        
        [BindProperty]
        public IFormFile? ImagemUpload { get; set; }

        public IActionResult OnGet()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nome");
            ViewData["ModeloId"] = new SelectList(_context.Modelos, "Id", "Nome");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nome");
                ViewData["ModeloId"] = new SelectList(_context.Modelos, "Id", "Nome");
                return Page();
            }

            // Upload da imagem
            if (ImagemUpload != null && ImagemUpload.Length > 0)
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(ImagemUpload.FileName)}";
                var filePath = Path.Combine(_environment.WebRootPath, "images", "pecas", fileName);
                
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImagemUpload.CopyToAsync(stream);
                }
                
                Peca.ImagemUrl = $"/images/pecas/{fileName}";
            }

            _context.Pecas.Add(Peca);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}