using ilmV3.Application.Common.Security;
using ilmV3.Domain.Constants;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Teacher.Queries;

[Authorize(Policy = Policies.CanUpdateAndDelete)]
public record GetTeacherQuery(int teacherId) : IRequest<TeacherVM>;

public class GetTeacherQueryHandler : IRequestHandler<GetTeacherQuery, TeacherVM>
{
    private readonly ITeacherRepository _teacherRepository;
    public GetTeacherQueryHandler(ITeacherRepository teacherRepository)
    {
        _teacherRepository = teacherRepository;
    }
    public async Task<TeacherVM> Handle(GetTeacherQuery request, CancellationToken cancellationToken)
    {
        var teacher = await _teacherRepository.GetTeacherByIdAsync(request.teacherId);
        if (teacher == null)
        {
            throw new KeyNotFoundException($"Record with ID {request.teacherId} not found!");
        }

        TeacherVM teacherVM = new TeacherVM()
        {
            Id = teacher.Id,
            Name = teacher.Name,
        };
        return teacherVM;
    }
}
