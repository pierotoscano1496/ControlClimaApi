using ControlClimaApi.Models;
using ControlClimaApi.Models.DBContext;
using ControlClimaApi.Models.DBContext.Interfaces;

//string corsAllowSpecificOriginsName = "AllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IControlClimaContext, ControlClimaContext>();
builder.Services.AddSingleton<IUsuarioContext, UsuarioContext>();
builder.Services.AddSingleton<IClimaContext, ClimaContext>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*
builder.Services.AddCors(options =>
{
    options.AddPolicy(corsAllowSpecificOriginsName, builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseCors(corsAllowSpecificOriginsName);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
