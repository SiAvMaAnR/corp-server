using CSN.Domain.Entities.Companies;
using CSN.Domain.Entities.Employees;
using CSN.Domain.Interfaces.UnitOfWork;
using CSN.Infrastructure.Interfaces.Services;
using CSN.Persistence.DBContext;
using CSN.WebApi.Services.Common;
using System.Security.Claims;

namespace CSN.WebApi.Services
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        public EmployeeService(EFContext eFContext, IUnitOfWork unitOfWork, ClaimsPrincipal claimsPrincipal)
            : base(eFContext, unitOfWork, claimsPrincipal)
        {
        }


        public async Task AddAsync(Employee employee)
        {
            await unitOfWork.Employee.AddAsync(employee);
            await unitOfWork.SaveChangesAsync();
        }


        public async Task<IEnumerable<Employee>?> GetAllAsync()
        {
            return await unitOfWork.Employee.GetAllAsync();
        }

        public async Task<Employee?> GetAsync(int id)
        {
            return await unitOfWork.Employee.GetAsync(employee => employee.Id == id);
        }
    }
}
