using ilmV3.Application.Common.Security;
using ilmV3.Application.Grade.Queries;
using ilmV3.Domain.Constants;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Grade.Commands.CreateGrade;

[Authorize(Policy = Policies.CanAdd)]
public record CreateGradeCommand(GradeDto Grade) : IRequest<GradeVM>;

public class CreateGradeCommandHandler : IRequestHandler<CreateGradeCommand, GradeVM>
{
    private readonly IGradeRepository _gradeRepository;
    public CreateGradeCommandHandler(IMapper mapper, IGradeRepository gradeRepository)
    {
        _gradeRepository = gradeRepository;
    }
    public async Task<GradeVM> Handle(CreateGradeCommand request, CancellationToken cancellationToken)
    {
        var grade = new GradeEntity
        {
            StudentId = request.Grade.StudentId,
            SubjectId = request.Grade.SubjectId,
            TeacherId = request.Grade.TeacherId,
            Grade = request.Grade.Grade,
            ClassDay = request.Grade.ClassDay,
            Date = request.Grade.Date,
        };
        var result = await _gradeRepository.CreateGradeAsync(grade, cancellationToken);
        GradeVM gradeVM = new GradeVM
        {
            Id = result.Id,
            StudentId = result.StudentId,
            SubjectId = result.SubjectId,
            TeacherId = result.TeacherId,
            Grade = result.Grade,
            ClassDay = result.ClassDay,
            Date = result.Date,
        };
        return gradeVM;
    }
}
