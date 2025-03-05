using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ilmV3.Application.Student.Queries;
using ilmV3.Application.StudentGroup.Queries;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.StudentGroup.Queries;
public record GetStudentGroupMembersQuery(int studentGroupId) : IRequest<IEnumerable<StudentVM>>;

public class GetStudentGroupMembersQueryHandler : IRequestHandler<GetStudentGroupMembersQuery, IEnumerable<StudentVM>>
{
    private readonly IStudentGroupRepository _studentGroupRepository;
    private readonly IMapper _mapper;
    public GetStudentGroupMembersQueryHandler(IMapper mapper, IStudentGroupRepository studentGroupRepository)
    {
        _mapper = mapper;
        _studentGroupRepository = studentGroupRepository;
    }
    public async Task<IEnumerable<StudentVM>> Handle(GetStudentGroupMembersQuery request, CancellationToken cancellationToken)
    {
        var result = await _studentGroupRepository.GetStudentGroupMembersAsync(request.studentGroupId);
        return _mapper.Map<IEnumerable<StudentVM>>(result);
    }
}
