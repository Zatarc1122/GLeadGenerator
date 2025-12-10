using GLeadGenerator.Api.Settings;
using GLeadGenerator.Contract.Emails;
using GLeadGenerator.Contract.Users;
using GLeadGenerator.Infrastructure.AspNetCore.Middleware.Error;
using GLeadGenerator.Infrastructure.Settings;
using GLeadGenerator.Model.Users;
using GLeadGenerator.Query.Users;
using GLeadGenerator.Repository.Users;
using GLeadGenerator.Service.Emails;
using GLeadGenerator.Service.Settings;
using GLeadGenerator.Service.Users;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Options;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add logging
builder.Host.UseSerilog();

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(config)
    .CreateLogger();

var appName = config.GetValue<string>("Serilog:Properties:ApplicationName");

// Add application services to the container
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IEmailService, EmailService>();

// Add domain services to the container
builder.Services.AddTransient<UserManager>();

// Add repositories to the container
builder.Services.AddTransient<IUserRepository, UserRepository>();

// Add queries to the container
builder.Services.AddTransient<IUserQuery, UserQuery>();

// Add settings to the container
builder.Services.Configure<IntegrationApiSettings>(builder.Configuration.GetSection(nameof(IntegrationApiSettings)));
builder.Services.AddScoped(config => config.GetService<IOptionsSnapshot<IntegrationApiSettings>>()!.Value);

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection(nameof(EmailSettings)));
builder.Services.AddScoped(config => config.GetService<IOptionsSnapshot<EmailSettings>>()!.Value);

// Add http clients to the container
builder.Services.AddHttpClient();

// Add fluent validation
builder.Services.AddValidatorsFromAssemblyContaining(typeof(UserRequestDtoValidator));
builder.Services.AddFluentValidationAutoValidation();

// Add exception handlers
builder.Services.AddTransient<ExceptionHandlerFactory>();
builder.Services.AddTransient<IExceptionHandler, DefaultExceptionHandler>();
builder.Services.AddTransient<IExceptionHandler, FluentValidationExceptionHandler>();
builder.Services.AddTransient<IExceptionHandler, BusinessExceptionHandler>();

// Add connection strings
builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection(nameof(ConnectionStrings)));
builder.Services.AddScoped(config => config.GetService<IOptionsSnapshot<ConnectionStrings>>()!.Value);

// Add builders
builder.Services.AddTransient<IUserBuilder, UserBuilder>();

// Add CORS
var appSettings = builder.Configuration.Get<AppSettings>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins(appSettings!.AllowedOrigins)
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

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

app.UseCors();

app.UseAuthorization();

app.UseMiddleware<ErrorMiddleware>();

app.MapControllers();

try
{
    Log.Information($"{appName} is starting");

    app.Run();
}
catch (Exception e)
{
    Log.Fatal(e, $"{appName} failed to start");
}
finally
{
    Log.CloseAndFlush();
}

