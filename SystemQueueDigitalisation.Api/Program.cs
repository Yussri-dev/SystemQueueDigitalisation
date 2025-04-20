using Microsoft.EntityFrameworkCore;
using SystemQueueDigitalisation.Api.Hubs;
using SystemQueueDigitalisation.Application.Interfaces;
using SystemQueueDigitalisation.Application.Interfaces.Services;
using SystemQueueDigitalisation.Application.Services;
using SystemQueueDigitalisation.Infrastructure.Data;
using SystemQueueDigitalisation.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

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

//SignalR
//builder.Services.AddSignalR();

var app = builder.Build();

//app.MapHub<QueueHub>("/queueHub");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
