using CSN.Domain.Entities.Employees;
using CSN.Persistence.DBContext;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CSN.WebApi.Extensions
{
    public static class EmployeeExtension
    {
        public static async Task<string?> GetEmailAsync(this ClaimsPrincipal claimsPrincipal)
        {
            return await Task.FromResult(claimsPrincipal?.FindFirst(ClaimTypes.Email)?.Value);
        }

        public static async Task<Employee?> GetEmployeeAsync(EFContext context, string email)
        {
            return await context.Employees.FirstOrDefaultAsync(employee => employee.Email == email);
        }

        public static async Task<Employee?> GetEmployeeAsync(this ClaimsPrincipal claimsPrincipal, EFContext context)
        {
            string? email = await GetEmailAsync(claimsPrincipal);
            return await context.Employees.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
