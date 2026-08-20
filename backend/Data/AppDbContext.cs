using Microsoft.EntityFrameworkCore;
using Backend.Models;

namespace Backend.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Assignment> Assignments { get; set; }
    public DbSet<AssignmentType> AssignmentTypes { get; set; }
    public DbSet<Grade> Grades { get; set; }
    public DbSet<Term> Terms { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Teacher>()
            .Propert(t => t.Role)
            .HasConversion<string>();
        modelBuilder.Entity<Student>()
            .Property(s => s.Role)
            .HasConversion<string>();
        modelBuilder.Entity<Term>()
            .HasKey(t => new { t.TermSeason, t.TermYear });
        modelBuilder.Entity<Term>()
            .Property(t => t.TermSeason)
            .HasConversion<string>();
        modelBuilder.Entity<Enrollment>()
            .HasKey(e => new { e.StudentId, e.CourseId });
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
        modelBuilder.Entity<Enrollment>()
            .Property(e => e.EnrollmentStatus)
            .HasConversion<string>();
    }
}
