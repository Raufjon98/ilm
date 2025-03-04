using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ilmV3.Application.Grade.Queries;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Grade.Commands.UpdateGrade;
public record UpdateGradeCommand(GradeDto grade, int gradeId) : IRequest<bool>;

public class UpdateGradeCommandHandler : IRequestHandler<UpdateGradeCommand, bool>
{
    private readonly IGradeRepository _gradeRepository;
    private readonly IMapper _mapper;
    public UpdateGradeCommandHandler(IMapper mapper, IGradeRepository gradeRepository)
    {
        _mapper = mapper;
        _gradeRepository = gradeRepository;
    }
    public async Task<bool> Handle(UpdateGradeCommand request, CancellationToken cancellationToken)
    {
        var grade = await _gradeRepository.GetGradeByIdAsync(request.gradeId);
        if (grade == null)
        {
            throw new KeyNotFoundException($"Record with ID {request.gradeId} not found.");
        }
        grade.StudentId = request.grade.StudentId;
        grade.SubjectId = request.grade.SubjectId;
        grade.TeacherId = request.grade.TeacherId;
        grade.ClassDay = request.grade.ClassDay;
        grade.Grade = request.grade.Grade;
        grade.Date = DateOnly.FromDateTime(DateTime.UtcNow);

        return await _gradeRepository.UpdateGradeAsync(grade, cancellationToken);
    }
}
