using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Teacher.Queries;
public record GetTeachersQuery : IRequest<IEnumerable<TeacherVM>>;

public class GetTeachersQueryHandler : IRequestHandler<GetTeachersQuery, IEnumerable<TeacherVM>>
{
    private readonly ITeacherRepository _teacherRepository;
    public GetTeachersQueryHandler(ITeacherRepository teacherRepository, IMapper mapper)
    {
        _teacherRepository = teacherRepository;
    }
    public async Task<IEnumerable<TeacherVM>> Handle(GetTeachersQuery request, CancellationToken cancellationToken)
    {
        var teachers = await _teacherRepository.GetTeachersAsync();
        List<TeacherVM> result = new List<TeacherVM>();

        foreach (var teacher in teachers)
        {
            TeacherVM teacherVM = new TeacherVM()
            {
                Id = teacher.Id,
                Name = teacher.Name,
            };
            result.Add(teacherVM);
        }
        return result;
    }
}
