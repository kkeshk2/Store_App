using Store_App;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors("_MyAllowSubdomainPolicy");
app.UseAuthorization();
app.MapControllers();
app.Run();