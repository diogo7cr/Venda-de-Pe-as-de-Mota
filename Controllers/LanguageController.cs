using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace MotoPartsShop.Controllers
{
    [Route("[controller]/[action]")]
    public class LanguageController : Controller
    {
        public IActionResult Change(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions 
                { 
                    Expires = DateTimeOffset.UtcNow.AddYears(1),
                    IsEssential = true,
                    Path = "/"
                }
            );

            return LocalRedirect(returnUrl ?? "/");
        }
    }
}