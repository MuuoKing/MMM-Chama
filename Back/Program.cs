using MMM.Chama.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<IChamaStore, InMemoryChamaStore>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy => policy
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod());
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors("Frontend");
app.MapControllers();

app.Run();

public partial class Program { }
