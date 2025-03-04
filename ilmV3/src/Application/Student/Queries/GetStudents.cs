using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Student.Queries;
public record GetStudentsQuery : IRequest<IEnumerable<StudentVM>>;

public class GetStudentsQueryHandler : IRequestHandler<GetStudentsQuery, IEnumerable<StudentVM>>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IMapper _mapper;
    public GetStudentsQueryHandler(IStudentRepository studentRepository, IMapper mapper)
    {
        _studentRepository = studentRepository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<StudentVM>> Handle(GetStudentsQuery request, CancellationToken cancellationToken)
    {
        return _mapper.Map<IEnumerable<StudentVM>>( await _studentRepository.GetStudentsAsync());
    }
}
