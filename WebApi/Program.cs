using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;
using Presentation.ActionFilters;
using Services.Concrete;
using Services.Contrats;
using WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));


// Add services to the container.

builder.Services.AddControllers(config =>
{
    config.RespectBrowserAcceptHeader = true; // false by default
    config.ReturnHttpNotAcceptable = true; // false by default
    config.CacheProfiles.Add("5mins",
        new CacheProfile()
        {
            Duration = 300
        });
})
 .AddXmlDataContractSerializerFormatters()
 .AddCustomCsvFormatter()
 .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);
 //.AddNewtonsoftJson();


builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
    
});

builder.Services.ConfigureSwagger();


//Extension Method to configure the Sql Context
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureLoggerManager();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.ConfigureActionFilters(); //IOC for Action Filters
builder.Services.ConfigureCors();
builder.Services.ConfigureDataShaper();
builder.Services.AddCustomMediaTypes(); //Custom Media Types
builder.Services.AddScoped<IBookLinks, BookLinks>();
builder.Services.ConfigureVersioning();
builder.Services.ConfigureResponseCaching();
builder.Services.ConfigureHttpCacheHeaders();
builder.Services.AddMemoryCache();
builder.Services.ConfigureRateLimitingOptions();
builder.Services.AddHttpContextAccessor();

builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWT(builder.Configuration);




var app = builder.Build();

//Configure the Exception Handler Middleware

var logger = app.Services.GetRequiredService<ILoggerService>();

app.ConfigureExceptionHandler(logger);


// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(s => {
        s.SwaggerEndpoint("/swagger/v1/swagger.json", "Btk Akademi V1");
        s.SwaggerEndpoint("/swagger/v2/swagger.json", "Btk Akademi V2");
    });
}

if (app.Environment.IsProduction())
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseIpRateLimiting();
app.UseCors("CorsPolicy");
app.UseResponseCaching();
app.UseHttpCacheHeaders();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
