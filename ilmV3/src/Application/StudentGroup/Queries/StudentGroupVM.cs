namespace ilmV3.Application.StudentGroup.Queries;
public class StudentGroupVM
{
    public int Id { get; set; }
    public string? CodeName { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int SubjectId { get; set; }
}
