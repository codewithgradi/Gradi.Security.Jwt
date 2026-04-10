using System.ComponentModel.DataAnnotations;

public class TokenRequestDto
{
  [Required]
  public string? AccessToken { get; set; }
  [Required]
  public string? RefreshToken { get; set; }
}
public class JwtSettings
{
  public string? Issuer { get; set; }
  public string? Audience { get; set; }
  public string? SigningKey { get; set; }
  public int DurationInMinutes { get; set; } = 5;
}