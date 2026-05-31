using Hydra.GatewayApi.Models;
using Hydra.GatewayApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontendApps", policy =>
    {
        policy.WithOrigins(
                "http://localhost:4200",
                "https://localhost:7200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var jwtKey = "HYDRA-SUPER-SECRET-KEY-FOR-DEMO-ONLY";
var keyBytes = Encoding.UTF8.GetBytes(jwtKey);

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(keyBytes)
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddScoped<IIntelligenceService,
                            IntelligenceService>();



builder.Services.AddHttpClient("ThreatApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7101");

    client.DefaultRequestHeaders.Add(
        "X-API-KEY",
        "HYDRA-SECRET-KEY");
});

builder.Services.AddHttpClient("OperationsApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7102");

    client.DefaultRequestHeaders.Add(
        "X-API-KEY",
        "HYDRA-SECRET-KEY");
});


var app = builder.Build();


app.UseCors("AllowFrontendApps");

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "Hydra Gateway API Running");

app.MapPost("/api/auth/login", (LoginRequest request) =>
{
    if (request.Username != "admin" || request.Password != "password")
    {
        return Results.Unauthorized();
    }

    var claims = new[]
    {
        new Claim(ClaimTypes.Name, request.Username),
        new Claim(ClaimTypes.Role, "Admin")
    };

    var key = new SymmetricSecurityKey(keyBytes);
    var credentials = new SigningCredentials(
        key,
        SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
        claims: claims,
        expires: DateTime.UtcNow.AddHours(1),
        signingCredentials: credentials);

    var tokenText = new JwtSecurityTokenHandler()
        .WriteToken(token);

    return Results.Ok(new
    {
        Token = tokenText
    });
});

app.MapGet("/api/intelligence/briefing", async (
    IIntelligenceService intelligenceService) =>
{
    var response = await intelligenceService.GetBriefingAsync();

    return Results.Ok(response);
})
.RequireAuthorization();

app.MapPost("/api/intelligence/analyze", async (
    IntelligenceRequestDto request,
    IIntelligenceService intelligenceService) =>
{
    var response = await intelligenceService.AnalyzeAsync(request);

    return Results.Ok(response);
})
.RequireAuthorization();

app.Run();

record LoginRequest(string Username, string Password);

