namespace ilmV3.Domain.interfaces;
public interface IAbsentRepository
{
    Task<AbsentEntity> UpdateAbsentAsync(AbsentEntity absent, CancellationToken cancellationToken);
    Task<AbsentEntity> CreateAbsentAsync(AbsentEntity absent, CancellationToken cancellationToken);
    Task<bool> DeleteAbsentAsync(AbsentEntity absent, CancellationToken cancellationToken);
    Task<AbsentEntity?> GetAbsentByIdAsync(int absentId);
}
