using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Common.Security;
using ilmV3.Domain.Constants;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Student.Queries;

[Authorize(Policy = Policies.CanRead)]
public record GetStudentQuery(string studentId) : IRequest<StudentVM>;

public class GetStudentQueryHandler : IRequestHandler<GetStudentQuery, StudentVM>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IIdentityService _identityService;
    public GetStudentQueryHandler(IStudentRepository studentRepository, IIdentityService identityService)
    {
        _identityService = identityService;
        _studentRepository = studentRepository;
    }
    public async Task<StudentVM> Handle(GetStudentQuery request, CancellationToken cancellationToken)
    {
        var user = await _identityService.GetUserByIdAsync(request.studentId);
        ArgumentNullException.ThrowIfNull(user);

        var student = await _studentRepository.GetStudentByIdAsync(user.ExternalUserId);
        ArgumentNullException.ThrowIfNull(student);
        var studentVM = new StudentVM()
        {
            Id = student.Id,
            Name = student.Name,
        };
        return studentVM;
    }
}
