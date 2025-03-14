using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Grade.Commands.DeleteGrade  ;
public record DeleteGradeCommand(int  gradeId) : IRequest<bool>;

public class DeleteGradeCommandhandler : IRequestHandler<DeleteGradeCommand, bool>
{
    private readonly IGradeRepository _gradeRepository;
    public DeleteGradeCommandhandler(IGradeRepository gradeRepository)
    {
        _gradeRepository = gradeRepository;
    }
    public async Task<bool> Handle(DeleteGradeCommand request, CancellationToken cancellationToken)
    {
        var grade = await _gradeRepository.GetGradeByIdAsync(request.gradeId);
        if (grade == null)
        {
            throw new KeyNotFoundException($"Record with ID {request.gradeId} not found.");
        }
        return await _gradeRepository.DeleteGradeAsync(grade, cancellationToken);  
    }
}
