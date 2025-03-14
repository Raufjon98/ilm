using ilmV3.Application.Grade.Queries;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Grade.Commands.UpdateGrade;
public record UpdateGradeCommand(GradeDto grade, int gradeId) : IRequest<GradeVM?>;

public class UpdateGradeCommandHandler : IRequestHandler<UpdateGradeCommand, GradeVM?>
{
    private readonly IGradeRepository _gradeRepository;
    public UpdateGradeCommandHandler(IMapper mapper, IGradeRepository gradeRepository)
    {
        _gradeRepository = gradeRepository;
    }
    public async Task<GradeVM?> Handle(UpdateGradeCommand request, CancellationToken cancellationToken)
    {
        var grade = await _gradeRepository.GetGradeByIdAsync(request.gradeId);
        if (grade == null)
        {
            return null;
        }
        grade.StudentId = request.grade.StudentId;
        grade.SubjectId = request.grade.SubjectId;
        grade.TeacherId = request.grade.TeacherId;
        grade.ClassDay = request.grade.ClassDay;
        grade.Grade = request.grade.Grade;
        grade.Date = DateOnly.FromDateTime(DateTime.UtcNow);

        var result = await _gradeRepository.UpdateGradeAsync(grade, cancellationToken);

        GradeVM gradeVM = new GradeVM
        {
            Id = result.Id,
            StudentId = result.StudentId,
            SubjectId = result.SubjectId,
            TeacherId = result.TeacherId,
            ClassDay = result.ClassDay,
            Grade = result.Grade,
            Date = result.Date,
        };
        return gradeVM;
    }
}
