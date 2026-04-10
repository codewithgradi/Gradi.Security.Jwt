using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

public static class IdentityServiceExtensions
{
  public static IServiceCollection AddGradiSecurity<TUser>(this IServiceCollection services, IConfiguration config)
      where TUser : IdentityUser
  {
    var jwtSettings = new JwtSettings();
    config.GetSection("JwtSettings").Bind(jwtSettings);
    services.Configure<JwtSettings>(config.GetSection("JwtSettings"));

    services.AddScoped<ITokenService<TUser>, TokenService<TUser>>();

    services.AddAuthentication(opt =>
    {
      opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(opt =>
    {
      opt.TokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuer = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidateAudience = true,
        ValidAudience = jwtSettings.Audience,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SigningKey!))
      };

      opt.Events = new JwtBearerEvents
      {
        OnMessageReceived = context =>
        {
          context.Token = context.Request.Cookies["jwt"];
          return Task.CompletedTask;
        }
      };
    });

    return services;
  }
}