using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.StudentGroup.Queries;
public record GetStudentGroupsQuery : IRequest<IEnumerable<StudentGroupVM>>;

public class GetStudentGroupsQueryHandler : IRequestHandler<GetStudentGroupsQuery, IEnumerable<StudentGroupVM>>
{
    private readonly IStudentGroupRepository _studentGroupRepository;
    private readonly IMapper _mapper;
    public GetStudentGroupsQueryHandler(IStudentGroupRepository studentGroupRepository, IMapper mapper)
    {
        _studentGroupRepository = studentGroupRepository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<StudentGroupVM>> Handle(GetStudentGroupsQuery request, CancellationToken cancellationToken)
    {
        return _mapper.Map<IEnumerable<StudentGroupVM>>( await _studentGroupRepository.GetStudentGroupsAsync());
    }
}
