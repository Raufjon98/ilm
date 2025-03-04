using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ilmV3.Domain.Entities;
public class TimeTableEntity
{
    public int Id { get; set; }
    public  int StudentGroupId { get; set; }
    public  int TeacherId { get; set; }
    public  int SubjectId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public SubjectEntity? Subject { get; set; }  
    public TeacherEntity? Teacher { get; set; } 
    public required StudentGroupEntity? StudentGroup { get; set; } 
}
