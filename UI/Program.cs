using BusinessLogicLayer;
using Core.Redis;
using DataAccessLayer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using StackExchange.Redis;
using System.Globalization;
using UI.Extensions.ServiceRegistration;
using UI.Middlewares;
using Utility.Security.Jwt;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;

// Add services to the container.

builder.Services.AddDataAccessLayerServiceRegistration(configuration);
builder.Services.AddBusinessLogicLayerServices();


builder.Services.AddSingleton<ITokenHelper, JwtHelper>();

builder.Services.AddAuthServices(configuration);

var multiplexer = ConnectionMultiplexer.Connect(configuration["RedisSettings:ConnectionString"]);
builder.Services.AddSingleton<IConnectionMultiplexer>(multiplexer);
builder.Services.AddSingleton<IRedisCacheManager, RedisCacheManager>();

builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder.SetIsOriginAllowed((host) => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});


builder.Services.AddMvc().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                         .AddDataAnnotationsLocalization();
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("en-US"),
        new CultureInfo("tr"),
    };
    options.DefaultRequestCulture = new RequestCulture(culture: "tr", uiCulture: "tr");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});


builder.Services.AddControllersWithViews();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors(x => x.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<CustomExceptionMiddleware>();

app.UseResponseCaching();

app.MapRazorPages();

app.MapControllerRoute(name: "default", pattern: "{controller=Auth}/{action=Login}");
app.MapControllerRoute(name: "Home", pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(name: "Error", pattern: "Error", defaults: new { controller = "Home", action = "Error" });

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.UseDeveloperExceptionPage();

app.Run();
