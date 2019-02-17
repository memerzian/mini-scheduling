using min_scheduling.Models;
using MySql.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace min_scheduling.DAL
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class SchedulingContext: DbContext
    {
        public SchedulingContext(): base("SchedulingDB")
        {
        }
        public DbSet<MasterSchedule> MasterSchedules { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}