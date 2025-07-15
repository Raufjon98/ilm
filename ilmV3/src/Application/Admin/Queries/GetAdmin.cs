using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Common.Security;
using ilmV3.Domain.Constants;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Admin.Queries;

[Authorize(Policy = Policies.CanUpdateAndDelete)]
public record GetAdminQuery(string adminId) : IRequest<AdminVM>;

public class GetAdminQueryHandler : IRequestHandler<GetAdminQuery, AdminVM>
{
    private readonly IIdentityService _identityService;
    private readonly IAplicationDbContext _context;

    public GetAdminQueryHandler(IIdentityService identityService, IAplicationDbContext context)
    {
        _context = context;
        _identityService = identityService;
    }
    public async Task<AdminVM> Handle(GetAdminQuery request, CancellationToken cancellationToken)
    {
        var user = await _identityService.GetUserByIdAsync(request.adminId);
        ArgumentNullException.ThrowIfNull(user);

        var admin = await _context.Admins.FirstOrDefaultAsync(x => x.Id == user.ExternalUserId);
        ArgumentNullException.ThrowIfNull(admin);

        AdminVM adminVM = new AdminVM
        {
            Id = admin.Id,
            Name = admin.Name,
        };
        return adminVM;
    }
}
