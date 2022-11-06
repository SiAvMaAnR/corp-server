using System.Text;
using System.Web;
using CSN.Domain.Entities.Companies;
using CSN.Domain.Interfaces.UnitOfWork;
using CSN.Email;
using CSN.Email.Interfaces;
using CSN.Email.Models;
using CSN.Infrastructure.Interfaces.Services;
using CSN.Persistence.DBContext;
using CSN.WebApi.Extensions;
using CSN.WebApi.Extensions.CustomExceptions;
using CSN.WebApi.Services.Common;

namespace CSN.WebApi.Services;

public class EmailService : BaseService<Company>, IEmailService
{
    private readonly EmailClient emailClient;
    private readonly IConfiguration configuration;

    public EmailService(EFContext eFContext, IUnitOfWork unitOfWork, IHttpContextAccessor context, IConfiguration configuration)
     : base(eFContext, unitOfWork, context)
    {
        this.configuration = configuration;

        this.emailClient = new EmailClient(new MessageHandler(new SmtpModel()
        {
            Email = this.configuration["Invite:Email"],
            Password = this.configuration["Invite:Password"],
            Host = this.configuration["Invite:Host"],
            Port = int.Parse(this.configuration["Invite:Port"]),
        }));
    }


    public async Task SendInviteAsync(string email)
    {
        Company? company = await claimsPrincipal!.GetCompanyAsync(this.unitOfWork);

        if (company == null)
        {
            throw new NotFoundException("Account is not found");
        }

        string message = Convert.ToBase64String(Encoding.UTF8.GetBytes(email));

        await this.emailClient.SendAsync(new MessageModel()
        {
            From = new AddressModel(company.Name, this.configuration["Invite:Email"]),
            To = new AddressModel("Employee", email),
            Subject = $"Welcome to {company.Name}",
            Message = HttpUtility.UrlEncode(message)
        });
    }
}
