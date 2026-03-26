using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MotoPartsShop.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class UsersModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersModel(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public List<UserViewModel> Users { get; set; } = new();
        public int TotalUsers { get; set; }
        public int TotalAdmins { get; set; }
        public int TotalGestores { get; set; }
        public int TotalClientes { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? FiltroRole { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Pesquisa { get; set; }

        public async Task OnGetAsync()
        {
            var allUsers = await _userManager.Users.ToListAsync();
            var userViewModels = new List<UserViewModel>();

            foreach (var user in allUsers)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var userVm = new UserViewModel
                {
                    Id = user.Id,
                    Email = user.Email ?? "",
                    UserName = user.UserName ?? "",
                    EmailConfirmed = user.EmailConfirmed,
                    Roles = roles.ToList()
                };

                userViewModels.Add(userVm);
            }

            // Filtro por role
            if (!string.IsNullOrEmpty(FiltroRole) && FiltroRole != "Todos")
            {
                userViewModels = userViewModels.Where(u => u.Roles.Contains(FiltroRole)).ToList();
            }

            // Pesquisa
            if (!string.IsNullOrEmpty(Pesquisa))
            {
                userViewModels = userViewModels.Where(u => 
                    u.Email.Contains(Pesquisa, StringComparison.OrdinalIgnoreCase) ||
                    u.UserName.Contains(Pesquisa, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            Users = userViewModels.OrderBy(u => u.Email).ToList();

            // Estatísticas
            TotalUsers = Users.Count;
            TotalAdmins = Users.Count(u => u.Roles.Contains("Admin"));
            TotalGestores = Users.Count(u => u.Roles.Contains("Gestor"));
            TotalClientes = Users.Count(u => u.Roles.Contains("Cliente") || u.Roles.Count == 0);
        }

        public async Task<IActionResult> OnPostAlterarRoleAsync(string userId, string novaRole)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            // Remove todas as roles atuais
            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);

            // Adiciona a nova role
            if (!string.IsNullOrEmpty(novaRole))
            {
                await _userManager.AddToRoleAsync(user, novaRole);
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEliminarAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            // Não permitir eliminar o próprio utilizador
            if (user.Id == _userManager.GetUserId(User))
            {
                TempData["Erro"] = "Não pode eliminar a sua própria conta!";
                return RedirectToPage();
            }

            await _userManager.DeleteAsync(user);
            TempData["Sucesso"] = "Utilizador eliminado com sucesso!";

            return RedirectToPage();
        }

        public class UserViewModel
        {
            public string Id { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string UserName { get; set; } = string.Empty;
            public bool EmailConfirmed { get; set; }
            public List<string> Roles { get; set; } = new();
        }
    }
}