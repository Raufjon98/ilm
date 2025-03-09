using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ilmV3.Application.Teacher.Queries;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.StudentGroup.Queries;
public record GetTeacherByStudentGroupQuery(int studentGroupId) : IRequest<TeacherVM>;

public class GetTeacherByStudentGroupQueryHandler : IRequestHandler<GetTeacherByStudentGroupQuery, TeacherVM>
{
    private readonly IMapper _mapper;
    private readonly IStudentGroupRepository _studentGroupRepository;
    public GetTeacherByStudentGroupQueryHandler(IMapper mapper, IStudentGroupRepository studentGroupRepository)
    {
        _mapper = mapper;
        _studentGroupRepository = studentGroupRepository;
    }
    public async Task<TeacherVM> Handle(GetTeacherByStudentGroupQuery request, CancellationToken cancellationToken)
    {
        var result = await _studentGroupRepository.GetTeacherByStudentGroupAsync(request.studentGroupId);
        return _mapper.Map<TeacherVM>(result);
    }
}
