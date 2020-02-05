using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Swallow.Core.Domains.CollectedData;

namespace Swallow.DataAccessLayer.EntitiesConfiguration
{
    internal class MeasurmentStationConfiguration : IEntityTypeConfiguration<MeasurmentStation>
    {
        public void Configure(EntityTypeBuilder<MeasurmentStation> builder)
        {
            builder.Property(t => t.CreationDate)
                .IsRequired();
        }
    }
}
