using Microsoft.EntityFrameworkCore;
using MovieRS.Video.Extensions;
using MovieRS.Video.Models;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseKestrel(options =>
{
    options.ListenAnyIP(builder.Configuration.GetValue<int>("StaticPort"));
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DatabaseContext>(options =>
{
    string? connectionString = builder.Configuration.GetConnectionString("Default");
    options.UseSqlite(connectionString);
});

builder.Services.ConfigureCors();
builder.Services.ConfigureRepository();
builder.Services.ConfigureTunnel();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.ConfigureExceptionHandler(app.Logger);

app.UseAuthorization();

app.MapControllers();

IUpdateDomain? updateApi = app.Services.GetService(typeof(IUpdateDomain)) as IUpdateDomain;
if (updateApi != null)
{
    string? email, password;
    do
    {
        Console.WriteLine("---Access---");
        Console.WriteLine("Email:");
        email = Console.ReadLine();
        Console.WriteLine("Password:");
        password = Console.ReadLine();
    }
    while (string.IsNullOrEmpty(email)
    || string.IsNullOrEmpty(password)
    || !await updateApi.Access(email, password));
}

app.Run();
