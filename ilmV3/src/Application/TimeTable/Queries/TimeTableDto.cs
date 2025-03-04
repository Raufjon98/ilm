using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ilmV3.Domain.Entities;

namespace ilmV3.Application.TimeTable.Queries;
public class TimeTableDto
{
    public int StudentGroupId { get; set; }
    public int TeacherId { get; set; }
    public int SubjectId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<TimeTableDto, TimeTableEntity>();
        }
    }
}
