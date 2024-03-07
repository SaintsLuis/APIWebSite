using Microsoft.EntityFrameworkCore;
using UNAD.DAL;
using WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Configuración de la cadena de conexión
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? String.Empty;

// Configuración de los servicios
builder.Services.AddDbContext<Context>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21))));


// Services
builder.Services.AddScoped<CategoriasServices>();
builder.Services.AddScoped<ClientesServices>();
builder.Services.AddScoped<DetalleFacturaService>();
builder.Services.AddScoped<FacturasService>();
builder.Services.AddScoped<ProductosService>();
builder.Services.AddScoped<TipoFacturaService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
