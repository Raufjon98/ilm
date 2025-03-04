using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ilmV3.Application.Student.Queries;
using ilmV3.Domain.Entities;

namespace ilmV3.Application.Grade.Queries;
public class GradeDto
{
    public int SubjectId { get; set; }
    public int StudentId { get; set; }
    public int TeacherId { get; set; }
    public int  Grade { get; set; }
    public string ClassDay { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
  private class Mapping: Profile
    {
        public Mapping()
        {
            CreateMap<GradeDto, GradeEntity>();
        }
    }
}
