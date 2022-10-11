using AD_Manager.Layers;
using AD_Manager.Layers.ADManagment;
using AD_Manager.Layers.Authentication.UserService;
using AD_Manager.Layers.BLL;
using AD_Manager.Layers.DAL;
using AD_Manager.Layers.Middleware;
using AD_Manager.Layers.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;
using System.Collections;


var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration; 
IWebHostEnvironment environment = builder.Environment;
IConfiguration appset;
if(environment.IsDevelopment())
    appset = configuration.GetSection("JwtConfig_Debuge");
else
    appset = configuration.GetSection("JwtConfig");
builder.Services.Configure<AppSettings>(appset);
var appsetting = appset.Get<AppSettings>();

builder.Services.AddTokenAuthentication(appsetting);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin", builder =>
    {
        builder.AllowAnyMethod().AllowAnyHeader()
            .SetIsOriginAllowed((host)=>true)
            .AllowCredentials();

    });
});
//builder.Services.AddMvc();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserManager, UserManager>();
builder.Services.AddScoped<IDatabaseHelper, DatabaseHelper>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IADHelper, ADHelper>();
builder.Services.AddScoped<ILogInfo, LogInfo>();
builder.Services.AddSingleton<Microsoft.AspNetCore.Authorization.IAuthorizationPolicyProvider, 
    AD_Manager.Layers.Authentication.Permision.PermisionProvider>();

//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "You api title", Version = "v1" });
//    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
//    {
//        Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
//        In = ParameterLocation.Header,
//        Name = "Authorization",
//        Type = SecuritySchemeType.ApiKey,
//        Scheme = "Bearer"
//    });
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowOrigin");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
//app.UseCorsMiddleware();

app.Run();
