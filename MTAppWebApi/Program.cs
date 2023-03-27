using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MTApp.Utilities.JWTAuthentication;
using MTApp.Utilities.Model;
using MTAPP.DAL;
using MTAPP.DAL.Repository;
using MTAppWebApi.Service;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MTAppWebApi.Service.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MTApp", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."

    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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

//Register Services
builder.Services.AddHttpContextAccessor();
var sqlconnectstring = builder.Configuration.GetConnectionString("MTAppConnection");
builder.Services.AddDbContext<MTAppContext>(options => options.UseSqlServer(sqlconnectstring));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddTransient<IJWTTokenHelper, JWTTokenHelper>();
builder.Services.AddTransient<IAuthService, AuthService>();
var appSettingsSection = builder.Configuration.GetSection("ServiceConfiguration");
builder.Services.Configure<ServiceConfiguration>(appSettingsSection);
var serviceConfiguration = appSettingsSection.Get<ServiceConfiguration>();
var JwtSecretkey = Encoding.ASCII.GetBytes(serviceConfiguration.JwtSettings.Secret);
var tokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(JwtSecretkey),
    ValidateIssuer = true,
    ValidateAudience = true,
    //RequireExpirationTime = false,
    ValidAudience = serviceConfiguration.JwtSettings.Audience,
    ValidIssuer = serviceConfiguration.JwtSettings.Issuer,
    //ValidateLifetime = true,
    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
    ClockSkew = TimeSpan.Zero
};
builder.Services.AddSingleton(tokenValidationParameters);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = tokenValidationParameters;

});
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
 );
});
builder.Services.AddAuthorization(options => {
    options.AddPolicy("MTAuthorizationPolicy",
        policy => {
            policy.RequireAuthenticatedUser()
            .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)            
            .Requirements.Add(new MTAuthorizationRequirement());

            policy.Build();
        });
    options.DefaultPolicy = options.GetPolicy("MTAuthorizationPolicy");
});
builder.Services.AddScoped<IAuthorizationHandler, MTAuthorizationHandler>();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers(); //.RequireAuthorization("MTAuthorizationPolicy");
app.Run();
