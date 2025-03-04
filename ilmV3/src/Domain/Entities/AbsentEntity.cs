using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ilmV3.Domain.Entities;
public class AbsentEntity 
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int SubjectId { get; set; } 
    public int TeacherId { get; set; } 
    public string ClassDay { get; set; } = string.Empty;
    public bool Absent { get; set; }
    public DateOnly Date { get; set; }
    public StudentEntity? Student { get; set; } = null!;
    public SubjectEntity? Subject { get; set; } = null!;
    public TeacherEntity? Teacher { get; set; } = null!; 
}
