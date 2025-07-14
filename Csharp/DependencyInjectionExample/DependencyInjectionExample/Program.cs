using DependencyInjectionExample.Middleware;
using DependencyInjectionExample.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IRandomNumberService, RandomNumberService>();
//builder.Services.AddSingleton<IRandomNumberService, RandomNumberService>();
//builder.Services.AddScoped<IRandomNumberService, RandomNumberService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<RandomLoggingMiddleware>(); // ✅ Correct usage

app.UseAuthorization();
app.MapControllers();
app.Run();
