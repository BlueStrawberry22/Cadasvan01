using Cadasvan01.Data;
using Cadasvan01.Models;
using Cadasvan01.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();



//antigo
//builder.Services.AddDefaultIdentity<Usuario>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>();
//Novo
builder.Services.AddIdentity<Usuario, Funcao>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();



builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin",
        politica =>
        {
            politica.RequireRole("Admin");
        });
    options.AddPolicy("Aluno",
        politica =>
        {
            politica.RequireRole("Aluno");
        });
    options.AddPolicy("Motorista",
        politica =>
        {
            politica.RequireRole("Motorista");
        });
});

var app = builder.Build();

using (var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;

    var servicoSeed = services.GetRequiredService<ISeedUserRoleInitial>();

    servicoSeed.SeedRoles();
    servicoSeed.SeedUsers();
}


if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();



app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Motorista}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


//app.MapRazorPages();




app.Run();
/*     void CriarPerfisUsuarios(WebApplication app)
     {
         var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
         using (var scope = scopedFactory.CreateScope())
         {
             var service = scope.ServiceProvider.GetService<ISeedUserRoleInitial>();
             service.SeedUsers();
             service.SeedRoles();
         }
     }*/
