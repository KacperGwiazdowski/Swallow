using Microsoft.EntityFrameworkCore;
using Swallow.Core.Domains.CollectedData;
using Swallow.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Swallow.DataAccessLayer
{
    public class SwallowCollectedDataDbContext : DbContext
    {
        public SwallowCollectedDataDbContext(DbContextOptions<SwallowCollectedDataDbContext> dbContextOptions)
            :base(dbContextOptions)
        {}

        public DbSet<MeasurmentStation> MeasurmentStations { get; set; }
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<DataMeasurment> DataMeasurments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
