using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AspNetCoreSample.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace AspNetCoreFeature.Jwt;

public class JwtTokenGenerator
{
    private readonly IOptions<JwtSettings> _jwtSettings;

    public JwtTokenGenerator(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings;
    }
    public string GenerateJwtToken(Guid userId, string userName, string[] roles)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Value.SignKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        
        
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(ClaimTypes.Name, userName)
        };
        var roleClaims = roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();
        claims.AddRange(roleClaims);
        var token = new JwtSecurityToken(
            issuer: this._jwtSettings.Value.Issuer,
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}