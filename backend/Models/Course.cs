using Backend.Enums;

namespace Backend.Models;

public class Course
{
    public Guid Id { get; set; }
    public string CourseName { get; set; } = null!;
    public Guid TeacherId { get; set; }
    public DateTime CreatedAt { get; set; }
    public TermSeason TermSeason { get; set; }
    public int TermYear { get; set; }
    public Teacher Teacher { get; set; } = null!;
    public Term Term { get; set; } = null!;
    public bool IsCurrentOffering { get; set; }
    public ICollection<Assignment> Assignments { get; set; } = [];
    public ICollection<AssignmentType> AssignmentTypes { get; set; } = [];
    public ICollection<Enrollment> Enrollments { get; set; } = [];
}
