using Microsoft.EntityFrameworkCore;
using NoBeard.Learn.AspNet.WebApp.Data;
using NoBeard.Learn.AspNet.WebApp.Data.Entities;
using NoBeard.Learn.AspNet.WebApp.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IBaseRepository<Movie>, BaseRepository<Movie>>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//var scope = app.Services.CreateScope();
//var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
//dbContext.Database.Migrate();

app.Run();
