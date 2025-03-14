namespace ilmV3.Domain.interfaces;
public interface IGradeRepository
{
    Task<List<GradeEntity>> GetGradesAsync();
    Task<GradeEntity?> GetGradeByIdAsync(int id);
    Task<GradeEntity> CreateGradeAsync(GradeEntity grade, CancellationToken cancellationToken);
    Task<GradeEntity> UpdateGradeAsync(GradeEntity grade, CancellationToken cancellationToken);
    Task<bool> DeleteGradeAsync(GradeEntity grade, CancellationToken cancellationToken);
}
