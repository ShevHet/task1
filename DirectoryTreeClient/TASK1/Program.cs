using Microsoft.EntityFrameworkCore;
using TASK1;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("DirectoryTree"));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
