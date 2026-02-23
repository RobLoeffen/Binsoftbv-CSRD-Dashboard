using Binsoft.CRSDdashboard.Api.Data;
using Binsoft.CRSDdashboard.Api.Mappers;
using Binsoft.CRSDdashboard.Api.Repositories.Implementations;
using Binsoft.CRSDdashboard.Api.Repositories.Interfaces;
using Binsoft.CRSDdashboard.Api.Services.Implementations;
using Binsoft.CRSDdashboard.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Database Configuration
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Repositories (Data Access Layer) - Following SRP
builder.Services.AddScoped<IEcopartRepository, EcopartRepository>();
builder.Services.AddScoped<IEmissionFactorRepository, EmissionFactorRepository>();

// Register Services (Business Logic Layer) - Following SRP
builder.Services.AddScoped<IEcopartService, EcopartService>();
builder.Services.AddScoped<ICo2CalculationService, Co2CalculationService>();

// Register Mappers (Object Transformation) - Following SRP
builder.Services.AddScoped<IEcopartMapper, EcopartMapper>();

var app = builder.Build();

// Apply migrations automatically on startup (Development only)
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        dbContext.Database.Migrate();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
