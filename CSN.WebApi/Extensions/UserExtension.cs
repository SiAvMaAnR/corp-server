using System;
using System.Linq.Expressions;
using CSN.Domain.Entities.Common;
using CSN.Domain.Entities.Companies;
using CSN.Domain.Entities.Employees;
using CSN.Domain.Interfaces.UnitOfWork;
using CSN.Persistence.DBContext;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CSN.WebApi.Extensions
{
    public static class UserExtension
    {
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
    }
}
