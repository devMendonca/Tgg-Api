using Tgg.Services;
using Tgg.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient("dynamic", x =>
{
    x.BaseAddress = new Uri(builder.Configuration["ServiceApi:url"]);
});

builder.Services.AddHttpClient("Autentica", x =>
{
    x.BaseAddress = new Uri(builder.Configuration["ServiceApi:url"]);
    x.DefaultRequestHeaders.Accept.Clear();
    x.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
});

builder.Services.AddScoped<IPedidos, PedidoService>();
builder.Services.AddScoped<IProdutos, ProdutoService>();
builder.Services.AddScoped<IAuth, Auth>();

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
