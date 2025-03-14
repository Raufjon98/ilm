using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.StudentGroup.Queries;

namespace ilmV3.Application.Student.Commands.UpdateStudentGroup;
public record UpdateStudentGroupCommand(int studentId, int studentGroupId) : IRequest<StudentGroupVM>;

public class UpdateStudentGroupCommandHandler : IRequestHandler<UpdateStudentGroupCommand, StudentGroupVM>
{
    private readonly IApplicationDbContext _context;
    public UpdateStudentGroupCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<StudentGroupVM> Handle(UpdateStudentGroupCommand request, CancellationToken cancellationToken)
    {
        var student = await _context.Students
         .Include(s => s.Groups)
         .Where(s => s.Id == request.studentId)
         .FirstOrDefaultAsync();
        if (student == null)
        {
            throw new Exception("Student not forund!");
        }
        student.Groups?.Clear();
        var group = await _context.StudentGroups.FindAsync(request.studentGroupId);
        if (group == null)
            throw new Exception("Group does not found!");

        student.Groups?.Add(group);
        var result = await _context.SaveChangesAsync(cancellationToken);
        StudentGroupVM studentGroupVM = new StudentGroupVM
        {
            Id = group.Id,
            Name = group.Name,
            CodeName = group.CodeName,
            SubjectId = group.SubjectId,
        };
        return studentGroupVM;
    }
}
