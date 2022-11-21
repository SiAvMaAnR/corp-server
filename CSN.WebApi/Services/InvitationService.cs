using System.Text.Json;
using CSN.Domain.Entities.Companies;
using CSN.Domain.Entities.Employees;
using CSN.Domain.Entities.Invitations;
using CSN.Domain.Interfaces.UnitOfWork;
using CSN.Email;
using CSN.Email.Models;
using CSN.Infrastructure.Interfaces.Services;
using CSN.Infrastructure.Models.Common;
using CSN.Infrastructure.Models.InvitationDto;
using CSN.WebApi.Extensions;
using CSN.WebApi.Extensions.CustomExceptions;
using CSN.WebApi.Services.Common;
using Microsoft.AspNetCore.DataProtection;

namespace CSN.WebApi.Services;

public class InvitationService : BaseService<Company>, IInvitationService
{
    private readonly EmailClient emailClient;
    private readonly IConfiguration configuration;
    private readonly IDataProtectionProvider protection;

    public InvitationService(IUnitOfWork unitOfWork, IHttpContextAccessor context, IConfiguration configuration,
        IDataProtectionProvider protection) : base(unitOfWork, context)
    {
        this.configuration = configuration;
        this.protection = protection;

        this.emailClient = new EmailClient(new MessageHandler(new SmtpModel()
        {
            Email = this.configuration["Smtp:Email"],
            Password = this.configuration["Smtp:Password"],
            Host = this.configuration["Smtp:Host"],
            Port = int.Parse(this.configuration["Smtp:Port"]),
        }));
    }

    public async Task<InvitationSendInviteResponse> SendInviteAsync(InvitationSendInviteRequest request)
    {
        Company? company = await this.claimsPrincipal!.GetCompanyAsync(this.unitOfWork);

        if (company == null)
        {
            throw new NotFoundException("Account is not found");
        }

        Employee? employee = await this.unitOfWork.Employee.GetAsync(employee => employee.Email == request.EmployeeEmail);

        if (employee != null)
        {
            throw new BadRequestException("The user is already in the company");
        }

        IDataProtector protector = this.protection.CreateProtector(this.configuration["Invite:SecretKey"]);

        string baseUrl = this.configuration["Invite:BaseUrl"];
        string path = this.configuration["Invite:Path"];

        var invitation = new Invitation()
        {
            Email = request.EmployeeEmail,
            CompanyId = company.Id
        };

        await this.unitOfWork.Invitation.AddAsync(invitation);

        await this.unitOfWork.SaveChangesAsync();

        string inviteJson = JsonSerializer.Serialize(new Invite(invitation.Id, invitation.Email, company.Id));

        string invite = protector.Protect(inviteJson);

        string message = $"{baseUrl}/{path}?invite={invite}";

        await this.emailClient.SendAsync(new MessageModel()
        {
            From = new AddressModel(company.Login, this.configuration["Smtp:Email"]),
            To = new AddressModel("Employee", request.EmployeeEmail),
            Subject = $"Welcome to {company.Login}",
            Message = message
        });

        return new InvitationSendInviteResponse()
        {
            IsSuccess = true
        };
    }

    public async Task<InvitationGetAllResponse> GetInvitesAsync(InvitationGetAllRequest request)
    {
        Company? company = await this.claimsPrincipal!.GetCompanyAsync(this.unitOfWork);

        if (company == null)
        {
            throw new NotFoundException("Account is not found");
        }

        var invitations = await this.unitOfWork.Invitation.GetAllAsync(invitation => invitation.CompanyId == company.Id);

        return new InvitationGetAllResponse(invitations);
    }

    public async Task<InvitationSetStateResponse> SetStateAsync(InvitationSetStateRequest request)
    {
        Company? company = await this.claimsPrincipal!.GetCompanyAsync(this.unitOfWork);

        if (company == null)
        {
            throw new NotFoundException("Account is not found");
        }

        var invitation = await this.unitOfWork.Invitation.GetAsync(invitation =>
            invitation.Id == request.Id && company.Id == invitation.CompanyId);

        if (invitation == null)
        {
            throw new NotFoundException("Invite is not found");
        }

        invitation.IsActive = request.IsActive;

        await this.unitOfWork.Invitation.UpdateAsync(invitation);

        await this.unitOfWork.SaveChangesAsync();

        return new InvitationSetStateResponse()
        {
            Id = invitation.Id,
            IsActive = invitation.IsActive
        };
    }

    public async Task<InvitationRemoveResponse> RemoveInviteAsync(InvitationRemoveRequest request)
    {
        var invitation = await this.unitOfWork.Invitation.GetAsync(invitation => invitation.Id == request.Id);

        if (invitation == null)
        {
            throw new NotFoundException("Invite is not found");
        }

        await this.unitOfWork.Invitation.DeleteAsync(invitation);

        await this.unitOfWork.SaveChangesAsync();

        return new InvitationRemoveResponse()
        {
            IsSuccess = true
        };
    }
}
