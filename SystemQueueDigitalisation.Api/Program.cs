using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SystemQueueDigitalisation.Api.Hubs;
using SystemQueueDigitalisation.Application.Interfaces;
using SystemQueueDigitalisation.Application.Interfaces.Services;
using SystemQueueDigitalisation.Application.Services;
using SystemQueueDigitalisation.Infrastructure.Data;
using SystemQueueDigitalisation.Infrastructure.Identity;
using SystemQueueDigitalisation.Infrastructure.Repositories;
using SystemQueueDigitalisation.Web.Requests;

var builder = WebApplication.CreateBuilder(args);

// Explicitly add configuration files
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true);
builder.Configuration.AddEnvironmentVariables();

// Debug: Print configuration path and if file exists
var appSettingsPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
Console.WriteLine($"Looking for appsettings.json at: {appSettingsPath}");
Console.WriteLine($"File exists: {File.Exists(appSettingsPath)}");
Console.WriteLine($"Current environment: {builder.Environment.EnvironmentName}");

//Adding Cors
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowOrigins", builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddRazorPages();

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuration de Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // Options de configuration...
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Check if JWT configuration exists
Console.WriteLine($"JWT Key: {builder.Configuration["Jwt:Key"]}");
Console.WriteLine($"JWT Issuer: {builder.Configuration["Jwt:Issuer"]}");
Console.WriteLine($"JWT Audience: {builder.Configuration["Jwt:Audience"]}");

try
{
    // Configuration JWT using safe approach with hardcoded fallback
    var jwtKey = builder.Configuration["Jwt:Key"] ?? "DefaultSecurityKeyWith32Characters!!!";
    var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "DefaultIssuer";
    var jwtAudience = builder.Configuration["Jwt:Audience"] ?? "DefaultAudience";

    Console.WriteLine("Using JWT Configuration:");
    Console.WriteLine($"Key: {(string.IsNullOrEmpty(builder.Configuration["Jwt:Key"]) ? "Using default fallback key" : "Using configured key")}");
    Console.WriteLine($"Issuer: {jwtIssuer}");
    Console.WriteLine($"Audience: {jwtAudience}");

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });
}
catch (Exception ex)
{
    Console.WriteLine($"Error configuring JWT: {ex.Message}");
    throw;
}

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

// Injection des dépendances
builder.Services.AddScoped<ITokenService, JwtTokenService>();
builder.Services.AddScoped<IIdentityService, IdentityService>();

// Register Repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(RepositoryBase<>));
builder.Services.AddScoped<IProviderRepository, ProviderRepository>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IQueueRepository, QueueRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();

// Register Services
builder.Services.AddScoped<IProviderService, ProviderService>();
builder.Services.AddScoped<IQueueService, QueueService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IServicesService, ServiceService>();
builder.Services.AddScoped<IAdminService, AdminService>();

//SignalR
//builder.Services.AddSignalR();

var app = builder.Build();

app.UseCors("AllowOrigins");
//app.MapHub<QueueHub>("/queueHub");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Make sure Authentication is before Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();