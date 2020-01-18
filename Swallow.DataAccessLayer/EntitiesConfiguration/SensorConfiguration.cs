using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Swallow.Core.Domains.CollectedData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swallow.DataAccessLayer.EntitiesConfiguration
{
    internal class SensorConfiguration : IEntityTypeConfiguration<Sensor>
    {
        public void Configure(EntityTypeBuilder<Sensor> builder)
        {
        }
    }
}
