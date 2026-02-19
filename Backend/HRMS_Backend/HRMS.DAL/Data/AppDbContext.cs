using Microsoft.EntityFrameworkCore;
using HRMS.DAL.Entities;

namespace HRMS.DAL.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

   
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Salary> Salaries { get; set; }
        public DbSet<Payroll> Payrolls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Salary>()
                .Property(s => s.BaseSalary).HasPrecision(18, 2);

            modelBuilder.Entity<Payroll>()
                .Property(p => p.NetPay).HasPrecision(18, 2);
        }
    }
}