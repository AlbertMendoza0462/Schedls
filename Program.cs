using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Schedls.BLL;
using Schedls.DAL;
using Schedls.Politicas;
using System.Configuration;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Contexto>(conn => conn.UseSqlServer(builder.Configuration.GetConnectionString("ConStr")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
builder.Services.AddTransient<EstadoSolicitudBLL>();
builder.Services.AddTransient<UsuarioBLL>();
builder.Services.AddTransient<SolicitudCambioBLL>();
builder.Services.AddTransient<TipoTurnoBLL>();
builder.Services.AddTransient<TurnoBLL>();
builder.Services.AddTransient<IAuthorizationHandler, ValidaTokenHandler>();
builder.Services.AddHttpContextAccessor();

// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ValidaToken", policy =>
        policy.Requirements.Add(new ValidaTokenRequirement()));
});

builder.Services.AddControllers()
      .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
//Se agrega en generador de Swagger
builder.Services.AddSwaggerGen(options =>
{
    var groupName = "Schedls";

    options.SwaggerDoc(groupName, new OpenApiInfo
    {
        Title = $"{groupName} API",
        Version = groupName,
        Description = "Foo API",
        Contact = new OpenApiContact
        {
            Name = "Albert Mendoza",
            Email = "Albertmendoza0462@gmail.com",
            Url = new Uri("https://albertmendoza.netlify.app"),
        }
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Jwt Authorization",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
//indica la ruta para generar la configuración de swagger
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/Schedls/swagger.json", "Api Caduca REST");
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
