using CampusTech.Reservas.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Registrar repositorio como singleton para mantener datos en memoria mientras la app esté viva
builder.Services.AddSingleton<IReservaRepository, InMemoryReservaRepository>();

var app = builder.Build();
// ... configuración normal: UseStaticFiles, Routing, etc.

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Reservas}/{action=Index}/{id?}");

app.Run();
