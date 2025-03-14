using ilmV3.Application.Grade.Queries;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Grade.Commands.CreateGrade;
public record CreateGradeCommand(GradeDto grade) : IRequest<GradeVM>;

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
            StudentId = request.grade.StudentId,
            SubjectId = request.grade.SubjectId,
            TeacherId = request.grade.TeacherId,
            Grade = request.grade.Grade,
            ClassDay = request.grade.ClassDay,
            Date = request.grade.Date,
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
