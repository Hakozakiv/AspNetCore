using Microsoft.EntityFrameworkCore;
using AspNetCore.Data; // Ajustado de SeuProjeto para AspNetCore

var builder = WebApplication.CreateBuilder(args);

// 1. Configura o Banco de Dados
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. ADICIONE ISSO: Configura os serviços do MVC (Controllers com Views)
builder.Services.AddControllersWithViews();

var app = builder.Build();

// 3. ADICIONE ISSO: Configura o pipeline de navegação
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Essencial para carregar CSS e JS (Bootstrap)

app.UseRouting();

app.UseAuthorization(); // Essencial para a tela de login funcionar depois

// 4. ADICIONE ISSO: Define a rota padrão (onde o sistema começa)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();