using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ilmV3.Domain.Entities;
public class SubjectEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int TeacherId { get; set; }
    public TeacherEntity? Teacher { get; set; } = null!;
    public StudentGroupEntity? StudentGroup { get; set; }
    public List<TimeTableEntity>? TimeTables { get; set; } = new List<TimeTableEntity>();
}
