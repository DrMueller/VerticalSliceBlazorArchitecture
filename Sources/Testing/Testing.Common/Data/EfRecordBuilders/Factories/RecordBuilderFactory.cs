using JetBrains.Annotations;
using VerticalSliceBlazorArchitecture.Testing.Common.Data.EfRecordBuilders.Builders.Base;

namespace VerticalSliceBlazorArchitecture.Testing.Common.Data.EfRecordBuilders.Factories
{
    [PublicAPI]
    public static class RecordBuilderFactory
    {
        public static T Create<T>()
            where T : IEfRecordBuilder, new()
        {
            var builder = new T();

            return builder;
        }
    }
}