using JetBrains.Annotations;
using VerticalSliceBlazorArchitecture.Data.Base;

namespace VerticalSliceBlazorArchitecture.Testing.Common.Data.EfRecordBuilders.Builders.Base
{
    [PublicAPI]
    public abstract class EfRecordBuilderBase<TRecord> : IEfRecordBuilder
        where TRecord : EfRecordBase, new()
    {
        protected TRecord Record { get; }

        protected EfRecordBuilderBase()
        {
            Record = new TRecord();

            // ReSharper disable once VirtualMemberCallInConstructor
            InitializePrimitiveValues();
        }

        public TRecord Build()
        {
            EnsureNavigationEntities();

            return Record;
        }

        protected virtual void EnsureNavigationEntities()
        {
        }

        protected virtual void InitializePrimitiveValues()
        {
        }
    }
}