using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ilmV3.Domain.Entities;

namespace ilmV3.Application.Absent.Queries.GetAbsent;
public class AbsentDto
{
    public int StudentId { get; set; }
    public int SubjectId { get; set; }
    public int TeacherId { get; set; }
    public string ClassDay { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
    public bool Absent { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<AbsentDto, AbsentEntity>();
        }
    }
}
