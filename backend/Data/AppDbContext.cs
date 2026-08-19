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
    public DbSet<StudentCourse> StudentCourses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Term>()
            .HasKey(t => new { t.Season, t.Year });
        modelBuilder.Entity<StudentCourse>()
            .HasKey(sc => new { sc.StudentId, sc.CourseId });
        modelBuilder.Entity<Grade>()
            .HasKey(g => new { g.StudentId, g.AssignmentId });
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