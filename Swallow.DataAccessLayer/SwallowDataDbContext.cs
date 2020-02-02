using Microsoft.EntityFrameworkCore;
using Swallow.Core.Domains.CollectedData;
using Swallow.Core.Domains.User;
using Swallow.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Swallow.DataAccessLayer
{
    public class SwallowDataDbContext : DbContext
    {
        public SwallowDataDbContext(DbContextOptions<SwallowDataDbContext> dbContextOptions)
            :base(dbContextOptions)
        {}

        public DbSet<MeasurmentStation> MeasurmentStations { get; set; }
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<DataMeasurment> DataMeasurments { get; set; }
        
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
