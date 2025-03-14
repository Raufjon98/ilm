namespace ilmV3.Domain.Entities;
public class StudentEntity 
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<StudentGroupEntity>? Groups { get; set; } = new List<StudentGroupEntity>();
    public List<AbsentEntity>? Absents { get; set; } = new List<AbsentEntity>();
    public List<GradeEntity>? Grades { get; set; } = new List<GradeEntity>();
}
