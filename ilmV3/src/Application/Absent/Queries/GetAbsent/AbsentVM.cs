using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ilmV3.Domain.Entities;

namespace ilmV3.Application.Absent.Queries.GetAbsent;
public class AbsentVM
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int SubjectId { get; set; }
    public int TeacherId { get; set; }
    public string ClassDay { get; set; } = string.Empty;
    public bool Absent { get; set; }
    public DateOnly Date { get; set; }
    private class Mapping:Profile
    {
        public Mapping()
        {
            CreateMap<AbsentEntity, AbsentVM>();
        }
    }
}
