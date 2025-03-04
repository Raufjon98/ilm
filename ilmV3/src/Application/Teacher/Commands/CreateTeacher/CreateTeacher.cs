using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ilmV3.Application.Teacher.Queries;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Teacher.Commands.CreateTeacher;
public record CreateTeacherCommand(TeacherDto teacher, string email, string password) : IRequest<bool>;

public class CreateTeacherCommandHandler : IRequestHandler<CreateTeacherCommand, bool>
{
    private readonly ITeacherRepository _teacherRepository;
    private readonly IMapper _mapper;
    public CreateTeacherCommandHandler(IMapper mapper, ITeacherRepository teacherRepository)
    {
        _mapper = mapper;
        _teacherRepository = teacherRepository;
    }
    public async Task<bool> Handle(CreateTeacherCommand request, CancellationToken cancellationToken)
    {
        var teacher = _mapper.Map<TeacherEntity>(request.teacher);
        return await _teacherRepository.CreateTeacherAsync(teacher, request.email, request.password, cancellationToken);
    }
}
