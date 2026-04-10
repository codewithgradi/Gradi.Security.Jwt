using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

public interface ITokenService<TUser> where TUser : IdentityUser
{
  string CreateToken(TUser user);
  string GenerateRefreshToken();
  ClaimsPrincipal GetClaimsPrincipalFromExpiredToken(string token);
}