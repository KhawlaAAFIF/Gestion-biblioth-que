using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BiblioFrontOffice.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BiblioFrontOfficeContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BiblioFrontOfficeContext") ?? throw new InvalidOperationException("Connection string 'BiblioFrontOfficeContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

//configure session
builder.Services.AddSession();
builder.Services.AddDistributedMemoryCache();

var app = builder.Build();

//enable session
app.UseSession();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Documents}/{action=Index}/{id?}");

app.Run();
