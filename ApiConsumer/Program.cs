
var builder = WebApplication.CreateBuilder(args);




// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<ApiClient>(); // API istemcisi ekleniyor
builder.Services.AddMemoryCache(); // Bellek i�i �nbellek ekleniyor
builder.Services.AddHttpContextAccessor();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // HSTS ayarlar�
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting(); // Do�ru s�rada middleware ekleniyor
app.UseAuthorization();

// Default route ayar�
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
