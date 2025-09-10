namespace VerticalSliceBlazorArchitecture.Domain.Infrastructure.Data.Querying
{
    public interface IQuerySpecification<out TResult>
    {
        IQueryable<TResult> Apply(IQueryBase qryProvider);
    }
}