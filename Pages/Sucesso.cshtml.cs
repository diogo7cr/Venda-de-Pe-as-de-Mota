using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MotoPartsShop.Pages
{
    public class SucessoModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int? PedidoId { get; set; }

        public void OnGet()
        {
        }
    }
}