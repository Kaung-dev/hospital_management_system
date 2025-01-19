using HospitalManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Data
{
    public class HospitalDbContext: DbContext
    {
        public HospitalDbContext(DbContextOptions<HospitalDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Appointment> Appointments { get; set; }


        public DbSet<Nurse> Nurses { get; set; }
        public DbSet<CashFlow> CashFlows { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CashFlow>().HasData(
                new CashFlow
                {
                    Id = 1,
                    AppointmentsCash = 200,
                    LabCash = 500,
                    PharmacyCash = 300
                }
            );

            base.OnModelCreating(modelBuilder);
        }

    }
}
