using Microsoft.EntityFrameworkCore;
using MovieRS.API.Extensions;
using MovieRS.API.Middlewares;
using MovieRS.API.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureCors();
builder.Services.AddDbContext<MovieRsContext>(options =>
{
    string? connectionString = builder.Environment.IsDevelopment()
    ? builder.Configuration.GetConnectionString("Testing")
    : builder.Configuration.GetConnectionString("Default");
    options.UseSqlServer(connectionString);
});
builder.Services.ConfigureAuthentication(builder.Configuration);
builder.Services.ConfigureExternalAPI();
builder.Services.AddSwaggerGen(options => options.ConfigureSwaggerOptions());
builder.Services.ConfigureRepository();
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())*/
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureExceptionHandler(app.Logger);

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<JwtMiddleware>();

app.Run();
