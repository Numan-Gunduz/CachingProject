
var builder = WebApplication.CreateBuilder(args);




// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<ApiClient>(); // API istemcisi ekleniyor
builder.Services.AddMemoryCache(); // Bellek içi önbellek ekleniyor
builder.Services.AddHttpContextAccessor();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // HSTS ayarlarý
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting(); // Doðru sýrada middleware ekleniyor
app.UseAuthorization();

// Default route ayarý
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
