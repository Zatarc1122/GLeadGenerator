using BitMouse.LeadGenerator.Integration.Contract.JsonPlaceholder;
using BitMouse.LeadGenerator.Integration.Service.HttpClients.JsonPlaceholder;
using BitMouse.LeadGenerator.Integration.Service.JsonPlaceholder;
using BitMouse.LeadGenerator.Integration.Service.Settings;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Contrib.WaitAndRetry;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddTransient<IJsonPlaceholderService, JsonPlaceholderService>();

// Add settings to the container
builder.Services.Configure<JsonPlaceholderApiSettings>(builder.Configuration.GetSection(nameof(JsonPlaceholderApiSettings)));
builder.Services.AddScoped(config => config.GetService<IOptionsSnapshot<JsonPlaceholderApiSettings>>()!.Value);

// Add http clients to the container
builder.Services.AddHttpClient<JsonPlaceholderHttpClient>()
    .AddTransientHttpErrorPolicy(policyBuilder =>
        policyBuilder.WaitAndRetryAsync(Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromSeconds(3), retryCount: 2)));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

// Turn HTTPS redirection off for development
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
