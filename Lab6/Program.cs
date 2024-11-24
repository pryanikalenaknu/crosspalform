using System.Text.Json.Serialization;
using Lab6;
using Lab6.Data;
using Lab6.Enums;
using Lab6.Service;
using Lab6.Service.Impl;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<SalesforceService>();
builder.Services.AddScoped<ISalesforceService, SalesforceService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API V1", Version = "v1" });
    c.SwaggerDoc("v2", new OpenApiInfo { Title = "My API V2", Version = "v2" });
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});

ApplicationContext.InitConfiguration(builder.Configuration);

builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);;

builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
});

switch (ApplicationContext.DBType)
{
    case DBType.SQLLite:
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(ApplicationContext.ConnectionString)
        );
        break;
    case DBType.InMemory:
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseInMemoryDatabase(ApplicationContext.ConnectionString)
        );
        break;
    case DBType.PostgreSQL:
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(ApplicationContext.ConnectionString)
        );
        break;
    case DBType.MSSQL:
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(ApplicationContext.ConnectionString)
        );
        break;
    default:
        throw new ArgumentOutOfRangeException();
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = ApplicationContext.Domain;
        options.Audience = ApplicationContext.Audience;
                
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = ApplicationContext.Domain,
            ValidAudience = ApplicationContext.Audience
        };
    });

builder.Services.AddAuthorization();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();
    //context.Seed10X();
}

app.MapControllers();

app.Run();