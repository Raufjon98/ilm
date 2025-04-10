﻿namespace ilmV3.Domain.Entities;
public class StudentGroupEntity
{
    public int Id { get; set; }
    public string? CodeName { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int SubjectId { get; set; }
    public int TeacherId { get; set; }
    public List<StudentEntity>? Students { get; set; } = new List<StudentEntity>();
    public SubjectEntity? Subject { get; set; }
    public List<TimeTableEntity>? TimeTables { get; set; } = new List<TimeTableEntity>();
    public TeacherEntity? Teacher { get; set; }
}
