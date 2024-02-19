using Dynacoin.Coinlore.Sdk;
using Dynacoin.Domain.Services;
using Dynacoin.Services;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    // TODO - extract the file path to a config file
    .WriteTo.File("c:/logs/dynacoin.log")
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

// Add services to the container.
// TODO - extract dependency configurations to a separate class
builder.Services.AddTransient<ICoinInfoService, CoinloreInfoService>();
builder.Services.AddTransient<ICoinloreClient, CoinloreClient>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
