using mini_scheduling.Models;
using MySql.Data.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace mini_scheduling.DAL
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class SchedulingContext : DbContext
    {
        public SchedulingContext() : base("SchedulingDB")
        {
        }
        public DbSet<MasterScheduleEntity> MasterSchedules { get; set; }
        public DbSet<RunEntity> Runs { get; set; }
        public DbSet<PartEntity> Parts { get; set; }
        public DbSet<ScheduledObjectEntity> ScheduledObjects { get; set; }
        public DbSet<SupplyEntity> Supplies { get; set; }
        public DbSet<BillOfMaterialsEntity> BillOfMaterials { get; set; }
        public DbSet<BillOfMaterialsRequirementEntity> BillOfMaterialsRequirements { get; set; }
        public DbSet<WorkOrderRequirementEntity> WorkOrderRequirements { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<MasterScheduleEntity>()
                .ToTable("MasterSchedule")
                .HasKey(m => m.MasterScheduleID);

            modelBuilder.Entity<RunEntity>()
                .ToTable("Run")
                .HasKey(r => r.RunID);

            modelBuilder.Entity<PartEntity>()
                .ToTable("Part")
                .HasKey(p => p.PartID);

            modelBuilder.Entity<ScheduledObjectEntity>()
                .ToTable("ScheduledObject")
                .HasKey(s => s.ScheduledObjectID);

            modelBuilder.Entity<SupplyEntity>()
                .ToTable("Supply")
                .HasKey(s => s.SupplyID);

            modelBuilder.Entity<BillOfMaterialsEntity>()
                .ToTable("BillOfMaterials")
                .HasKey(b => b.BillOfMaterialsID);

            modelBuilder.Entity<BillOfMaterialsRequirementEntity>()
                .ToTable("BillOfMaterialsRequirement")
                .HasKey(b => b.BillOfMaterialsRequirementID)
                .HasRequired(b => b.Part).WithMany().HasForeignKey(b => b.RequiredPartID);

            modelBuilder.Entity<BillOfMaterialsRequirementEntity>()
                .HasRequired(b => b.Bom).WithMany().HasForeignKey(b => b.BillOfMaterialsID);

            modelBuilder.Entity<WorkOrderRequirementEntity>()
                .ToTable("WorkOrderRequirement")
                .HasKey(w => w.WorkOrderRequirementID)
                .HasRequired(w => w.Supply).WithMany().HasForeignKey(w => w.SupplyID);
        }
    }
}