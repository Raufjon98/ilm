using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ilmV3.Domain.Entities;

namespace ilmV3.Application.Student.Queries;
internal class StudentGradeWM
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int SubjectId  { get; set; }
    public int Grade  { get; set; }

}
