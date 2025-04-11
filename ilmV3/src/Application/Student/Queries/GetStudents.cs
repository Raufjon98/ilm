using ilmV3.Application.Common.Security;
using ilmV3.Domain.Constants;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Student.Queries;

[Authorize(Policy = Policies.CanRead)]
public record GetStudentsQuery : IRequest<IEnumerable<StudentVM>>;

public class GetStudentsQueryHandler : IRequestHandler<GetStudentsQuery, IEnumerable<StudentVM>>
{
    private readonly IStudentRepository _studentRepository;
    public GetStudentsQueryHandler(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }
    public async Task<IEnumerable<StudentVM>> Handle(GetStudentsQuery request, CancellationToken cancellationToken)
    {
        var students = await _studentRepository.GetStudentsAsync();
        List<StudentVM> result = new List<StudentVM>();
        foreach (var student in students)
        {
            StudentVM studentVM = new StudentVM()
            {
                Id = student.Id,
                Name = student.Name
            };
            result.Add(studentVM);
        }
        return result;
    }
}
