using ilmV3.Application.Common.Security;
using ilmV3.Domain.Constants;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Student.Queries;

[Authorize(Policy = Policies.CanRead)]
public record GetStudentQuery(int studentId) : IRequest<StudentVM>;

public class GetStudentQueryHandler : IRequestHandler<GetStudentQuery, StudentVM>
{
    private readonly IStudentRepository _studentRepository;
    public GetStudentQueryHandler(IStudentRepository studentRepository, IMapper mapper)
    {
        _studentRepository = studentRepository;
    }
    public async Task<StudentVM> Handle(GetStudentQuery request, CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetStudentByIdAsync(request.studentId);
        if (student == null)
        {
            throw new KeyNotFoundException($"Record with ID {request.studentId} not found.");
        }
        var studentVM = new StudentVM()
        {
            Id = student.Id,
            Name = student.Name,
        };
        return studentVM;
    }
}
