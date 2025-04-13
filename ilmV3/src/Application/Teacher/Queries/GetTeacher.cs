using System.Security.Principal;
using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Common.Security;
using ilmV3.Domain.Constants;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Teacher.Queries;

[Authorize(Policy = Policies.CanUpdateAndDelete)]
public record GetTeacherQuery(string teacherId) : IRequest<TeacherVM>;

public class GetTeacherQueryHandler : IRequestHandler<GetTeacherQuery, TeacherVM>
{
    private readonly ITeacherRepository _teacherRepository;
    private readonly IIdentityService _identityService;
    public GetTeacherQueryHandler(ITeacherRepository teacherRepository, IIdentityService identityService)
    {
        _identityService = identityService;
        _teacherRepository = teacherRepository;
    }
    public async Task<TeacherVM> Handle(GetTeacherQuery request, CancellationToken cancellationToken)
    {
        var user = await _identityService.GetUserByIdAsync(request.teacherId);
        ArgumentNullException.ThrowIfNull(user);
        var teacher = await _teacherRepository.GetTeacherByIdAsync(user.ExternalUserId);
        ArgumentNullException.ThrowIfNull(teacher);

        TeacherVM teacherVM = new TeacherVM()
        {
            Id = teacher.Id,
            Name = teacher.Name,
        };
        return teacherVM;
    }
}
