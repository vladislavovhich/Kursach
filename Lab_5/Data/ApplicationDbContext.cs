using Lab_5.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lab_5.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Department> Departments { get; set; }

        public virtual DbSet<Employee> Employees { get; set; }

        public virtual DbSet<Post> Posts { get; set; }

        public virtual DbSet<Project> Projects { get; set; }

        public virtual DbSet<Task> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__B2079BEDB72556CC");

                entity.Property(e => e.DepartmentName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04F114F445702");

                entity.Property(e => e.EmployeeMiddlename)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.EmployeeName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.EmployeeSurname)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Department).WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_EMPLOYEES_DEPARTMENTS");

                entity.HasOne(d => d.Post).WithMany(p => p.Employees)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_EMPLOYEES_POSTS");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasKey(e => e.PostId).HasName("PK__Posts__AA1260184949557A");

                entity.Property(e => e.PostName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.PostSalary).HasColumnType("money");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(e => e.ProjectId).HasName("PK__Projects__761ABEF0E09D9649");

                entity.Property(e => e.ProjectName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.HasKey(e => e.TaskId).HasName("PK__Tasks__7C6949B19681EEA9");

                entity.Property(e => e.BeginDate).HasColumnType("date");
                entity.Property(e => e.CheckDate).HasColumnType("date");
                entity.Property(e => e.EndDate).HasColumnType("date");
                entity.Property(e => e.FailedReason)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Employee).WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_EMPLOYEES_POSTS_122343");

                entity.HasOne(d => d.Project).WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_EMPLOYEES_POSTS_4112");
            });
        }
    }
}