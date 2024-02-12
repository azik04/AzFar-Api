using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Final.Domain.Helpers;

public class JwtService
{
    private string secureKey = "9B028A3A-513D-4284-AB5B-DD03688B3DA4";

    public string Generate(int id, int roleId, string name)
    {
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secureKey));
        var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
        var header = new JwtHeader(credentials);

        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Role, roleId.ToString()),
            new Claim(ClaimTypes.Name, name.ToString())
        };

        var payload = new JwtPayload(id.ToString(), null, claims, null, DateTime.Today.AddDays(1)); // 1 day
        var securityToken = new JwtSecurityToken(header, payload);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
    public JwtSecurityToken Verify(string jwt)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(secureKey);
        tokenHandler.ValidateToken(jwt, new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuerSigningKey = true,
            ValidateIssuer = false,
            ValidateAudience = false
        }, out SecurityToken validatedToken);

        return (JwtSecurityToken)validatedToken;
    }
}