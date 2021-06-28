using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Profiling> Profilings { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                        .HasOne(a => a.Account)
                        .WithOne(e => e.Employee)
                        .HasForeignKey<Account>(f => f.NIK);

            modelBuilder.Entity<Account>()
                        .HasOne(p => p.Profiling)
                        .WithOne(a => a.Account)
                        .HasForeignKey<Profiling>(f => f.NIK);

            modelBuilder.Entity<University>()
                        .HasMany(e => e.Educations)
                        .WithOne(u => u.University);

            modelBuilder.Entity<Education>()
                        .HasMany(p => p.Profilings)
                        .WithOne(e => e.Education);

            modelBuilder.Entity<Role>()
                        .HasMany(a => a.Accounts)
                        .WithMany(r => r.Roles)
                        .UsingEntity<AccountRole>(
                        a => a.HasOne(a => a.Account)
                        .WithMany().HasForeignKey(a => a.NIK),
                        r => r.HasOne(r => r.Role)
                        .WithMany().HasForeignKey(r => r.RoleId));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
