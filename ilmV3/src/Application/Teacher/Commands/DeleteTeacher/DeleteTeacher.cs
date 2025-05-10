﻿using ilmV3.Application.Common.Security;
using ilmV3.Domain.Constants;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Teacher.Commands.DeleteTeacher;

[Authorize(Policy = Policies.CanUpdateAndDelete)]
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
        ArgumentNullException.ThrowIfNull(teacher);
        return await _teacherRepository.DeleteTeacherAsync(teacher, cancellationToken);
    }
}
