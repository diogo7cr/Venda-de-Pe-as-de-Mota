using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MotoPartsShop.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();
// CONFIGURAÇÃO DE LOCALIZAÇÃO (MULTILINGUE)
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { "pt", "en", "es", "fr" };
    options.SetDefaultCulture("pt")
        .AddSupportedCultures(supportedCultures)
        .AddSupportedUICultures(supportedCultures);
});
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();
// USAR LOCALIZAÇÃO
app.UseRequestLocalization();

app.UseSession();   

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();
app.MapControllers(); // Para o LanguageController funcionar
// ========== ADICIONA ESTAS LINHAS AQUI ==========
// Adicionar dados iniciais à BD
// Adicionar dados iniciais à BD e criar roles
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    
    // Inicializar dados
    DbInitializer.Initialize(context);
    
    // Criar roles
    await RoleSeeder.SeedRolesAsync(services);
}
// ================================================
// ========================================
// ADICIONAR ISTO NO Program.cs
// Logo ANTES de app.Run();
// ========================================

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    // Criar Roles se não existirem
    string[] roles = { "Admin", "Gestor", "Cliente" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    // Criar utilizador Admin padrão (se não existir)
    string adminEmail = "admin@motoparts.com";
    string adminPassword = "Admin@123";

    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        var newAdmin = new IdentityUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(newAdmin, adminPassword);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(newAdmin, "Admin");
            Console.WriteLine("✅ Admin criado: admin@motoparts.com / Admin@123");
        }
    }
}
// Inicializa a Base de Dados com dados de exemplo
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    DbInitializer.Initialize(context);
}

app.Run();
