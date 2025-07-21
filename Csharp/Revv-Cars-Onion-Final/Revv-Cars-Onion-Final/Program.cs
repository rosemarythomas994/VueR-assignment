using Revv.cars.Domain;
using Revv.cars.handler.QueryHandler;
using Revv.cars.handler.RepoInterface;
using Revv.cars.Repo.repository;

using MicrosoftLogLevel = Microsoft.Extensions.Logging.LogLevel;

    var builder = WebApplication.CreateBuilder(args);

    // Read config
    builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDBSettings"));


    // Dependency Injection
    builder.Services.AddSingleton<ICarRepository, CarRepository>();
    //builder.Services.AddSingleton<ICarService, CarService>();
//builder.Services.AddSingleton<ILoginService, LoginService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); 

builder.Services.AddAuthorization();
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblyContaining<GetAllCarsQueryHandler>());

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();      
    app.UseSwaggerUI();    
}

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
