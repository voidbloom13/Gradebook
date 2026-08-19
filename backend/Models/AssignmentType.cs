namespace Backend.Models;

public class AssignmentType
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public string Label { get; set; } = null!;
    public decimal Weight { get; set; }
    public decimal MaxGrade { get; set; }
    public DateTime CreatedAt { get; set; }
    public Course Course { get; set; } = null!;
    public ICollection<Assignment> Assignments { get; set; } = [];
}