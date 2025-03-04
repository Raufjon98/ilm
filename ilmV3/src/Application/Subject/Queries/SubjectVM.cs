using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ilmV3.Domain.Entities;

namespace ilmV3.Application.Subject.Queries;
public class SubjectVM
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int TeacherId { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<SubjectEntity, SubjectVM>();
        }
    }
}
