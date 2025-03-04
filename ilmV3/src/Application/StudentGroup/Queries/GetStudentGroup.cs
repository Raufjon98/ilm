using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.StudentGroup.Queries;
public record GetStudentGroupQuery(int studentGroupId) : IRequest<StudentGroupVM>;

public class StudentGroupQueryHandler : IRequestHandler<GetStudentGroupQuery, StudentGroupVM>
{
    private readonly IStudentGroupRepository _studentGroupRepository;
    private readonly IMapper _mapper;
    public StudentGroupQueryHandler(IStudentGroupRepository studentGroupRepository, IMapper mapper)
    {
        _studentGroupRepository = studentGroupRepository;
        _mapper = mapper;
    }
    public async Task<StudentGroupVM> Handle(GetStudentGroupQuery request, CancellationToken cancellationToken)
    {
        var result =  await _studentGroupRepository.GetStudentGroupByIdAsync(request.studentGroupId);
        if (result == null)
        {
            throw new KeyNotFoundException($"Record with ID {request.studentGroupId} not found.");
        }
        return _mapper.Map<StudentGroupVM>(result);
    }
}
