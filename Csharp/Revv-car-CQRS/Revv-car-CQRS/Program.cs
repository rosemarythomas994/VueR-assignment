using System.Text;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;
using Revv_car_CQRS.Commands;
using Revv_car_CQRS.Filter;
using Revv_car_CQRS.Middleware;
using Revv_car_CQRS.Model;
using Revv_car_CQRS.Notifications.Handlers;
using Revv_car_CQRS.Repository;

using MicrosoftLogLevel = Microsoft.Extensions.Logging.LogLevel;

var logger = NLog.LogManager.Setup().LoadConfigurationFromFile("nlog.config").GetCurrentClassLogger();


try
{
    logger.Debug("Starting Revv_car_CQRS Application");

    var builder = WebApplication.CreateBuilder(args);

    // Use NLog for Dependency Injection logging
    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(MicrosoftLogLevel.Trace);
    builder.Host.UseNLog();
    // Read config
    builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDBSettings"));
    builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

    var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
    var key = Encoding.UTF8.GetBytes(jwtSettings.SecretKey);

    // Dependency Injection
    builder.Services.AddSingleton<ICarRepository, CarRepository>();
    //builder.Services.AddSingleton<ICarService, CarService>();
    builder.Services.AddSingleton<IUserRepository, UserRepository>();
    //builder.Services.AddSingleton<ILoginService, LoginService>();

    builder.Services.AddControllers(options =>
    {
        options.Filters.Add<GlobalExceptionFilter>();
    });

    builder.Services.AddEndpointsApiExplorer();

    // ✅ Swagger + JWT Auth support
    builder.Services.AddSwaggerGen(o =>
{
    o.SwaggerDoc("v1", new OpenApiInfo { Title = "Revv_car_CQRS API", Version = "v1" });
    o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer {your token here}'"
    });
    o.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// ✅ JWT Authentication
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
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
builder.Services.AddAuthorization();

    builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblyContaining<GetAllCarsQuery>());
    builder.Services.AddMediatR(cfg =>
        cfg.RegisterServicesFromAssemblyContaining<CarCreatedEventHandler>());

    //builder.Services.AddMediatR(typeof(GetAllCarsQuery).Assembly);
    //builder.Services.AddMediatR(typeof(LoginUserCommand).Assembly);

    var app = builder.Build();

// Middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
    app.UseStaticFiles(); 
    app.UseMiddleware<ExceptionHandlingMiddleware>();

    app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthentication(); 
app.UseAuthorization();
app.MapControllers();
app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "Application stopped due to an exception");
    throw;
}
finally
{
    LogManager.Shutdown();
}