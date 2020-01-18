using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Swallow.Core.Domains.CollectedData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swallow.DataAccessLayer.EntitiesConfiguration
{
    internal class DataMeasurmentConfiguration : IEntityTypeConfiguration<DataMeasurment>
    {
        public void Configure(EntityTypeBuilder<DataMeasurment> builder)
        {
        }
    }
}
