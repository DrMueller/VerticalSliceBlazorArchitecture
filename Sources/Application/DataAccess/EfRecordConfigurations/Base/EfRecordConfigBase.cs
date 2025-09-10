using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VerticalSliceBlazorArchitecture.Data.Base;

namespace VerticalSliceBlazorArchitecture.DataAccess.EfRecordConfigurations.Base
{
    public abstract class EfRecordConfigBase<T> : IEntityTypeConfiguration<T> where T : EfRecordBase
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(f => f.Id);
            builder.Property(f => f.Id).IsRequired().ValueGeneratedOnAdd();

            builder.ToTable(typeof(T).Name);

            ConfigureEntity(builder);
        }

        protected abstract void ConfigureEntity(EntityTypeBuilder<T> builder);
    }
}