using Microsoft.Extensions.Hosting.Internal;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure ocelot
var ocelotConfig = builder.Environment.IsDevelopment() ? "ocelot.development.json" : "ocelot.production.json";

//ocelotConfig = "ocelot.development.json";
builder.Configuration.AddJsonFile(ocelotConfig, optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseOcelot().Wait();

app.MapControllers();

app.Run();
