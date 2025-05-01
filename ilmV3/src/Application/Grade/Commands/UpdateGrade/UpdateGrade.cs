using ilmV3.Application.Common.Security;
using ilmV3.Application.Grade.Queries;
using ilmV3.Domain.Constants;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Grade.Commands.UpdateGrade;

[Authorize(Policy = Policies.CanUpdateAndDelete)]
public record UpdateGradeCommand(GradeDto Grade, int gradeId) : IRequest<GradeVM?>;

public class UpdateGradeCommandHandler : IRequestHandler<UpdateGradeCommand, GradeVM?>
{
    private readonly IGradeRepository _gradeRepository;
    public UpdateGradeCommandHandler(IGradeRepository gradeRepository)
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
        grade.StudentId = request.Grade.StudentId;
        grade.SubjectId = request.Grade.SubjectId;
        grade.TeacherId = request.Grade.TeacherId;
        grade.ClassDay = request.Grade.ClassDay;
        grade.Grade = request.Grade.Grade;
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
