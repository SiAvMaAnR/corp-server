using CSN.Domain.Entities.Employees;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CSN.Infrastructure.Helpers
{
    public class AuthOptions
    {
        public static Task<string> CreateEmployeeTokenAsync(Employee? employee, Dictionary<string, string> tokenParams)
        {
            return Task.Run(() =>
            {
                try
                {
                    if (employee == null) throw new Exception();

                    List<Claim> claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, employee.Id.ToString()),
                        new Claim(ClaimTypes.Name, employee.Login),
                        new Claim(ClaimTypes.Email, employee.Email),
                        new Claim(ClaimTypes.Role, employee.Role)
                    };

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimTypes.Name, ClaimTypes.Role);

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

            });
        }

        public static bool CreatePasswordHash(string password, out byte[]? passwordHash, out byte[]? passwordSalt)
        {
            try
            {
                using (var hmac = new HMACSHA512())
                {
                    passwordSalt = hmac.Key;
                    passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                }
                return true;
            }
            catch (Exception)
            {
                passwordSalt = default;
                passwordHash = default;
                return false;
            }
        }

        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            try
            {
                using (var hmac = new HMACSHA512(passwordSalt))
                {
                    var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                    return computedHash.SequenceEqual(passwordHash);
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
