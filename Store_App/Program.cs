using Microsoft.AspNetCore.Authorization;
using Store_App;
using Store_App.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IAuthorizationHandler, ValidUserHandler>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: "_MyAllowSubdomainPolicy",
        policy =>
        {
            policy.WithOrigins("https://localhost:44412")
                .AllowAnyHeader()
                .AllowAnyMethod();
        }
    );
});

builder.Services.AddAuthorization(options =>
    options.AddPolicy("ValidUser", policy => policy.Requirements.Add(new ValidUserRequirement()))
);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
