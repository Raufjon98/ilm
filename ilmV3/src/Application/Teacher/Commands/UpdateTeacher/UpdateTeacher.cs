using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ilmV3.Application.Teacher.Queries;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Teacher.Commands.UpdateTeacher;
public record UpdateTeacherCommand(int teacherId, TeacherDto teacher) : IRequest<bool>;

public class UpdateTeacherCommandHandler : IRequestHandler<UpdateTeacherCommand, bool>
{
    private readonly ITeacherRepository _teacherRepository;
    public UpdateTeacherCommandHandler(ITeacherRepository teacherRepository)
    {
        _teacherRepository = teacherRepository;
    }
    public async Task<bool> Handle(UpdateTeacherCommand request, CancellationToken cancellationToken)
    {
        var teacher = await _teacherRepository.GetTeacherByIdAsync(request.teacherId);
        if (teacher == null)
        {
            throw new KeyNotFoundException($"Record with ID {request.teacherId} not found");
        }
        teacher.Name = request.teacher.Name;
        return await _teacherRepository.UpdateTeacherAsync(teacher, cancellationToken);
    }
}
