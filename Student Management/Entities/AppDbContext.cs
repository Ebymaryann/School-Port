using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Student_Management.Models;

namespace Student_Management.Entities
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Department> Departments { get; set; }   // ✅ Add this
        public DbSet<Course> Courses { get; set; }           // ✅ Add this
        public DbSet<Assignment> Assignments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Department>()
                .HasMany(d => d.Courses)
                .WithOne(c => c.Department)
                .HasForeignKey(c => c.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Department>()
                .HasMany(d => d.Lecturers)
                .WithOne(l => l.Department)
                .HasForeignKey(l => l.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Course>()
                .HasMany(c => c.Lecturers)
                .WithOne(l => l.Course)
                .HasForeignKey(l => l.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
