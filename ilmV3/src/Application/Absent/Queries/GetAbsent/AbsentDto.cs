namespace ilmV3.Application.Absent.Queries.GetAbsent;
public class AbsentDto
{
    public int StudentId { get; set; }
    public int SubjectId { get; set; }
    public int TeacherId { get; set; }
    public string ClassDay { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
    public bool Absent { get; set; }
}
