using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.StudentGroup.Queries;
public record GetStudentGroupByStudentQuery(int  studentId) : IRequest<IEnumerable<StudentGroupVM>>;

public class GetStudentGroupByStudentQueryHandler : IRequestHandler<GetStudentGroupByStudentQuery, IEnumerable<StudentGroupVM>>
{
    private readonly IStudentGroupRepository _studentGroupRepository;
    private readonly IMapper _mapper;
    public GetStudentGroupByStudentQueryHandler(IStudentGroupRepository studentGroupRepository, IMapper mapper)
    {
        _studentGroupRepository = studentGroupRepository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<StudentGroupVM>> Handle(GetStudentGroupByStudentQuery request, CancellationToken cancellationToken)
    {
        var result = await _studentGroupRepository.GetStudentGroupByStudentAsync(request.studentId);
        if (result == null)
        {
            throw new Exception("The null error!");
        }
        return _mapper.Map<IEnumerable<StudentGroupVM>>(result);
    }
}
