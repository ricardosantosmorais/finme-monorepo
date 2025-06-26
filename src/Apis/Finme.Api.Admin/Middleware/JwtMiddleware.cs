using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Finme.Api.Admin.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly ILogger<JwtMiddleware> _logger;

        public JwtMiddleware(RequestDelegate next, IConfiguration configuration, ILogger<JwtMiddleware> logger)
        {
            _next = next;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();

            _logger.LogInformation("JwtMiddleware: Processing request");

            if (token != null)
            {
                try
                {
                    _logger.LogDebug("JwtMiddleware: Token found, attempting validation");
                    AttachUserToContext(context, token);
                    _logger.LogDebug("JwtMiddleware: Token validated successfully");
                }
                catch (Exception ex)
                {
                    // Não interrompa o pipeline, apenas registre o erro
                    _logger.LogWarning(ex, "JwtMiddleware: Token validation failed");
                }
            }
            else
            {
                _logger.LogDebug("JwtMiddleware: No token found in request");
            }

            await _next(context);
        }

        private void AttachUserToContext(HttpContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]);

                // Configuração de validação do token
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = _configuration["Jwt:Audience"],
                    ClockSkew = TimeSpan.Zero,
                    ValidateLifetime = true
                };

                // Validar o token
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);

                // Extrair claims (reivindicações) do token
                var username = principal.FindFirst(ClaimTypes.Name)?.Value;
                var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var roles = principal.FindAll(ClaimTypes.Role)?.Select(c => c.Value).ToList();

                // Registrar informações para depuração
                _logger.LogDebug($"Token válido para usuário: {username}, ID: {userId}");
                if (roles != null && roles.Any())
                {
                    _logger.LogDebug($"Papéis do usuário: {string.Join(", ", roles)}");
                }

                // Anexar informações do usuário ao contexto
                context.User = principal;
            }
            catch (SecurityTokenExpiredException ex)
            {
                _logger.LogWarning($"Token expirado: {ex.Message}");
                throw;
            }
            catch (SecurityTokenInvalidSignatureException ex)
            {
                _logger.LogWarning($"Assinatura do token inválida: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Validação de token falhou: {ex.Message}");
                throw;
            }
        }
    }

    // 2. Extensão de Middleware
    public static class JwtMiddlewareExtensions
    {
        public static IApplicationBuilder UseJwtMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JwtMiddleware>();
        }
    }
}
