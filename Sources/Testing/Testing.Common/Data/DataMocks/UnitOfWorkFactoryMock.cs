using JetBrains.Annotations;
using Moq;
using VerticalSliceBlazorArchitecture.Domain.Infrastructure.Data.Writing;

namespace VerticalSliceBlazorArchitecture.Testing.Common.Data.DataMocks
{
    [PublicAPI]
    public class UnitOfWorkFactoryMock
    {
        public readonly Mock<IUnitOfWorkFactory> UowFactoryMock;

        public IUnitOfWorkFactory Object => UowFactoryMock.Object;
        public Mock<IUnitOfWork> UnitOfWorkMock { get; }

        public UnitOfWorkFactoryMock()
        {
            UowFactoryMock = new Mock<IUnitOfWorkFactory>();
            UnitOfWorkMock = new Mock<IUnitOfWork>();
            UowFactoryMock.Setup(f => f.Create()).Returns(UnitOfWorkMock.Object);
        }

        public Mock<TRepo> RegisterRepoMock<TRepo>()
            where TRepo : class, IRepository
        {
            var mock = new Mock<TRepo>();
            UnitOfWorkMock.Setup(f => f.GetRepository<TRepo>()).Returns(mock.Object);

            return mock;
        }
    }
}