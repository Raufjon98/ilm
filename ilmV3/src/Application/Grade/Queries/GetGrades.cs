using ilmV3.Application.Common.Security;
using ilmV3.Domain.Constants;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Grade.Queries;

[Authorize(Policy = Policies.CanRead)]
public record GetGradesQuery : IRequest<IEnumerable<GradeVM>>;

public class GetGradesQueryHandler : IRequestHandler<GetGradesQuery, IEnumerable<GradeVM>>
{
    private readonly IGradeRepository _gradeRepository;
    public GetGradesQueryHandler(IGradeRepository gradeRepository, IMapper mapper)
    {
        _gradeRepository = gradeRepository;
    }
    public async Task<IEnumerable<GradeVM>> Handle(GetGradesQuery request, CancellationToken cancellationToken)
    {
        var grades = await _gradeRepository.GetGradesAsync();
        List<GradeVM> result = new List<GradeVM>();
        foreach (var grade in grades)
        {
            var gradeVM = new GradeVM()
            {
                Id = grade.Id,
                StudentId = grade.StudentId,
                TeacherId = grade.TeacherId,
                SubjectId = grade.SubjectId,
                ClassDay = grade.ClassDay,
                Date = grade.Date,
                Grade = grade.Grade
            };
        }
        return result;
    }
}
