namespace ilmV3.Application.Absent.Queries.GetAbsent;
public class AbsentVM
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int SubjectId { get; set; }
    public int TeacherId { get; set; }
    public string ClassDay { get; set; } = string.Empty;
    public bool Absent { get; set; }
    public DateOnly Date { get; set; }
}
