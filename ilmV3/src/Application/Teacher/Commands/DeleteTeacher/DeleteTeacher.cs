using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Teacher.Commands.DeleteTeacher;
public record DeleteTeacherCommand(int teacherId) : IRequest<bool>;

public class DeleteTeacherCommandHandler : IRequestHandler<DeleteTeacherCommand, bool>
{
    private readonly ITeacherRepository _teacherRepository;
    public DeleteTeacherCommandHandler(ITeacherRepository teacherRepository)
    {
        _teacherRepository = teacherRepository;
    }
    public async Task<bool> Handle(DeleteTeacherCommand request, CancellationToken cancellationToken)
    {
        var teacher = await _teacherRepository.GetTeacherByIdAsync(request.teacherId);
        if (teacher == null)
        {
            throw new KeyNotFoundException($"Record with id {request.teacherId} not found");
        }
        return await _teacherRepository.DeleteTeacherAsync(teacher, cancellationToken);
    }
}
