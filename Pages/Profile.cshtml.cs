using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MotoPartsShop.Data;
using MotoPartsShop.Models;

namespace MotoPartsShop.Pages
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IWebHostEnvironment _environment;

        public ProfileModel(
            ApplicationDbContext context, 
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IWebHostEnvironment environment)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _environment = environment;
        }

        [BindProperty]
        public InputModel Input { get; set; } = default!;

        [BindProperty]
        public IFormFile? FotoUpload { get; set; }

        public string Email { get; set; } = string.Empty;
        public string StatusMessage { get; set; } = string.Empty;
        public string? FotoPerfilUrl { get; set; }

        public class InputModel
        {
            public string Username { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string PrimeiroNome { get; set; } = string.Empty;
            public string UltimoNome { get; set; } = string.Empty;
            public DateTime? DataNascimento { get; set; }
            public string? Telefone { get; set; }
            public string? Morada { get; set; }
            public string? CodigoPostal { get; set; }
            public string? Cidade { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            Email = user.Email ?? string.Empty;

            var userProfile = await _context.UserProfiles
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.UserId == user.Id);

            Input = new InputModel
            {
                Username = user.UserName ?? string.Empty,
                Email = user.Email ?? string.Empty,
                PrimeiroNome = userProfile?.PrimeiroNome ?? string.Empty,
                UltimoNome = userProfile?.UltimoNome ?? string.Empty,
                DataNascimento = userProfile?.DataNascimento,
                Telefone = userProfile?.Telefone,
                Morada = userProfile?.Morada,
                CodigoPostal = userProfile?.CodigoPostal,
                Cidade = userProfile?.Cidade
            };

            FotoPerfilUrl = userProfile?.FotoPerfilUrl;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            // Validar campos obrigatórios
            if (string.IsNullOrWhiteSpace(Input.PrimeiroNome) || string.IsNullOrWhiteSpace(Input.UltimoNome))
            {
                StatusMessage = "❌ Primeiro e Último nome são obrigatórios!";
                Email = user.Email ?? string.Empty;
                await OnGetAsync();
                return Page();
            }

            bool needsReSignIn = false;

            try
            {
                // Atualizar Email
                if (!string.IsNullOrEmpty(Input.Email) && Input.Email != user.Email)
                {
                    var emailExists = await _userManager.FindByEmailAsync(Input.Email);
                    if (emailExists != null && emailExists.Id != user.Id)
                    {
                        StatusMessage = "❌ Este email já está em uso!";
                        Email = user.Email ?? string.Empty;
                        await OnGetAsync();
                        return Page();
                    }
                    
                    user.Email = Input.Email;
                    user.NormalizedEmail = Input.Email.ToUpper();
                    needsReSignIn = true;
                }

                // Atualizar Username
                if (!string.IsNullOrEmpty(Input.Username) && Input.Username != user.UserName)
                {
                    var usernameExists = await _userManager.FindByNameAsync(Input.Username);
                    if (usernameExists != null && usernameExists.Id != user.Id)
                    {
                        StatusMessage = "❌ Este username já está em uso!";
                        Email = user.Email ?? string.Empty;
                        await OnGetAsync();
                        return Page();
                    }
                    
                    user.UserName = Input.Username;
                    user.NormalizedUserName = Input.Username.ToUpper();
                    needsReSignIn = true;
                }

                // Atualizar utilizador
                if (needsReSignIn)
                {
                    var updateResult = await _userManager.UpdateAsync(user);
                    if (!updateResult.Succeeded)
                    {
                        StatusMessage = "❌ Erro ao atualizar dados de login: " + string.Join(", ", updateResult.Errors.Select(e => e.Description));
                        Email = user.Email ?? string.Empty;
                        await OnGetAsync();
                        return Page();
                    }
                }

                // Buscar ou criar perfil
                var userProfile = await _context.UserProfiles
                    .FirstOrDefaultAsync(p => p.UserId == user.Id);

                if (userProfile == null)
                {
                    userProfile = new UserProfile { UserId = user.Id };
                    _context.UserProfiles.Add(userProfile);
                }

                // Atualizar dados do perfil
                userProfile.PrimeiroNome = Input.PrimeiroNome.Trim();
                userProfile.UltimoNome = Input.UltimoNome.Trim();
                userProfile.DataNascimento = Input.DataNascimento;
                userProfile.Telefone = Input.Telefone?.Trim();
                userProfile.Morada = Input.Morada?.Trim();
                userProfile.CodigoPostal = Input.CodigoPostal?.Trim();
                userProfile.Cidade = Input.Cidade?.Trim();

                // Upload da foto
                if (FotoUpload != null && FotoUpload.Length > 0)
                {
                    // Validar tipo de ficheiro
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    var extension = Path.GetExtension(FotoUpload.FileName).ToLower();
                    
                    if (!allowedExtensions.Contains(extension))
                    {
                        StatusMessage = "❌ Apenas são aceites imagens JPG, PNG ou GIF!";
                        Email = user.Email ?? string.Empty;
                        await OnGetAsync();
                        return Page();
                    }

                    // Validar tamanho (5MB)
                    if (FotoUpload.Length > 5 * 1024 * 1024)
                    {
                        StatusMessage = "❌ A imagem não pode ter mais de 5MB!";
                        Email = user.Email ?? string.Empty;
                        await OnGetAsync();
                        return Page();
                    }

                    var fileName = $"{Guid.NewGuid()}{extension}";
                    var uploadsFolder = Path.Combine(_environment.WebRootPath, "images", "profiles");
                    
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }
                    
                    var filePath = Path.Combine(uploadsFolder, fileName);
                    
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await FotoUpload.CopyToAsync(stream);
                    }
                    
                    userProfile.FotoPerfilUrl = $"/images/profiles/{fileName}";
                }

                // Guardar alterações na base de dados
                await _context.SaveChangesAsync();

                // Limpar cache do Entity Framework
                _context.Entry(userProfile).State = EntityState.Detached;

                // Re-sign in para atualizar claims
                await _signInManager.RefreshSignInAsync(user);

                // ========== ADICIONA ESTAS 3 LINHAS AQUI ==========
                // Forçar reload sem cache (funciona com adblockers)
                Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
                Response.Headers["Pragma"] = "no-cache";
                Response.Headers["Expires"] = "0";
                // ===================================================

                StatusMessage = "✅ Perfil atualizado com sucesso!";
                
                // Redirect para evitar resubmit e forçar refresh
                return RedirectToPage();
            }
            catch (DbUpdateException dbEx)
            {
                StatusMessage = $"❌ Erro ao guardar na base de dados: {dbEx.InnerException?.Message ?? dbEx.Message}";
                Email = user.Email ?? string.Empty;
                await OnGetAsync();
                return Page();
            }
            catch (Exception ex)
            {
                StatusMessage = $"❌ Erro inesperado: {ex.Message}";
                Email = user.Email ?? string.Empty;
                await OnGetAsync();
                return Page();
            }
        }
    }
}