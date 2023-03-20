using CSN.Domain.Entities.Companies;
using CSN.Domain.Entities.Employees;
using CSN.Domain.Entities.Users;
using CSN.Domain.Interfaces.UnitOfWork;
using System.Linq.Expressions;
using System.Security.Claims;

namespace CSN.Application.Extensions
{
    public static class UserExtension
    {
        public static async Task<User?> GetUserAsync(this ClaimsPrincipal claimsPrincipal, IUnitOfWork unitOfWork,
            params Expression<Func<User, object>>[] includeProperties)
        {
            string? email = claimsPrincipal?.FindFirst(ClaimTypes.Email)?.Value;
            return await unitOfWork.User.GetAsync(user => user.Email == email, includeProperties);
        }

        public static async Task<Employee?> GetEmployeeAsync(this ClaimsPrincipal claimsPrincipal, IUnitOfWork unitOfWork,
            params Expression<Func<Employee, object>>[] includeProperties)
        {
            string? email = claimsPrincipal?.FindFirst(ClaimTypes.Email)?.Value;
            return await unitOfWork.Employee.GetAsync(user => user.Email == email, includeProperties);
        }

        public static async Task<Company?> GetCompanyAsync(this ClaimsPrincipal claimsPrincipal, IUnitOfWork unitOfWork,
            params Expression<Func<Company, object>>[] includeProperties)
        {
            string? email = claimsPrincipal?.FindFirst(ClaimTypes.Email)?.Value;
            return await unitOfWork.Company.GetAsync(user => user.Email == email, includeProperties);
        }

        public static async Task<Company?> GetUserCompanyAsync(this ClaimsPrincipal claimsPrincipal, IUnitOfWork unitOfWork,
            params Expression<Func<User, object>>[] includeProperties)
        {
            string? email = claimsPrincipal?.FindFirst(ClaimTypes.Email)?.Value;
            User? user = await unitOfWork.User.GetAsync(user => user.Email == email, includeProperties);

            return (user is Employee)
                ? (user as Employee)?.Company
                : (user as Company);
        }
    }
}
