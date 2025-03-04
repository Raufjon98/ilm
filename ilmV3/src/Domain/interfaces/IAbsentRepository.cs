using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ilmV3.Domain.interfaces;
public interface IAbsentRepository
{
    Task<List<AbsentEntity>> GetAbsentsAsync();
    Task<AbsentEntity?> GetAbsentByIdAsync(int id);
    Task<bool> UpdateAbsentAsync(AbsentEntity absent, CancellationToken cancellationToken);
    Task<bool> CreateAbsentAsync(AbsentEntity absent, CancellationToken cancellationToken);
    Task<bool> DeleteAbsentAsync(AbsentEntity absent, CancellationToken cancellationToken);
}
