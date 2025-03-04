using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Teacher.Queries;
public record GetTeacherQuery(int teacherId) : IRequest<TeacherVM>;

public class GetTeacherQueryHandler : IRequestHandler<GetTeacherQuery, TeacherVM>
{
    private readonly ITeacherRepository _teacherRepository;
    private readonly IMapper _mapper;
    public GetTeacherQueryHandler(ITeacherRepository teacherRepository, IMapper mapper)
    {
        _teacherRepository = teacherRepository;
        _mapper = mapper;
    }
    public async Task<TeacherVM> Handle(GetTeacherQuery request, CancellationToken cancellationToken)
    {
        var teacher = await _teacherRepository.GetTeacherByIdAsync(request.teacherId);
        if (teacher == null)
        {
            throw new KeyNotFoundException($"Record with ID {request.teacherId} not found!");
        }
        return _mapper.Map<TeacherVM>(teacher);
    }
}
