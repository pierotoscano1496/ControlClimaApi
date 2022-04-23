using ControlClimaApi.Infraestructure.Persistance;

string originsAllowed = "originsAllowed";

var builder = WebApplication.CreateBuilder(args);

// Infraestructure services
builder.Services.AddPersistanceInfraestructure();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// HTTP client
builder.Services.AddHttpClient("SensorServer", (client) =>
{
    client.BaseAddress = new Uri("http://192.168.1.22/");
});

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(originsAllowed, builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(originsAllowed);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
