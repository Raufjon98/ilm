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
    private readonly IStudentGroupRepository _studentGroupRepository;
    private readonly IMapper _mapper;
    public GetTeacherByStudentGroupQueryHandler(IStudentGroupRepository studentGroupRepository, IMapper mapper)
    {
        _studentGroupRepository = studentGroupRepository;
        _mapper = mapper;
    }
    public async Task<TeacherVM> Handle(GetTeacherByStudentGroupQuery request, CancellationToken cancellationToken)
    {
        var result = await _studentGroupRepository.GetTeacherByStudentGroupAsync(request.studentGroupId);
        if (result == null)
        {
            throw new ArgumentNullException(nameof(result), $"Somthing went wrong!");
        }
        return _mapper.Map<TeacherVM>(result);
    }
}
