using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SecurityTokenExpiredException = Microsoft.IdentityModel.Tokens.SecurityTokenExpiredException;

namespace mptc.dgc.sample.webapi.Extensions
{
    public static class JwtExtensions
    {

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["Secret"];
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = issuer,
                        ValidAudience = audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!))
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception is SecurityTokenExpiredException)
                            {
                                context.HttpContext.Items["ExpiredToken"] = context.Exception;
                                context.NoResult();
                                return Task.CompletedTask;
                            }
                            context.HttpContext.Items["InvalidToken"] = context.Exception;
                            context.NoResult();
                            return Task.CompletedTask;
                        },
                        OnTokenValidated = context =>
                        {
                            var nameId = context.Principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                            if (!string.IsNullOrEmpty(nameId))
                                context.HttpContext.Items["RateLimitUserId"] = nameId;

                            return Task.CompletedTask;
                        }
                    };

                });
            return services;
        }
    }
}
