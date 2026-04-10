using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;

public class TokenService<TUser> : ITokenService<TUser> where TUser : IdentityUser
{
  private readonly SymmetricSecurityKey _key;
  private readonly JwtSettings _jwtSettings;

  public TokenService(IOptions<JwtSettings> jwtOptions)
  {
    _jwtSettings = jwtOptions.Value;
    _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SigningKey!));
  }

  public string CreateToken(TUser user)
  {
    var claims = new List<Claim>
        {
            new (JwtRegisteredClaimNames.NameId, user.Id),
            new (JwtRegisteredClaimNames.Email, user.Email!),
            new (JwtRegisteredClaimNames.UniqueName, user.UserName!)
        };

    var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Subject = new ClaimsIdentity(claims),
      Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
      SigningCredentials = creds,
      Issuer = _jwtSettings.Issuer,
      Audience = _jwtSettings.Audience
    };

    var tokenHandler = new JwtSecurityTokenHandler();
    return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
  }

  public string GenerateRefreshToken()
  {
    var randomNumber = new byte[64];
    using var rng = RandomNumberGenerator.Create();
    rng.GetBytes(randomNumber);
    return Convert.ToBase64String(randomNumber);
  }
  public ClaimsPrincipal GetClaimsPrincipalFromExpiredToken(string token)
  {
    if (token.StartsWith("Bearer "))
    {
      token = token.Substring(7);
    }
    var tokenValidationParameters = new TokenValidationParameters
    {
      ValidateAudience = false,
      ValidateIssuer = false,
      ValidateIssuerSigningKey = true,
      IssuerSigningKey = _key,
      ValidateLifetime = false,
    };
    var tokenHandler = new JwtSecurityTokenHandler();
    var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
    if (securityToken is not JwtSecurityToken jwtSecurityToken ||
      !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512, StringComparison.InvariantCultureIgnoreCase)
    ) throw new SecurityTokenException("Invalid token");
    return principal;
  }
}