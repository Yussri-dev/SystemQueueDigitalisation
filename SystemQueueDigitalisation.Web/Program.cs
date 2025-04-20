using DinkToPdf.Contracts;
using DinkToPdf;
using SystemQueueDigitalisation.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Shared BaseAddress
var baseAddress = new Uri("http://localhost:5107/");

// Register services with shared BaseAddress
AddHttpClientWithBaseAddress<ProviderService>(builder, baseAddress);
AddHttpClientWithBaseAddress<ClientService>(builder, baseAddress);
AddHttpClientWithBaseAddress<QueueService>(builder, baseAddress);
AddHttpClientWithBaseAddress<ServiceService>(builder, baseAddress);

builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

// Add services to the container.
builder.Services.AddRazorPages();

// Add session support
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapRazorPages();

app.Run();

void AddHttpClientWithBaseAddress<TService>(WebApplicationBuilder builder, Uri baseAddress)
    where TService : class
{
    builder.Services.AddHttpClient<TService>(client =>
    {
        client.BaseAddress = baseAddress;
    });
}