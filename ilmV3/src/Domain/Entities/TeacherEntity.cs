namespace ilmV3.Domain.Entities;
public class TeacherEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public SubjectEntity? Subject { get; set; } 
    public List<StudentGroupEntity>? StudentGroups { get; set; } 
    public List<TimeTableEntity>? TimeTables { get; set; } = new List<TimeTableEntity>();
}
