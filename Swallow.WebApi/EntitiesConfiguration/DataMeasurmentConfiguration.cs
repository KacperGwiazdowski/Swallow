using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Swallow.Core.Domains.CollectedData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swallow.WebApi.EntitiesConfiguration
{
    internal class DataMeasurmentConfiguration : IEntityTypeConfiguration<DataMeasurment>
    {
        public void Configure(EntityTypeBuilder<DataMeasurment> builder)
        {
        }
    }
}
