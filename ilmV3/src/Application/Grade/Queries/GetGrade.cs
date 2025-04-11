using ilmV3.Application.Common.Security;
using ilmV3.Domain.Constants;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Grade.Queries;

[Authorize(Policy = Policies.CanRead)]
public record GetGradeQuery(int gradeId) : IRequest<GradeVM>;

public class GetGradeQueryHandler : IRequestHandler<GetGradeQuery, GradeVM>
{
    private readonly IGradeRepository _gradeRepository;
    public GetGradeQueryHandler(IGradeRepository gradeRepository, IMapper mapper)
    {
        _gradeRepository = gradeRepository;
    }
    public async Task<GradeVM> Handle(GetGradeQuery request, CancellationToken cancellationToken)
    {
        var grade = await _gradeRepository.GetGradeByIdAsync(request.gradeId);
        if (grade == null)
        {
            throw new KeyNotFoundException($"Record with ID {request.gradeId} not found.");
        }
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
        return gradeVM;
    }
}
