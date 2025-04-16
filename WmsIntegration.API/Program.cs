using Microsoft.EntityFrameworkCore;
using WmsIntegration.Infrastructure.Data;
using WmsIntegration.Core.Entities; 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("GACWMS-Database"));


var app = builder.Build();
// Ensure that the database is created and apply seed data
using (var scope = app.Services.CreateScope()){
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    // Create the database (if it doesn't exist)
    context.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
