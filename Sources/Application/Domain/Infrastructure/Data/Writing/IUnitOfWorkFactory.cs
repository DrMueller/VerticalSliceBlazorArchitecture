namespace VerticalSliceBlazorArchitecture.Domain.Infrastructure.Data.Writing
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}