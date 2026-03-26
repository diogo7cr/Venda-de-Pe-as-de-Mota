using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotoPartsShop.Data;

namespace MotoPartsShop.ViewComponents
{
    public class UserDisplayNameViewComponent : ViewComponent
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public UserDisplayNameViewComponent(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (User?.Identity?.IsAuthenticated == true)
            {
                var user = await _userManager.GetUserAsync((System.Security.Claims.ClaimsPrincipal)User);
                if (user != null)
                {
                    var profile = await _context.UserProfiles
                        .AsNoTracking()
                        .FirstOrDefaultAsync(p => p.UserId == user.Id);

                    var displayName = profile != null && !string.IsNullOrEmpty(profile.PrimeiroNome)
                        ? $"{profile.PrimeiroNome} {profile.UltimoNome}"
                        : user.Email?.Split('@')[0] ?? "User";

                    var photoUrl = profile?.FotoPerfilUrl;

                    return View((displayName, photoUrl));
                }
            }

            return Content(string.Empty);
        }
    }
}