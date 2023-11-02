using VetCare.API.Appointment.Domain.Repositories;
using VetCare.API.Appointment.Domain.Services;
using VetCare.API.Appointment.Mapping;
using VetCare.API.Appointment.Persistence.Repositories;
using VetCare.API.Appointment.Services;
using VetCare.API.Store.Domain.Repositories;
using VetCare.API.Store.Domain.Services;
using VetCare.API.Store.Mapping;
using VetCare.API.Store.Persistence.Repositories;
using VetCare.API.Store.Services;
using VetCare.API.Shared.Persistence.Contexts;
using VetCare.API.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using VetCare.API.Identification.Domain.Repositories;
using VetCare.API.Identification.Domain.Services;
using VetCare.API.Identification.Persistence.Repositories;
using VetCare.API.Identification.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Database Connection

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseMySQL(connectionString)
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors());

// Add lowercase routes

builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Dependency Injection Configuration

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUnitOfWorkS, UnitOfWorkS>();
builder.Services.AddScoped<IPetRepository, PetRepository>();
builder.Services.AddScoped<IPetService, PetService>();
builder.Services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
builder.Services.AddScoped<IPrescriptionService, PrescriptionService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();


// AutoMapper Configuration

builder.Services.AddAutoMapper(
    typeof(ModelToResourceProduct), 
    typeof(ResourceToModelProduct), 
    typeof(ModelToResourceProfile), 
    typeof(ResourceToModelProfile),
    typeof(VetCare.API.Identification.Mapping.ModelToResourceProfile),
    typeof(VetCare.API.Identification.Mapping.ResourceToModelProfile));
    

var app = builder.Build();

// Validation for ensuring Database Objects are created

using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<AppDbContext>())
{
    context.Database.EnsureCreated();
}

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