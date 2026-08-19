namespace Backend.Models;

public class StudentCourse
{
    public Guid StudentId { get; set; }
    public Guid CourseId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? DroppedAt { get; set; }
    public Student Student { get; set; } = null!;
    public Course Course { get; set; } = null!;
}