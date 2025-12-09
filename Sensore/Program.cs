

using Microsoft.EntityFrameworkCore;
using Sensore.Data;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession(); // enables session management

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
app.UseSession(); //  activate sessions for all requests
app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
   pattern: "{controller=Loginpage}/{action=PatientClinicianLogin}/{id?}");


app.Run();
