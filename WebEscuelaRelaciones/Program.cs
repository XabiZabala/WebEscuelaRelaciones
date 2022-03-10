using Microsoft.EntityFrameworkCore;
using WebEscuelaRelaciones.Data;
//using WebEscuelaRelaciones.Data.DataSeeder;

var builder = WebApplication.CreateBuilder(args);

//creamos un servicio para añadir el contexto de la aplicación
builder.Services.AddDbContext<AcademiaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AcademiaContext")));

// Add services to the container.
builder.Services.AddControllersWithViews();

//Filtro de excepción para comprobar si hay errores al crear la BD
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

//Poblar de datos seeder
//Llamamos al método Inicialize de la clase DbInitializer

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    DbInitializer.Initialize(services);
}


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
