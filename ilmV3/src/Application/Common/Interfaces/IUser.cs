namespace ilmV3.Application.Common.Interfaces;

public interface IUser
{
    string? Id { get; }
    public IEnumerable<string> Roles {  get; }
}
