using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Teacher.Queries;
public record GetTeachersQuery : IRequest<IEnumerable<TeacherVM>>;

public class GetTeachersQueryHandler : IRequestHandler<GetTeachersQuery, IEnumerable<TeacherVM>>
{
    private readonly ITeacherRepository _teacherRepository;
    private readonly IMapper _mapper;
    public GetTeachersQueryHandler(ITeacherRepository teacherRepository, IMapper mapper)
    {
        _teacherRepository = teacherRepository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<TeacherVM>> Handle(GetTeachersQuery request, CancellationToken cancellationToken)
    {
        return _mapper.Map<IEnumerable<TeacherVM>>(await _teacherRepository.GetTeachersAsync());
    }
}
