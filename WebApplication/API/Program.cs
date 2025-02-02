using API.Extensions;
using API.Middlewares;
using API.Utils;
using Application.Services;
using MarkdownProccesor;
using Microsoft.AspNetCore.CookiePolicy;
using Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);

var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(
    options =>
    {
        options.UseNpgsql(configuration.GetConnectionString(nameof(AppDbContext)));
    });
builder.Services.Configure<MinioOptions>(configuration.GetSection(nameof(MinioOptions)));

builder.Services.AddScoped<ResponseResultCreator>();
builder.Services.AddSingleton<MarkdownToHtmlProcessor>();

builder.Services.AddApiAuthentication(configuration);
builder.Services.AddAppServices();
builder.Services.AddAppRepositories();
builder.Services.AddValidators();
builder.Services.AddFilters();

var app = builder.Build();

/*if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}*/


app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});

app.UseMiddleware<LoggingMiddleware>();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();