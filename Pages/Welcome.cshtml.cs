using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MotoPartsShop.Data;
using MotoPartsShop.Models;

namespace MotoPartsShop.Pages
{
    [Authorize]
    public class WelcomeModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public WelcomeModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string PrimeiroNome, string UltimoNome, DateTime? DataNascimento)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToPage("/Index");

            var profile = new UserProfile
            {
                UserId = user.Id,
                PrimeiroNome = PrimeiroNome,
                UltimoNome = UltimoNome,
                DataNascimento = DataNascimento
            };

            _context.UserProfiles.Add(profile);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}