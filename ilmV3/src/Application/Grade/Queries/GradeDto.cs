namespace ilmV3.Application.Grade.Queries;
public class GradeDto
{
    public int SubjectId { get; set; }
    public int StudentId { get; set; }
    public int TeacherId { get; set; }
    public int  Grade { get; set; }
    public string ClassDay { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
}
