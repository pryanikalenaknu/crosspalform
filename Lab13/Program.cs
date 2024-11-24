using Lab13.Services;
using Lab13;
using Lab13.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
ApplicationContext.InitConfiguration(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddHttpClient<Auth0ImplService>();
builder.Services.AddScoped<IAuthService, Auth0ImplService>();
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

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin() // Allows any origin
            .AllowAnyHeader() // Allows any header
            .AllowAnyMethod(); // Allows any method
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("AllowAll");
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();
app.MapControllers();

app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");


app.Run();
