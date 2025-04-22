using DinkToPdf.Contracts;
using DinkToPdf;
using SystemQueueDigitalisation.Web.Services;
using SystemQueueDigitalisation.Web.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Shared BaseAddress
var baseAddress = new Uri("http://localhost:5107/");


// Configuration de la session
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});


// Register services with shared BaseAddress
AddHttpClientWithBaseAddress<ProviderService>(builder, baseAddress);
AddHttpClientWithBaseAddress<ClientService>(builder, baseAddress);
AddHttpClientWithBaseAddress<QueueService>(builder, baseAddress);
AddHttpClientWithBaseAddress<ServiceService>(builder, baseAddress);
AddHttpClientWithBaseAddress<AdminService>(builder, baseAddress);

builder.Services.AddScoped<ISessionService, SessionService>();

builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddHttpContextAccessor();

builder.Services.AddSignalR();
// Add session support
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


var app = builder.Build();

//mapping Notifications Hub
app.MapHub<NotificationHub>("/hubs/notifications");
app.MapHub<QueueHub>("/queueHub");


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