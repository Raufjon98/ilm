using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Common.Security;
using ilmV3.Domain.Constants;

namespace ilmV3.Application.Student.Queries;

[Authorize(Policy = Policies.CanRead)]
public record GetStudentQuery(int studentId) : IRequest<StudentVM>;

public class GetStudentQueryHandler : IRequestHandler<GetStudentQuery, StudentVM>
{
    private readonly IApplicationDbContext _context;
    public GetStudentQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<StudentVM> Handle(GetStudentQuery request, CancellationToken cancellationToken)
    {
        var student = await _context.Students.FirstOrDefaultAsync(x => x.Id == request.studentId);
        ArgumentNullException.ThrowIfNull(student);

        var studentVM = new StudentVM()
        {
            Id = student.Id,
            Name = student.Name,
        };
        return studentVM;
    }
}
