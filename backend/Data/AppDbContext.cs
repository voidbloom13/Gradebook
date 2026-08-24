using Microsoft.EntityFrameworkCore;
using Backend.Models;

namespace Backend.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Assignment> Assignments { get; set; }
    public DbSet<AssignmentType> AssignmentTypes { get; set; }
    public DbSet<Grade> Grades { get; set; }
    public DbSet<Term> Terms { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Session> Sessions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasDiscriminator<string>("UserType")
            .HasValue<Student>("Student")
            .HasValue<Teacher>("Teacher");
        modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .HasConversion<string>();
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<Term>()
            .HasKey(t => new { t.TermSeason, t.TermYear });
        modelBuilder.Entity<Term>()
            .Property(t => t.TermSeason)
            .HasConversion<string>();

        modelBuilder.Entity<Enrollment>()
            .HasKey(e => new { e.StudentId, e.CourseId });
        modelBuilder.Entity<Enrollment>()
            .Property(e => e.EnrollmentStatus)
            .HasConversion<string>();

        modelBuilder.Entity<Grade>()
            .HasKey(g => new { g.StudentId, g.AssignmentId });

        modelBuilder.Entity<Course>()
            .Property(c => c.TermSeason)
            .HasConversion<string>();
        modelBuilder.Entity<Course>()
            .HasOne(c => c.Term)
            .WithMany(t => t.Courses)
            .HasForeignKey(c => new
            {
                c.TermSeason,
                c.TermYear
            });

    }
}
