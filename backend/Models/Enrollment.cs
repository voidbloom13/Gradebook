using Backend.Enums;

namespace Backend.Models;

public class Enrollment
{
    public Guid StudentId { get; set; }
    public Guid CourseId { get; set; }
    public DateTime RequestedAt { get; set; }
    public DateTime? EnrolledAt { get; set; }
    public DateTime? DroppedAt { get; set; }
    public EnrollmentStatus EnrollmentStatus { get; set; }
    public Student Student { get; set; } = null!;
    public Course Course { get; set; } = null!;
}