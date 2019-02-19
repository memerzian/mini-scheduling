using mini_scheduling.Models;
using MySql.Data.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace mini_scheduling.DAL
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class SchedulingContext: DbContext
    {
        public SchedulingContext(): base("SchedulingDB")
        {
        }
        public DbSet<MasterScheduleEntity> MasterSchedules { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<MasterScheduleEntity>()
                .ToTable("MasterSchedule");
        }
    }
}