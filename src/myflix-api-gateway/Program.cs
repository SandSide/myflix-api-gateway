using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "http://localhost:5002",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("x+RxEw(WF({0&y6$2zxvewrZ8{vfycB%._)!W+k_D8*n#d4(UpryY%T&CL3JDi)xJw)*iE6i?gb[.EnE9_F6RmCX=S@?xG]?rna%qAWH+R(TC?#!u97Zi/78bCBFA-_V")) // Specify your JWT secret key
    };
});

// Configure ocelot
var ocelotConfig = builder.Environment.IsDevelopment() ? "ocelot.development.json" : "ocelot.production.json";

//ocelotConfig = "ocelot.development.json";
builder.Configuration.AddJsonFile(ocelotConfig, optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

app.UseAuthentication();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseOcelot().Wait();

app.MapControllers();

app.Run();
