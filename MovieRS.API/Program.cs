using Microsoft.EntityFrameworkCore;
using MovieRS.API.Error;
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
builder.Services.AddDbContext<RsContext>(options =>
{
    string? connectionString = string.Format(builder.Configuration.GetConnectionString("RS"), new FileInfo(Path.Combine(Environment.CurrentDirectory, "wwwroot")).FullName);
    options.UseSqlite(connectionString);
});
builder.Services.ConfigureAuthentication(builder.Configuration);
builder.Services.ConfigureExternalAPI();
builder.Services.AddSwaggerGen(options => options.ConfigureSwaggerOptions());
builder.Services.ConfigureRepository();
builder.Services.ConfigureMail(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<MovieRsContext>();
    try
    {
        await context.Database.MigrateAsync();
        if (!await context.Countries.AnyAsync())
        {
            await context.Countries.OnModelCreatingPartial();
            await context.SaveChangesAsync();
        }
    }
    catch (Exception ex)
    {

    }
}

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
