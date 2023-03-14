using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CSN.Infrastructure.AuthOptions;

public class AuthOptions
{
    public static string CreateToken(List<Claim> claims, Dictionary<string, string> tokenParams)
    {
        try
        {
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimTypes.Email, ClaimTypes.Role);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenParams["secretKey"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                audience: tokenParams["audience"],
                issuer: tokenParams["issuer"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(double.Parse(tokenParams["lifeTime"])),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        catch (Exception)
        {
            return "";
        }
    }

    public static bool CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        try
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return true;
        }
        catch (Exception)
        {
            passwordSalt = new byte[0];
            passwordHash = new byte[0];
            return false;
        }
    }

    public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        try
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
        catch (Exception)
        {
            return false;
        }
    }
}
