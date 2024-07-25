using Microsoft.EntityFrameworkCore;
using MVC.NetCore8.WebSite.Data.DbContextService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// When we want to use Database, uncomment the below line
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//{
//    var connectionString = builder.Configuration.GetConnectionString("DbConnection");
//    options.UseSqlServer(connectionString);
//});

// When we want to use In-Memory, uncomment the below line
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseInMemoryDatabase("MvcCrud_InMemoryDb");
});

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
