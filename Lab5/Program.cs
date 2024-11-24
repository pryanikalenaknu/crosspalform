using Lab5;
using Lab5.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

ApplicationContext.InitConfiguration(builder.Configuration);
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<Auth0ImplService>();
builder.Services.AddScoped<IAuthService, Auth0ImplService>();
builder.Services.AddHttpClient<Lab6CustomApiService>();
builder.Services.AddScoped<ICustomApiService, Lab6CustomApiService>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = "Auth0";
    })
    .AddCookie()
    .AddOpenIdConnect("Auth0", options =>
    {
        options.Scope.Add("profile");
        options.Scope.Add("email");
        options.Scope.Add("openid");
        
        options.ClientId = ApplicationContext.Id;
        options.ClientSecret = ApplicationContext.Secret;
        options.ResponseType = "code";
        options.Authority = $"https://{ApplicationContext.Domain}";
        
        options.SaveTokens = true;
        options.CallbackPath = new PathString("/signin");
    });

builder.Services.AddSession();

builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource.AddService(serviceName: "Lab8", serviceVersion: "1.0.0"))
    .WithTracing(traceBuilder =>
    {
        traceBuilder
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddZipkinExporter(options =>
            {
                options.Endpoint = new Uri("http://localhost:9411/api/v2/spans");
            });
    })
    .WithMetrics(metricsBuilder =>
    {
        metricsBuilder
            .AddAspNetCoreInstrumentation()
            .AddRuntimeInstrumentation()
            .AddHttpClientInstrumentation()
            .AddMeter("Microsoft.AspNetCore.Hosting")
            .AddMeter("Microsoft.AspNetCore.Server.Kestrel")
            .AddPrometheusExporter();
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseOpenTelemetryPrometheusScrapingEndpoint();
app.UseAuthentication();
app.UseAuthorization();

app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();