using Kursovaia;

var builder = WebApplication.CreateBuilder(args);

var Configuration = builder.Configuration;
var services = builder.Services;

string connection = Configuration.GetConnectionString("SqlServerConnection");

services.AddDbContext<ProjectsContext>(options => options.UseSqlServer(connection));

builder.Services.AddControllersWithViews();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
