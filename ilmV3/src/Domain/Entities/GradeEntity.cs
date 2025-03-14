namespace ilmV3.Domain.Entities;
public class GradeEntity 
{
    public int Id { get; set; }
    public int SubjectId { get; set; } 
    public int StudentId { get; set; } 
    public int TeacherId { get; set; }
    public int Grade { get; set; }
    public string ClassDay { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
    public StudentEntity? Student { get; set; } = null!;
    public SubjectEntity? Subject { get; set; } = null!;
    public TeacherEntity? Teacher { get; set; } = null!;
}
